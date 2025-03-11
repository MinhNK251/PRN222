using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoriesLayer
{
    public interface ICategoryRepo
    {
        Task<Category?> GetCategoryById(short categoryId);
        Task<List<Category>> GetCategories();
        Task<List<Category>> GetActiveCategories();
        Task AddCategory(Category category);
        Task UpdateCategory(short categoryId, Category category);
        Task RemoveCategory(short categoryId);
    }
}