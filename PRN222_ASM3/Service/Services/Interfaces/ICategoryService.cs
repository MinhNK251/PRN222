using BusinessObject.Entities;

namespace Service.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();

        Task<Category?> GetCategoryByIdAsync(int id);

        Task AddCategoryAsync(Category category);

        Task UpdateCategoryAsync(Category category);

        Task DeleteCategoryAsync(int id);

        Task<string?> GetCategoryNameAsync(int categoryId);

        Task RefreshCacheAsync();
    }
}