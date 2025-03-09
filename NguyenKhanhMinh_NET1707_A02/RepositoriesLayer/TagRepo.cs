using BusinessObjectsLayer.Models;
using DAOsLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoriesLayer
{
    public class TagRepo : ITagRepo
    {
        private readonly TagDAO _tagDAO;

        // Constructor with dependency injection
        public TagRepo(TagDAO tagDAO)
        {
            _tagDAO = tagDAO;
        }

        public Task<List<Tag>> GetAllTags()
        {
            return _tagDAO.GetAllTags();
        }

        public Task<Tag> GetTagById(int id)
        {
            return _tagDAO.GetTagById(id);
        }

        public Task AddTag(Tag tag)
        {
            return _tagDAO.AddTag(tag);
        }

        public Task UpdateTag(Tag tag)
        {
            return _tagDAO.UpdateTag(tag);
        }

        public Task DeleteTag(int id)
        {
            return _tagDAO.DeleteTag(id);
        }
    }
}