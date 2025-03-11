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
    //    public class NewsArticleDAO
    //    {
    //        private FunewsManagementContext _dbContext;
    //        private static NewsArticleDAO? instance;

    //        public NewsArticleDAO()
    //        {
    //            _dbContext = new FunewsManagementContext();
    //        }

    //        public static NewsArticleDAO Instance
    //        {
    //            get
    //            {
    //                if (instance == null)
    //                {
    //                    instance = new NewsArticleDAO();
    //                }
    //                return instance;
    //            }
    //        }

    //        private FunewsManagementContext CreateDbContext()
    //        {
    //            return new FunewsManagementContext();
    //        }

    //        // Get NewsArticle by Id
    //        public async Task<NewsArticle?> GetNewsArticleById(string newsArticleId)
    //        {
    //            using (var dbContext = CreateDbContext())
    //            {
    //                return await dbContext.NewsArticles.AsNoTracking()
    //                    .Include(n => n.Category)
    //                    .Include(n => n.Tags)
    //                    .Include(n => n.CreatedBy)
    //                    .Include(n => n.UpdatedBy)
    //                    .SingleOrDefaultAsync(n => n.NewsArticleId == newsArticleId);
    //            }
    //        }

    //        // Get all NewsArticles
    //        public async Task<List<NewsArticle>> GetNewsArticles()
    //        {
    //            using (var dbContext = CreateDbContext())
    //            {
    //                return await dbContext.NewsArticles.AsNoTracking()
    //                    .Include(n => n.Category)
    //                    .Include(n => n.Tags)
    //                    .Include(n => n.CreatedBy)
    //                    .Include(n => n.UpdatedBy)
    //                    .ToListAsync();
    //            }
    //        }

    //        // Get all active NewsArticles
    //        public async Task<List<NewsArticle>> GetActiveNewsArticles()
    //        {
    //            using (var dbContext = CreateDbContext())
    //            {
    //                return await dbContext.NewsArticles.AsNoTracking()
    //                    .Where(n => n.NewsStatus == true)
    //                    .Include(n => n.Category)
    //                    .Include(n => n.Tags)
    //                    .Include(n => n.CreatedBy)
    //                    .Include(n => n.UpdatedBy)
    //                    .ToListAsync();
    //            }
    //        }

    //        // Add a new NewsArticle
    //        public async Task AddNewsArticle(NewsArticle newsArticle)
    //        {
    //            using (var dbContext = CreateDbContext())
    //            {
    //                dbContext.NewsArticles.Add(newsArticle);
    //                await dbContext.SaveChangesAsync();
    //            }
    //        }

    //        // Update an existing NewsArticle
    //        public async Task UpdateNewsArticle(string newsArticleId, NewsArticle updatedNewsArticle)
    //        {
    //            using (var dbContext = CreateDbContext())
    //            {
    //                var existingNewsArticle = await GetNewsArticleById(newsArticleId);
    //                if (existingNewsArticle != null)
    //                {
    //                    dbContext.NewsArticles.Update(updatedNewsArticle);
    //                    await dbContext.SaveChangesAsync();
    //                }
    //            }
    //        }

    //        // Remove a NewsArticle by Id
    //        public async Task RemoveNewsArticle(string newsArticleId)
    //        {
    //            using (var dbContext = CreateDbContext())
    //            {
    //                var existingNewsArticle = await GetNewsArticleById(newsArticleId);
    //                if (existingNewsArticle != null)
    //                {
    //                    dbContext.NewsArticles.Remove(existingNewsArticle);
    //                    await dbContext.SaveChangesAsync();
    //                }
    //            }
    //        }

    //        // Remove NewsArticle Tags By NewsArticle Id
    //        public async Task RemoveTagsByArticleId(string articleId)
    //        {
    //            using (var dbContext = new FunewsManagementContext())
    //            {
    //                var article = await dbContext.NewsArticles
    //                                             .Include(a => a.Tags)
    //                                             .FirstOrDefaultAsync(a => a.NewsArticleId == articleId);
    //                if (article != null)
    //                {
    //                    article.Tags.Clear();
    //                    await dbContext.SaveChangesAsync();
    //                }
    //            }
    //        }

    //        public void AttachTag(Tag tag)
    //        {
    //            _dbContext.Tags.Attach(tag);
    //        }
    //    }
    public class NewsArticleDAO
    {
        private readonly FunewsManagementContext _dbContext;
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

        // Get NewsArticle by Id
        public async Task<NewsArticle?> GetNewsArticleById(string newsArticleId)
        {
            return await _dbContext.NewsArticles.AsNoTracking()
                .Include(n => n.Category)
                .Include(n => n.Tags)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdatedBy)
                .SingleOrDefaultAsync(n => n.NewsArticleId == newsArticleId);
        }

        // Get all NewsArticles
        public async Task<List<NewsArticle>> GetNewsArticles()
        {
            return await _dbContext.NewsArticles.AsNoTracking()
                .Include(n => n.Category)
                .Include(n => n.Tags)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdatedBy)
                .ToListAsync();
        }

        // Get all active NewsArticles
        public async Task<List<NewsArticle>> GetActiveNewsArticles()
        {
            return await _dbContext.NewsArticles.AsNoTracking()
                .Where(n => n.NewsStatus == true)
                .Include(n => n.Category)
                .Include(n => n.Tags)
                .Include(n => n.CreatedBy)
                .Include(n => n.UpdatedBy)
                .ToListAsync();
        }

        // Add a new NewsArticle
        public async Task AddNewsArticle(NewsArticle newsArticle)
        {
            _dbContext.NewsArticles.Add(newsArticle);
            await _dbContext.SaveChangesAsync();
        }

        // Update an existing NewsArticle
        public async Task UpdateNewsArticle(string newsArticleId, NewsArticle updatedNewsArticle)
        {
            var existingNewsArticle = await _dbContext.NewsArticles
                .Include(n => n.Tags)
                .SingleOrDefaultAsync(n => n.NewsArticleId == newsArticleId);

            if (existingNewsArticle != null)
            {
                _dbContext.Entry(existingNewsArticle).CurrentValues.SetValues(updatedNewsArticle);

                // Update Tags
                existingNewsArticle.Tags.Clear();
                foreach (var tag in updatedNewsArticle.Tags)
                {
                    existingNewsArticle.Tags.Add(tag);
                }

                await _dbContext.SaveChangesAsync();
            }
        }

        // Remove a NewsArticle by Id
        public async Task RemoveNewsArticle(string newsArticleId)
        {
            var existingNewsArticle = await _dbContext.NewsArticles
                .SingleOrDefaultAsync(n => n.NewsArticleId == newsArticleId);

            if (existingNewsArticle != null)
            {
                _dbContext.NewsArticles.Remove(existingNewsArticle);
                await _dbContext.SaveChangesAsync();
            }
        }

        // Remove NewsArticle Tags By NewsArticle Id
        public async Task RemoveTagsByArticleId(string articleId)
        {
            var article = await _dbContext.NewsArticles
                .Include(a => a.Tags)
                .FirstOrDefaultAsync(a => a.NewsArticleId == articleId);

            if (article != null)
            {
                article.Tags.Clear();
                await _dbContext.SaveChangesAsync();
            }
        }

        public void AttachTag(Tag tag)
        {
            _dbContext.Tags.Attach(tag);
        }
    }
}