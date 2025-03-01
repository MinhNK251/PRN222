using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface ICategoryRepo
    {
        Task<List<Category>> GetAllCategories();
        Task<Category> GetCategoryById(int id);
        Task AddCategory(CategoryDTO category);
        Task UpdateCategory(CategoryDTO category);
        Task DeleteCategory(int id);
    }
}