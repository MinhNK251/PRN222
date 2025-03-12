using BusinessObjectsLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOsLayer
{
    public class TagDAO
    {
        private FunewsManagementContext _dbContext;
        private static TagDAO? instance;

        private TagDAO()
        {
            _dbContext = new FunewsManagementContext();
        }

        public static TagDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TagDAO();
                }
                return instance;
            }
        }

        private FunewsManagementContext CreateDbContext()
        {
            return new FunewsManagementContext();
        }

        // Get all tags
        public List<Tag> GetTags()
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.Tags.AsNoTracking()
                    .Include(t => t.NewsArticles)
                    .ToList();
            }
        }

        // Get tag by ID
        public Tag? GetTagById(int tagId)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.Tags.AsNoTracking()
                    .Include(t => t.NewsArticles)
                    .SingleOrDefault(t => t.TagId == tagId);
            }
        }

        // Get tags by a list of IDs
        public List<Tag> GetTagsByIds(List<int> tagIds)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.Tags.AsNoTracking().Where(t => tagIds.Contains(t.TagId)).ToList();
            }
        }

        // Get tags by article ID
        public List<Tag> GetTagsByNewsArticleId(string newsArticleId)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.Tags.AsNoTracking()
                    .Where(t => dbContext.Set<Dictionary<string, object>>("NewsTag")
                        .Any(nt => EF.Property<int>(nt, "TagId") == t.TagId &&
                                   EF.Property<string>(nt, "NewsArticleId") == newsArticleId))
                    .ToList();
            }
        }

        // Add a new tag
        public void AddTag(Tag tag)
        {
            using (var dbContext = CreateDbContext())
            {
                dbContext.Tags.Add(tag);
                dbContext.SaveChanges();
            }
        }

        // Update an existing tag
        public void UpdateTag(Tag tag)
        {
            using (var dbContext = CreateDbContext())
            {
                dbContext.Tags.Update(tag);
                dbContext.SaveChanges();
            }
        }

        // Delete a tag (only if not linked to any news articles)
        public void RemoveTag(int tagId)
        {
            using (var dbContext = CreateDbContext())
            {
                var tag = dbContext.Tags.Include(t => t.NewsArticles).FirstOrDefault(t => t.TagId == tagId);
                if (tag != null && !tag.NewsArticles.Any())
                {
                    dbContext.Tags.Remove(tag);
                    dbContext.SaveChanges();
                }
            }
        }

        // Remove NewsArticle Tags By NewsArticle Id
        public void RemoveArticlesByTagId(int tagId)
        {
            using (var dbContext = new FunewsManagementContext())
            {
                var tag = dbContext.Tags
                    .Include(a => a.NewsArticles)
                    .FirstOrDefault(a => a.TagId == tagId);
                if (tag != null)
                {
                    tag.NewsArticles.Clear();
                    dbContext.SaveChanges();
                }
            }
        }
    }
}