using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjectsLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DAOsLayer
{
    public class TagDAO
    {
        private readonly FunewsManagementContext _context;

        public TagDAO()
        {
            _context = new FunewsManagementContext();
        }

        // Get all tags
        public async Task<List<Tag>> GetAllTags()
        {
            return await _context.Tags.ToListAsync();
        }

        // Get a tag by ID
        public async Task<Tag> GetTagById(int id)
        {
            return await _context.Tags.FindAsync(id);
        }

        // Add a new tag
        public async Task AddTag(Tag tag)
        {
            try
            {
                var existingTag = await GetTagById(tag.TagId);
                if (existingTag != null)
                {
                    throw new Exception("Tag already exists");
                }
                _context.Tags.Add(tag);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Update an existing tag
        public async Task UpdateTag(Tag tag)
        {
            var existingTag = await GetTagById(tag.TagId);
            if (existingTag == null)
            {
                throw new Exception("Tag not found");
            }

            existingTag.TagName = tag.TagName;
            existingTag.Note = tag.Note;

            _context.Tags.Update(existingTag);
            await _context.SaveChangesAsync();
        }

        // Delete a tag
        public async Task DeleteTag(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                throw new Exception("Tag not found");
            }

            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
        }
    }
}