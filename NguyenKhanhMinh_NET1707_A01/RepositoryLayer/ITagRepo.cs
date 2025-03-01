using BusinessObjectsLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public interface ITagRepo
    {
        Task<List<Tag>> GetAllTags();
        Task<Tag> GetTagById(int id);
        Task AddTag(Tag tag);
        Task UpdateTag(Tag tag);
        Task DeleteTag(int id);
    }
}