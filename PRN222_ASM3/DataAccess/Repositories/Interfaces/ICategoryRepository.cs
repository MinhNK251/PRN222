using BusinessObject.Entities;

namespace DataAccess.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();

        Task<Category> GetByIdAsync(int id);

        Task AddAsync(Category category);

        Task UpdateAsync(Category category);

        Task DeleteAsync(int id);
        Task<string?> GetCategoryNameByIdAsync(int categoryId);
    }
}