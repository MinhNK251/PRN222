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
    public class NewsArticleDAO
    {
        private readonly FunewsManagementContext _context;

        public NewsArticleDAO()
        {
            _context = new FunewsManagementContext();
        }

        // Get all active news articles (for public view)
        public async Task<List<NewsArticle>> GetActiveNewsArticles()
        {
            return await _context.NewsArticles
                .Include(x => x.Category)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.Tags)
                .Where(x => x.NewsStatus == true)
                .ToListAsync();
        }

        // Get all news articles (for Admin/Staff)
        public async Task<List<NewsArticle>> GetAllNewsArticles()
        {
            return await _context.NewsArticles
                .Include(x => x.Category)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.Tags)
                .ToListAsync();
        }

        // Search news articles by title, headline, or content
        public async Task<List<NewsArticle>> SearchNewsArticles(string searchTerm)
        {
            var query = _context.NewsArticles
                .Include(x => x.Category)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.Tags)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(x => x.NewsTitle.ToLower().Contains(searchTerm.ToLower()) ||
                                         x.Headline.ToLower().Contains(searchTerm.ToLower()) ||
                                         x.NewsContent.ToLower().Contains(searchTerm.ToLower()));
            }

            return await query.ToListAsync();
        }

        // Get a news article by ID
        public async Task<NewsArticle> GetNewsArticleById(int id)
        {
            return await _context.NewsArticles
                .Include(x => x.Category)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(m => m.NewsArticleId == id);
        }

        public async Task<List<NewsArticle>> GenerateReport(DateTime startDate, DateTime endDate)
        {
            return await _context.NewsArticles
                .Include(x => x.Category)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.Tags)
                .Where(x => x.CreatedDate >= startDate && x.CreatedDate <= endDate)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<NewsArticle>> GetNewsByCreator(int creatorId)
        {
            return await _context.NewsArticles
                .Include(x => x.Category)
                .Include(x => x.CreatedBy)
                .Include(x => x.UpdatedBy)
                .Include(x => x.Tags)
                .Where(x => x.CreatedById == creatorId)
                .ToListAsync();
        }

        // Add a new news article with selected tags
        public async Task AddNewsArticle(NewsArticleDTO newsArticleDTO, int createdBy, int updatedBy, int[] selectedTagIds)
        {
            try
            {
                // Check if a news article with the same ID already exists
                var existingArticle = await GetNewsArticleById(newsArticleDTO.NewsArticleId);
                if (existingArticle != null)
                {
                    throw new Exception("News article already exists");
                }

                // Map the DTO to the NewsArticle entity
                var newsArticle = new NewsArticle
                {
                    NewsTitle = newsArticleDTO.NewsTitle,
                    Headline = newsArticleDTO.Headline,
                    NewsContent = newsArticleDTO.NewsContent,
                    NewsSource = newsArticleDTO.NewsSource,
                    CategoryId = newsArticleDTO.CategoryId,
                    NewsStatus = newsArticleDTO.NewsStatus,
                    CreatedById = createdBy,
                    UpdatedById = updatedBy,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                };

                // Add the news article to the database
                _context.NewsArticles.Add(newsArticle);
                await _context.SaveChangesAsync();

                // Handle selected tags
                if (selectedTagIds != null && selectedTagIds.Length > 0)
                {
                    var tags = await _context.Tags
                                              .Where(t => selectedTagIds.Contains(t.TagId))
                                              .ToListAsync();
                    newsArticle.Tags = tags;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to add news article", e);
            }
        }

        // Update an existing news article and its associated tags
        public async Task UpdateNewsArticle(NewsArticleDTO newsArticleDTO, int createdBy, int updatedBy, int[] selectedTagIds)
        {
            try
            {
                // Retrieve the existing news article by ID
                var existingArticle = await GetNewsArticleById(newsArticleDTO.NewsArticleId);
                if (existingArticle == null)
                {
                    throw new Exception("News article does not exist");
                }

                // Update properties of the existing article
                existingArticle.NewsTitle = newsArticleDTO.NewsTitle;
                existingArticle.Headline = newsArticleDTO.Headline;
                existingArticle.NewsContent = newsArticleDTO.NewsContent;
                existingArticle.NewsSource = newsArticleDTO.NewsSource;
                existingArticle.CategoryId = newsArticleDTO.CategoryId;
                existingArticle.NewsStatus = newsArticleDTO.NewsStatus;
                existingArticle.UpdatedById = updatedBy;
                existingArticle.ModifiedDate = DateTime.Now;

                // Update the news article in the database
                _context.NewsArticles.Update(existingArticle);
                await _context.SaveChangesAsync();

                // Handle associated tags
                existingArticle.Tags.Clear(); // Clear existing tags to avoid duplicates
                if (selectedTagIds != null && selectedTagIds.Length > 0)
                {
                    var tags = await _context.Tags
                                              .Where(t => selectedTagIds.Contains(t.TagId))
                                              .ToListAsync();
                    existingArticle.Tags = tags;
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Failed to update news article", e);
            }
        }


        // Delete a news article
        public async Task DeleteNewsArticle(int id)
        {
            var existingArticle = await _context.NewsArticles
                                                .Include(na => na.Tags) // Include related tags
                                                .FirstOrDefaultAsync(na => na.NewsArticleId == id);

            if (existingArticle == null)
            {
                throw new Exception("News article not found.");
            }

            // Remove the article from the tags' collections
            foreach (var tag in existingArticle.Tags.ToList())
            {
                tag.NewsArticles.Remove(existingArticle);
            }

            // Remove the article from the database
            _context.NewsArticles.Remove(existingArticle);
            await _context.SaveChangesAsync();
        }
    }
}