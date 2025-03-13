using BusinessObjectsLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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

        // Get all NewsArticles
        public List<NewsArticle> GetNewsArticles()
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.NewsArticles.AsNoTracking()
                    //.OrderByDescending(n => n.NewsArticleId.Length) // Longer IDs first
                    //.ThenByDescending(n => n.NewsArticleId) // Then order lexicographically
                    .OrderByDescending(n => n.NewsArticleId)
                    .Include(n => n.Category)
                    .Include(n => n.Tags)
                    .Include(n => n.CreatedBy)
                    .Include(n => n.UpdatedBy)
                    .ToList();
            }
        }

        // Get all NewsArticles by CreatedById
        public List<NewsArticle> GetNewsArticlesByCreatedBy(int createdById)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.NewsArticles.AsNoTracking()
                    .OrderByDescending(n => n.NewsArticleId)
                    .Include(n => n.Category)
                    .Include(n => n.Tags)
                    .Include(n => n.CreatedBy)
                    .Include(n => n.UpdatedBy)
                    .Where(n => n.CreatedById == createdById)
                    .ToList();
            }
        }

        // Get NewsArticle by Id
        public NewsArticle? GetNewsArticleById(string newsArticleId)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.NewsArticles.AsNoTracking()
                    .Include(n => n.Category)
                    .Include(n => n.Tags)
                    .Include(n => n.CreatedBy)
                    .Include(n => n.UpdatedBy)
                    .SingleOrDefault(n => n.NewsArticleId == newsArticleId);
            }
        }

        // Get articles by tag ID
        public List<NewsArticle> GetArticlesByTagId(int tagId)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.NewsArticles.AsNoTracking()
                    .Where(n => dbContext.Set<Dictionary<string, object>>("NewsTag")
                        .Any(nt => EF.Property<int>(nt, "TagId") == tagId && 
                                   EF.Property<string>(nt, "NewsArticleId") == n.NewsArticleId))
                    .OrderByDescending(n => n.NewsArticleId)
                    .ToList();
            }
        }

        // Get all active NewsArticles
        public List<NewsArticle> GetActiveNewsArticles()
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.NewsArticles.AsNoTracking()
                    .Where(n => n.NewsStatus == true)
                    .OrderByDescending(n => n.NewsArticleId)
                    .Include(n => n.Category)
                    .Include(n => n.Tags)
                    .Include(n => n.CreatedBy)
                    .Include(n => n.UpdatedBy)
                    .ToList();
            }
        }

        // Add a new NewsArticle
        public void AddNewsArticle(NewsArticle newsArticle)
        {
            using (var dbContext = CreateDbContext())
            {
                // Attach existing tags to the context to prevent re-insertion
                foreach (var tag in newsArticle.Tags)
                {
                    dbContext.Tags.Attach(tag);
                }

                dbContext.NewsArticles.Add(newsArticle);
                dbContext.SaveChanges();
            }
        }

        // Update an existing NewsArticle
        public void UpdateNewsArticle(string newsArticleId, NewsArticle updatedNewsArticle)
        {
            using (var dbContext = CreateDbContext())
            {
                var existingNewsArticle = GetNewsArticleById(newsArticleId);
                if (existingNewsArticle != null)
                {
                    dbContext.NewsArticles.Update(updatedNewsArticle);
                    dbContext.SaveChanges();
                }
            }
        }

        // Remove a NewsArticle by Id
        public void RemoveNewsArticle(string newsArticleId)
        {
            using (var dbContext = CreateDbContext())
            {
                RemoveTagsByArticleId(newsArticleId);
                var existingNewsArticle = GetNewsArticleById(newsArticleId);
                if (existingNewsArticle != null)
                {
                    dbContext.NewsArticles.Remove(existingNewsArticle);
                    dbContext.SaveChanges();
                }
            }
        }

        // Remove NewsArticle Tags By NewsArticle Id
        public void RemoveTagsByArticleId(string articleId)
        {
            using (var dbContext = new FunewsManagementContext())
            {
                // Get all NewsTag records linked to the article
                var tags = dbContext.Set<Dictionary<string, object>>("NewsTag")
                                    .Where(nt => EF.Property<string>(nt, "NewsArticleId") == articleId)
                                    .ToList();

                if (tags.Any())
                {
                    dbContext.RemoveRange(tags);
                    dbContext.SaveChanges();
                }
            }
        }

        public List<NewsArticle> GetNewsArticlesByDateRange(DateTime startDate, DateTime endDate)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.NewsArticles.AsNoTracking()
                    .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate.AddDays(1))
                    .OrderByDescending(n => n.NewsArticleId)
                    .Include(n => n.Category)
                    .Include(n => n.CreatedBy)
                    .Include(n => n.UpdatedBy)
                    .ToList();
            }
        }
    }
}
