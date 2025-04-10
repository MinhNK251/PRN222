using BusinessObject.Entities;
using DataAccess.DTO;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Service.Services.Interfaces;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IHubContext<ProductCategoryHub> _hub;
        private readonly IDatabase _redisDb;
        private const string RedisCategoryKey = "categories:all";

        public CategoryService(
            ICategoryRepository repository,
            IHubContext<ProductCategoryHub> hub,
            IConnectionMultiplexer redis
        )
        {
            _repository = repository;
            _hub = hub;
            _redisDb = redis.GetDatabase();
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            var cached = await _redisDb.StringGetAsync(RedisCategoryKey);
            if (!cached.IsNullOrEmpty)
            {
                Console.WriteLine("Redis cache hit: all_categories");
                return JsonConvert.DeserializeObject<List<Category>>(cached!)!;
            }
            Console.WriteLine("Cache miss: fetching from DB");
            var categories = await _repository.GetAllAsync();

            await _redisDb.StringSetAsync(
                RedisCategoryKey,
                JsonConvert.SerializeObject(categories, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }),
                TimeSpan.FromMinutes(30)
            );

            return categories;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var all = await GetAllCategoriesAsync();
            return all.FirstOrDefault(c => c.CategoryId == id);
        }

        public async Task<string?> GetCategoryNameAsync(int categoryId)
        {
            var category = await GetCategoryByIdAsync(categoryId);
            return category?.CategoryName;
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _repository.AddAsync(category);
            await RefreshCacheAsync();

            await _hub.Clients.All.SendAsync("CategoryCreated", new CategorySignalRDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            });
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _repository.UpdateAsync(category);
            await RefreshCacheAsync();

            await _hub.Clients.All.SendAsync("CategoryUpdated", new CategorySignalRDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description
            });
        }

        public async Task DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _repository.GetByIdAsync(id);
                if (category == null)
                    throw new Exception("Category not found");

                await _repository.DeleteAsync(id);
                await RefreshCacheAsync();

                await _hub.Clients.All.SendAsync("CategoryDeleted", id);
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Cannot delete category because it is referenced by existing products.", ex);
            }
        }

        public async Task RefreshCacheAsync()
        {
            var categories = await _repository.GetAllAsync();
            var json = JsonConvert.SerializeObject(categories,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

            await _redisDb.StringSetAsync(RedisCategoryKey, json, TimeSpan.FromMinutes(30));
        }
    }
}
