using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using DAOsLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoriesLayer
{
    public class NewsArticleRepo : INewsArticleRepo
    {
        public async Task<NewsArticle?> GetNewsArticleById(string articleId)
            => await NewsArticleDAO.Instance.GetNewsArticleById(articleId);

        public async Task<List<NewsArticle>> GetNewsArticles()
            => await NewsArticleDAO.Instance.GetNewsArticles();

        public async Task<List<NewsArticle>> GetActiveNewsArticles()
            => await NewsArticleDAO.Instance.GetActiveNewsArticles();

        public async Task AddNewsArticle(NewsArticle newsArticle)
            => await NewsArticleDAO.Instance.AddNewsArticle(newsArticle);

        public async Task UpdateNewsArticle(string articleId, NewsArticle updatedArticle)
            => await NewsArticleDAO.Instance.UpdateNewsArticle(articleId, updatedArticle);

        public async Task RemoveNewsArticle(string articleId)
            => await NewsArticleDAO.Instance.RemoveNewsArticle(articleId);
    }
}
