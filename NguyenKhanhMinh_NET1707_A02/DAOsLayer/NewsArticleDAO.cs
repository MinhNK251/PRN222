using BusinessObjectsLayer.DTOs;
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

        // Get all NewsArticles
        public List<NewsArticle> GetNewsArticles()
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.NewsArticles.AsNoTracking()
                    .Include(n => n.Category)
                    .Include(n => n.Tags)
                    .Include(n => n.CreatedBy)
                    .Include(n => n.UpdatedBy)
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
                var article = dbContext.NewsArticles
                                       .Include(a => a.Tags)
                                       .FirstOrDefault(a => a.NewsArticleId == articleId);
                if (article != null)
                {
                    article.Tags.Clear();
                    dbContext.SaveChanges();
                }
            }
        }

        public void AttachTag(Tag tag)
        {
            _dbContext.Tags.Attach(tag);
        }
    }
}
