using BusinessObjectsLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoriesLayer
{
    public interface ITagRepo
    {
        List<Tag> GetTags();
        Tag GetTagById(int id);
        List<Tag> GetTagsByIds(List<int> tagIds);
        List<Tag> GetTagsByNewsArticleIdAsync(string newsArticleId);
        void AddTag(Tag tag);
        void UpdateTag(Tag tag);
        void RemoveTag(int id);
    }
}