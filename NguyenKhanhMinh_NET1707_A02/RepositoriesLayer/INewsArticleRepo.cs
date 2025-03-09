using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoriesLayer
{
    public interface INewsArticleRepo
    {
        Task<NewsArticle?> GetNewsArticleById(string articleId);
        Task<List<NewsArticle>> GetNewsArticles();
        Task<List<NewsArticle>> GetActiveNewsArticles();
        Task AddNewsArticle(NewsArticle newsArticle);
        Task UpdateNewsArticle(string articleId, NewsArticle updatedArticle);
        Task RemoveNewsArticle(string articleId);
    }
}
