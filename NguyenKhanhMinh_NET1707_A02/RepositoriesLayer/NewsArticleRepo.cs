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
        private readonly NewsArticleDAO _newsArticleDAO;

        // Constructor with dependency injection
        public NewsArticleRepo(NewsArticleDAO newsArticleDAO)
        {
            _newsArticleDAO = newsArticleDAO;
        }

        public Task<List<NewsArticle>> GetActiveNewsArticles()
        {
            return _newsArticleDAO.GetActiveNewsArticles();
        }

        public Task<List<NewsArticle>> GetAllNewsArticles()
        {
            return _newsArticleDAO.GetAllNewsArticles();
        }

        public Task<List<NewsArticle>> SearchNewsArticles(string searchTerm)
        {
            return _newsArticleDAO.SearchNewsArticles(searchTerm);
        }

        public Task<NewsArticle> GetNewsArticleById(int id)
        {
            return _newsArticleDAO.GetNewsArticleById(id);
        }

        public Task<List<NewsArticle>> GenerateReport(DateTime startDate, DateTime endDate)
        {
            return _newsArticleDAO.GenerateReport(startDate, endDate);
        }

        public Task AddNewsArticle(NewsArticleDTO newsArticleDTO, int createdBy, int updatedBy, int[] selectedTagIds)
        {
            return _newsArticleDAO.AddNewsArticle(newsArticleDTO, createdBy, updatedBy, selectedTagIds);
        }

        public Task UpdateNewsArticle(NewsArticleDTO newsArticleDTO, int createdBy, int updatedBy, int[] selectedTagIds)
        {
            return _newsArticleDAO.UpdateNewsArticle(newsArticleDTO, createdBy, updatedBy, selectedTagIds);
        }

        public Task DeleteNewsArticle(int id)
        {
            return _newsArticleDAO.DeleteNewsArticle(id);
        }

        public Task<List<NewsArticle>> GetNewsByCreator(int creatorId)
        {
            return _newsArticleDAO.GetNewsByCreator(creatorId);
        }
    }
}
