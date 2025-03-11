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
        public async Task<List<Tag>> GetTags()
        {
            using (var dbContext = CreateDbContext())
            {
                return await dbContext.Tags.ToListAsync();
            }
        }

        // Get tag by ID
        public async Task<Tag?> GetTagById(int tagId)
        {
            using (var dbContext = CreateDbContext())
            {
                return await dbContext.Tags.FindAsync(tagId);
            }
        }

        // Get tags by a list of IDs
        public async Task<List<Tag>> GetTagsByIds(List<int> tagIds)
        {
            using (var dbContext = CreateDbContext())
            {
                return await dbContext.Tags.Where(t => tagIds.Contains(t.TagId)).ToListAsync();
            }
        }

        // Add a new tag
        public async Task AddTag(Tag tag)
        {
            using (var dbContext = CreateDbContext())
            {
                dbContext.Tags.Add(tag);
                await dbContext.SaveChangesAsync();
            }
        }

        // Update an existing tag
        public async Task UpdateTag(Tag tag)
        {
            using (var dbContext = CreateDbContext())
            {
                dbContext.Tags.Update(tag);
                await dbContext.SaveChangesAsync();
            }
        }

        // Delete a tag (only if not linked to any news articles)
        public async Task RemoveTag(int tagId)
        {
            using (var dbContext = CreateDbContext())
            {
                var tag = dbContext.Tags.Include(t => t.NewsArticles).FirstOrDefault(t => t.TagId == tagId);
                if (tag != null && !tag.NewsArticles.Any())
                {
                    dbContext.Tags.Remove(tag);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}