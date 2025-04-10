using BusinessObject.Entities;
using DataAccess.DTO;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHubContext<ProductCategoryHub> _hub;

        public ProductService(IProductRepository repository, ICategoryRepository categoryRepository, IHubContext<ProductCategoryHub> hub)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _hub = hub;
        }

        public async Task AddProductAsync(Product product)
        {
            await _repository.AddAsync(product);

            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);

            await _hub.Clients.All.SendAsync("ProductCreated", new ProductSignalRDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                CategoryName = category?.CategoryName,
                Weight = product.Weight,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock
            });
        }

        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteAsync(id);
            await _hub.Clients.All.SendAsync("ProductDeleted", id);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _repository.UpdateAsync(product);

            var category = await _categoryRepository.GetByIdAsync(product.CategoryId);

            await _hub.Clients.All.SendAsync("ProductUpdated", new ProductSignalRDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                CategoryName = category?.CategoryName,
                Weight = product.Weight,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock
            });
        }

        public async Task<List<Product>> GetAllProductsWithCategoryAsync()
        {
            return await _repository.GetAllWithCategoryAsync();
        }

        public async Task<List<LowStockAlertDTO>> GetLowStockAlertsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products
                .Where(p => p.UnitsInStock < 10)
                .Select(p => new LowStockAlertDTO
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    UnitsInStock = p.UnitsInStock
                })
                .ToList();
        }
    }
}