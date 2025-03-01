using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using DAOsLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly CategoryDAO _categoryDAO;

        // Constructor with dependency injection
        public CategoryRepo(CategoryDAO categoryDAO)
        {
            _categoryDAO = categoryDAO;
        }

        public Task<List<Category>> GetAllCategories()
        {
            return _categoryDAO.GetAllCategories();
        }

        public Task<Category> GetCategoryById(int id)
        {
            return _categoryDAO.GetCategoryById(id);
        }
        public Task AddCategory(CategoryDTO category)
        {
            return _categoryDAO.AddCategory(category);
        }

        public Task UpdateCategory(CategoryDTO category)
        {
            return _categoryDAO.UpdateCategory(category);
        }

        public Task DeleteCategory(int id)
        {
            return _categoryDAO.DeleteCategory(id);
        }
    }
}