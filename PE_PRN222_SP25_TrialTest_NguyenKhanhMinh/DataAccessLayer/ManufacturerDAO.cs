using BusinessObjectLayer;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ManufacturerDAO
    {
        private Sp25PharmaceuticalDbContext _dbContext;
        private static ManufacturerDAO instance;

        public ManufacturerDAO()
        {
            _dbContext = new Sp25PharmaceuticalDbContext();
        }

        public static ManufacturerDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManufacturerDAO();
                }
                return instance;
            }
        }

        private Sp25PharmaceuticalDbContext CreateDbContext()
        {
            return new Sp25PharmaceuticalDbContext();
        }

        public List<Manufacturer> GetManufacturers()
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.Manufacturers.AsNoTracking().Include(manufacturer => manufacturer.MedicineInformations).ToList();
            }
        }
    }
}
