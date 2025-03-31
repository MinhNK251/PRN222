using BOs.Models;
using DAOs;

namespace Repositories
{
    public class LionProfileRepository : ILionProfileRepository
    {
        public LionProfile GetLionProfileById(int? id)
            => LionProfileDAO.Instance.GetLionProfileById(id);

        // Get LionProfile by Name
        public LionProfile GetLionProfileByName(string? name)
            => LionProfileDAO.Instance.GetLionProfileByName(name);

        // Get all LionProfiles
        public List<LionProfile> GetLionProfiles()
            => LionProfileDAO.Instance.GetLionProfiles();

        // Add a new LionProfile
        public void AddLionProfile(LionProfile lionProfile)
            => LionProfileDAO.Instance.AddLionProfile(lionProfile);

        // Update an existing LionProfile
        public void UpdateLionProfile(int id, LionProfile lionProfile)
            => LionProfileDAO.Instance.UpdateLionProfile(id, lionProfile);

        // Remove a LionProfile by Id
        public void RemoveLionProfile(int id)
            => LionProfileDAO.Instance.RemoveLionProfile(id);
    }
}
