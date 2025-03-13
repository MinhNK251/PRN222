using BusinessObjectsLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoriesLayer
{
    public interface INewsArticleRepo
    {
        List<NewsArticle> GetNewsArticles();
        List<NewsArticle> GetNewsArticlesByCreatedBy(int createdById);
        NewsArticle? GetNewsArticleById(string articleId);
        List<NewsArticle> GetArticlesByTagId(int tagId);
        List<NewsArticle> GetActiveNewsArticles();
        void AddNewsArticle(NewsArticle newsArticle);
        void UpdateNewsArticle(string articleId, NewsArticle updatedArticle);
        void RemoveNewsArticle(string articleId);
        void RemoveTagsByArticleId(string articleId);
        List<NewsArticle> GetNewsArticlesByDateRange(DateTime startDate, DateTime endDate);
    }
}
