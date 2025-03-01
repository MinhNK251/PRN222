using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOsLayer
{
    public class CategoryDAO
    {
        private readonly FunewsManagementContext _context;

        public CategoryDAO(FunewsManagementContext context)
        {
            _context = context;
        }

        // Get all categories
        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        //Get by id
        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories
                         .Include(c => c.ParentCategory)
                         .Include(c => c.NewsArticles)
                         .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        // Add a new category
        public async Task AddCategory(CategoryDTO categoryDTO)
        {
            try
            {
                // Check if the category already exists
                var existingCategory = await _context.Categories
                                                    .FirstOrDefaultAsync(c => c.CategoryId == categoryDTO.CategoryId);

                if (existingCategory != null)
                {
                    throw new Exception("Category already exists.");
                }

                // Map DTO to entity
                var category = new Category
                {
                    CategoryName = categoryDTO.CategoryName,
                    CategoryDescription = categoryDTO.CategoryDescription,
                    ParentCategoryId = categoryDTO.ParentCategoryId,
                    IsActive = categoryDTO.IsActive
                };

                // Add the category to the database
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the category.", ex);
            }
        }

        // Update a category
        public async Task UpdateCategory(CategoryDTO categoryDTO)
        {
            try
            {
                // Fetch the existing category
                var existingCategory = await _context.Categories
                                                    .FirstOrDefaultAsync(c => c.CategoryId == categoryDTO.CategoryId);

                if (existingCategory == null)
                {
                    throw new Exception("Category not found.");
                }

                // Map DTO to entity
                existingCategory.CategoryName = categoryDTO.CategoryName;
                existingCategory.CategoryDescription = categoryDTO.CategoryDescription;
                existingCategory.ParentCategoryId = categoryDTO.ParentCategoryId;
                existingCategory.IsActive = categoryDTO.IsActive;

                // Update the category in the database
                _context.Categories.Update(existingCategory);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the category.", ex);
            }
        }

        // Delete a category (only if not used by any news article)
        public async Task DeleteCategory(int id)
        {
            // Fetch the category with its related entities
            var existingCategory = await _context.Categories
                .Include(c => c.NewsArticles) // Include related news articles
                .Include(c => c.InverseParentCategory) // Include child categories
                .FirstOrDefaultAsync(c => c.CategoryId == id);

            if (existingCategory == null)
            {
                throw new Exception("Category not found.");
            }

            if (existingCategory.NewsArticles.Any())
            {
                throw new Exception("Category is used by one or more news articles and cannot be deleted.");
            }

            // If the category is not associated with any news articles, delete it
            _context.Categories.Remove(existingCategory);
            await _context.SaveChangesAsync();
        }
    }
}