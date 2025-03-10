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
        private FunewsManagementContext _dbContext;
        private static NewsArticleDAO? instance;

        public NewsArticleDAO()
        {
            _dbContext = new FunewsManagementContext();
        }

        public static NewsArticleDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NewsArticleDAO();
                }
                return instance;
            }
        }

        private FunewsManagementContext CreateDbContext()
        {
            return new FunewsManagementContext();
        }

        // Get NewsArticle by Id
        public async Task<NewsArticle?> GetNewsArticleById(string newsArticleId)
        {
            using (var dbContext = CreateDbContext())
            {
                return await dbContext.NewsArticles.AsNoTracking()
                    .Include(n => n.Category)
                    .Include(n => n.Tags)
                    .Include(n => n.CreatedBy)
                    .SingleOrDefaultAsync(n => n.NewsArticleId == newsArticleId);
            }
        }

        // Get all NewsArticles
        public async Task<List<NewsArticle>> GetNewsArticles()
        {
            using (var dbContext = CreateDbContext())
            {
                return await dbContext.NewsArticles.AsNoTracking()
                    .Include(n => n.Category)
                    .Include(n => n.Tags)
                    .Include(n => n.CreatedBy)
                    .ToListAsync();
            }
        }

        // Get all active NewsArticles
        public async Task<List<NewsArticle>> GetActiveNewsArticles()
        {
            using (var dbContext = CreateDbContext())
            {
                return await dbContext.NewsArticles.AsNoTracking()
                    .Where(n => n.NewsStatus == true)
                    .Include(n => n.Category)
                    .Include(n => n.Tags)
                    .Include(n => n.CreatedBy)
                    .ToListAsync();
            }
        }

        // Add a new NewsArticle
        public async Task AddNewsArticle(NewsArticle newsArticle)
        {
            using (var dbContext = CreateDbContext())
            {
                dbContext.NewsArticles.Add(newsArticle);
                await dbContext.SaveChangesAsync();
            }
        }

        // Update an existing NewsArticle
        public async Task UpdateNewsArticle(string newsArticleId, NewsArticle updatedNewsArticle)
        {
            using (var dbContext = CreateDbContext())
            {
                var existingNewsArticle = await GetNewsArticleById(newsArticleId);
                if (existingNewsArticle != null)
                {
                    dbContext.NewsArticles.Update(updatedNewsArticle);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        // Remove a NewsArticle by Id
        public async Task RemoveNewsArticle(string newsArticleId)
        {
            using (var dbContext = CreateDbContext())
            {
                var existingNewsArticle = await GetNewsArticleById(newsArticleId);
                if (existingNewsArticle != null)
                {
                    dbContext.NewsArticles.Remove(existingNewsArticle);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }

}