using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoriesLayer
{
    public interface INewsArticleRepo
    {
        NewsArticle? GetNewsArticleById(string articleId);
        List<NewsArticle> GetNewsArticles();
        List<NewsArticle> GetActiveNewsArticles();
        void AddNewsArticle(NewsArticle newsArticle);
        void UpdateNewsArticle(string articleId, NewsArticle updatedArticle);
        void RemoveNewsArticle(string articleId);
        void RemoveTagsByArticleId(string articleId);
        void AttachTag(Tag tag);
    }
}
