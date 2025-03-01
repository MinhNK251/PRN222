using BusinessObjectsLayer.DTOs;
using BusinessObjectsLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface INewsArticleRepo
    {
        Task<List<NewsArticle>> GetActiveNewsArticles();
        Task<List<NewsArticle>> GetAllNewsArticles();
        Task<List<NewsArticle>> SearchNewsArticles(string searchTerm);
        Task<NewsArticle> GetNewsArticleById(int id);
        Task<List<NewsArticle>> GenerateReport(DateTime startDate, DateTime endDate);
        Task AddNewsArticle(NewsArticleDTO newsArticleDTO, int createdBy, int updatedBy, int[] selectedTagIds);
        Task UpdateNewsArticle(NewsArticleDTO newsArticleDTO, int createdBy, int updatedBy, int[] selectedTagIds);
        Task DeleteNewsArticle(int id);
        Task<List<NewsArticle>> GetNewsByCreator(int creatorId);
    }
}
