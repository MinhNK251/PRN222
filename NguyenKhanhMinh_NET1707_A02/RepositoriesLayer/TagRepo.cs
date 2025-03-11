using BusinessObjectsLayer.Models;
using DAOsLayer;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoriesLayer
{
    public class TagRepo : ITagRepo
    {
        public async Task<List<Tag>> GetTags()
            => await TagDAO.Instance.GetTags();

        public async Task<Tag?> GetTagById(int tagId)
            => await TagDAO.Instance.GetTagById(tagId);

        public async Task<List<Tag>> GetTagsByIds(List<int> tagIds)
            => await TagDAO.Instance.GetTagsByIds(tagIds);

        public async Task AddTag(Tag tag)
            => await TagDAO.Instance.AddTag(tag);

        public async Task UpdateTag(Tag tag)
            => await TagDAO.Instance.UpdateTag(tag);

        public async Task RemoveTag(int tagId)
            => await TagDAO.Instance.RemoveTag(tagId);
    }
}