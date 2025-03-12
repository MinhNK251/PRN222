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
        public Category? GetCategoryById(short categoryId)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.Categories.AsNoTracking()
                    .Include(c => c.ParentCategory)
                    .Include(c => c.InverseParentCategory)
                    .Include(c => c.NewsArticles)
                    .SingleOrDefault(c => c.CategoryId == categoryId);
            }
        }

        // Get all categories
        public List<Category> GetCategories()
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.Categories.AsNoTracking()
                    .Include(c => c.ParentCategory)
                    .Include(c => c.InverseParentCategory)
                    .Include(c => c.NewsArticles)
                    .ToList();
            }
        }

        // Get active categories only
        public List<Category> GetActiveCategories()
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.Categories.AsNoTracking()
                    .Where(c => c.IsActive == true)
                    .Include(c => c.ParentCategory)
                    .Include(c => c.InverseParentCategory)
                    .Include(c => c.NewsArticles)
                    .ToList();
            }
        }

        // Add a new category
        public void AddCategory(Category category)
        {
            using (var dbContext = CreateDbContext())
            {
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
            }
        }

        // Update an existing category
        public void UpdateCategory(short categoryId, Category updatedCategory)
        {
            using (var dbContext = CreateDbContext())
            {
                var existingCategory = GetCategoryById(categoryId);
                if (existingCategory != null)
                {
                    dbContext.Categories.Update(updatedCategory);
                    dbContext.SaveChanges();
                }
            }
        }

        // Remove a category (only if not linked to any news articles)
        public void RemoveCategory(short categoryId)
        {
            using (var dbContext = CreateDbContext())
            {
                var existingCategory = GetCategoryById(categoryId);
                if (existingCategory != null && !existingCategory.NewsArticles.Any())
                {
                    dbContext.Categories.Remove(existingCategory);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}