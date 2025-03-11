﻿using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using DAOsLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoriesLayer
{
    public class NewsArticleRepo : INewsArticleRepo
    {
        public NewsArticle? GetNewsArticleById(string articleId)
            => NewsArticleDAO.Instance.GetNewsArticleById(articleId);

        public List<NewsArticle> GetNewsArticles()
            => NewsArticleDAO.Instance.GetNewsArticles();

        public List<NewsArticle> GetActiveNewsArticles()
            => NewsArticleDAO.Instance.GetActiveNewsArticles();

        public void AddNewsArticle(NewsArticle newsArticle)
            => NewsArticleDAO.Instance.AddNewsArticle(newsArticle);

        public void UpdateNewsArticle(string articleId, NewsArticle updatedArticle)
            => NewsArticleDAO.Instance.UpdateNewsArticle(articleId, updatedArticle);

        public void RemoveNewsArticle(string articleId)
            => NewsArticleDAO.Instance.RemoveNewsArticle(articleId);

        public void RemoveTagsByArticleId(string articleId)
            => NewsArticleDAO.Instance.RemoveTagsByArticleId(articleId);
    }
}
