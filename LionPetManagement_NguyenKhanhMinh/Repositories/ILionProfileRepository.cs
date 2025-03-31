using BOs.Models;

namespace Repositories
{
    public interface ILionProfileRepository
    {
        // Get LionProfile by Id
        public LionProfile GetLionProfileById(int? medicineId);

        // Get LionProfile by Name
        public LionProfile GetLionProfileByName(string? name);

        // Get all LionProfiles
        public List<LionProfile> GetLionProfiles();

        // Add a new LionProfile
        public void AddLionProfile(LionProfile lionProfile);

        // Update an existing LionProfile
        public void UpdateLionProfile(int id, LionProfile LionProfile);

        // Remove a LionProfile by Id
        public void RemoveLionProfile(int id);
    }
}
