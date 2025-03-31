using BOs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class LionTypeDAO
    {
        private Sp25lionDbContext _dbContext;
        private static LionTypeDAO instance;

        public LionTypeDAO()
        {
            _dbContext = new Sp25lionDbContext();
        }

        public static LionTypeDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LionTypeDAO();
                }
                return instance;
            }
        }

        private Sp25lionDbContext CreateDbContext()
        {
            return new Sp25lionDbContext();
        }

        public List<LionType> GetLionTypess()
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.LionTypes.AsNoTracking().Include(l => l.LionProfiles).ToList();
            }
        }
    }
}
