using BusinessObjectsLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoriesLayer
{
    public interface ITagRepo
    {
        Task<List<Tag>> GetTags();
        Task<Tag> GetTagById(int id);
        Task<List<Tag>> GetTagsByIds(List<int> tagIds);
        Task<List<Tag>> GetTagsByNewsArticleIdAsync(string newsArticleId);
        Task AddTag(Tag tag);
        Task UpdateTag(Tag tag);
        Task RemoveTag(int id);
    }
}