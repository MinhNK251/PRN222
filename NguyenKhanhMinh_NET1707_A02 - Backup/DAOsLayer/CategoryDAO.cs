using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOsLayer
{
    public class CategoryDAO
    {
        private FunewsManagementContext _dbContext;
        private static CategoryDAO? instance;

        public CategoryDAO()
        {
            _dbContext = new FunewsManagementContext();
        }

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoryDAO();
                }
                return instance;
            }
        }

        private FunewsManagementContext CreateDbContext()
        {
            return new FunewsManagementContext();
        }

        // Get category by Id
        public async Task<Category?> GetCategoryById(short categoryId)
        {
            using (var dbContext = CreateDbContext())
            {
                return await dbContext.Categories.AsNoTracking()
                    .Include(c => c.InverseParentCategory)
                    .Include(c => c.NewsArticles)
                    .SingleOrDefaultAsync(c => c.CategoryId == categoryId);
            }
        }

        // Get all categories
        public async Task<List<Category>> GetCategories()
        {
            using (var dbContext = CreateDbContext())
            {
                return await dbContext.Categories.AsNoTracking()
                    .Include(c => c.InverseParentCategory)
                    .Include(c => c.NewsArticles)
                    .ToListAsync();
            }
        }

        // Get active categories only
        public async Task<List<Category>> GetActiveCategories()
        {
            using (var dbContext = CreateDbContext())
            {
                return await dbContext.Categories.AsNoTracking()
                    .Where(c => c.IsActive == true)
                    .Include(c => c.InverseParentCategory)
                    .Include(c => c.NewsArticles)
                    .ToListAsync();
            }
        }

        // Add a new category
        public async Task AddCategory(Category category)
        {
            using (var dbContext = CreateDbContext())
            {
                dbContext.Categories.Add(category);
                await dbContext.SaveChangesAsync();
            }
        }

        // Update an existing category
        public async Task UpdateCategory(short categoryId, Category updatedCategory)
        {
            using (var dbContext = CreateDbContext())
            {
                var existingCategory = GetCategoryById(categoryId);
                if (existingCategory != null)
                {
                    dbContext.Categories.Update(updatedCategory);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        // Remove a category (only if not linked to any news articles)
        public async Task RemoveCategory(short categoryId)
        {
            using (var dbContext = CreateDbContext())
            {
                var existingCategory = await GetCategoryById(categoryId);
                if (existingCategory != null && !existingCategory.NewsArticles.Any())
                {
                    dbContext.Categories.Remove(existingCategory);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}