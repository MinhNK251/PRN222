using BOs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class LionProfileDAO
    {
        private Sp25lionDbContext _dbContext;
        private static LionProfileDAO instance;

        public LionProfileDAO()
        {
            _dbContext = new Sp25lionDbContext();
        }

        public static LionProfileDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LionProfileDAO();
                }
                return instance;
            }
        }

        private Sp25lionDbContext CreateDbContext()
        {
            return new Sp25lionDbContext();
        }

        // Get LionProfile by Id
        public LionProfile GetLionProfileById(int? lionProfileId)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.LionProfiles.AsNoTracking()
                    .Include(m => m.LionType)
                    .SingleOrDefault(m => m.LionProfileId == lionProfileId);
            }
        }

        // Get LionProfile by Name
        public LionProfile GetLionProfileByName(string? name)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.LionProfiles.AsNoTracking()
                    .Include(m => m.LionType)
                    .SingleOrDefault(m => m.LionName.Equals(name));
            }
        }

        // Get all LionProfiles
        public List<LionProfile> GetLionProfiles()
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.LionProfiles.AsNoTracking()
                    .Include(m => m.LionType)
                    .OrderByDescending(m => m.LionProfileId)
                    .ToList();
            }
        }

        // Add a new LionProfile
        public void AddLionProfile(LionProfile lionProfile)
        {
            using (var dbContext = CreateDbContext())
            {
                LionProfile currentLion = GetLionProfileByName(lionProfile.LionName);
                if (currentLion == null)
                {
                    dbContext.LionProfiles.Add(lionProfile);
                    dbContext.SaveChanges();
                }
            }
        }

        // Update an existing LionProfile
        public void UpdateLionProfile(int id, LionProfile lionProfile)
        {
            using (var dbContext = CreateDbContext())
            {
                LionProfile currentLion = GetLionProfileById(id);
                if (currentLion != null)
                {
                    dbContext.LionProfiles.Update(lionProfile);
                    dbContext.SaveChanges();
                }
            }
        }

        // Remove a LionProfile by Id
        public void RemoveLionProfile(int id)
        {
            using (var dbContext = CreateDbContext())
            {
                LionProfile currentLion = GetLionProfileById(id);
                if (currentLion != null)
                {
                    dbContext.LionProfiles.Remove(currentLion);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
