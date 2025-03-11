using BusinessObjectsLayer.Models;
using DAOsLayer;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoriesLayer
{
    public class TagRepo : ITagRepo
    {
        public List<Tag> GetTags()
            => TagDAO.Instance.GetTags();

        public Tag? GetTagById(int tagId)
            => TagDAO.Instance.GetTagById(tagId);

        public List<Tag> GetTagsByIds(List<int> tagIds)
            => TagDAO.Instance.GetTagsByIds(tagIds);

        public List<Tag> GetTagsByNewsArticleIdAsync(string newsArticleId)
            => TagDAO.Instance.GetTagsByNewsArticleIdAsync(newsArticleId);

        public void AddTag(Tag tag)
            => TagDAO.Instance.AddTag(tag);

        public void UpdateTag(Tag tag)
            => TagDAO.Instance.UpdateTag(tag);

        public void RemoveTag(int tagId)
            => TagDAO.Instance.RemoveTag(tagId);
    }
}