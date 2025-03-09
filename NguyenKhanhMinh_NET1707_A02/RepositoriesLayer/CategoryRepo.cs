using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using DAOsLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoriesLayer
{
    public class CategoryRepo : ICategoryRepo
    {
        public async Task<Category?> GetCategoryById(short categoryId)
            => await CategoryDAO.Instance.GetCategoryById(categoryId);

        public async Task<List<Category>> GetCategories()
            => await CategoryDAO.Instance.GetCategories();

        public async Task<List<Category>> GetActiveCategories()
            => await CategoryDAO.Instance.GetActiveCategories();

        public async Task AddCategory(Category category)
            => await CategoryDAO.Instance.AddCategory(category);

        public async Task UpdateCategory(short categoryId, Category category)
            => await CategoryDAO.Instance.UpdateCategory(categoryId, category);

        public async Task RemoveCategory(short categoryId)
            => await CategoryDAO.Instance.RemoveCategory(categoryId);
    }
}