using Microsoft.EntityFrameworkCore;
using Sp25PharmaceuticalDB_BusinessObjects;

namespace Sp25PharmaceuticalDB_DAO
{
    public class MedicineInformationDAO
    {
        private Sp25PharmaceuticalDbContext _dbContext;
        private static MedicineInformationDAO instance;

        public MedicineInformationDAO()
        {
            _dbContext = new Sp25PharmaceuticalDbContext();
        }

        public static MedicineInformationDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineInformationDAO();
                }
                return instance;
            }
        }

        private Sp25PharmaceuticalDbContext CreateDbContext()
        {
            return new Sp25PharmaceuticalDbContext();
        }

        // Get MedicineInformation by Id
        public MedicineInformation GetMedicineInformationById(string? medicineId)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.MedicineInformations.AsNoTracking()
                    .Include(m => m.Manufacturer)
                    .SingleOrDefault(m => m.MedicineId.Equals(medicineId));
            }
        }

        // Get MedicineInformation by Name
        public MedicineInformation GetMedicineInformationByName(string? name)
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.MedicineInformations.AsNoTracking()
                    .Include(m => m.Manufacturer)
                    .SingleOrDefault(m => m.MedicineName.Equals(name));
            }
        }

        // Get all MedicineInformations
        public List<MedicineInformation> GetMedicineInformations()
        {
            using (var dbContext = CreateDbContext())
            {
                return dbContext.MedicineInformations.AsNoTracking()
                    .Include(m => m.Manufacturer)
                    .ToList();
            }
        }

        // Add a new MedicineInformation
        public void AddMedicineInformation(MedicineInformation medicine)
        {
            using (var dbContext = CreateDbContext())
            {
                MedicineInformation currentMedicine = GetMedicineInformationByName(medicine.MedicineName);
                if (currentMedicine == null)
                {
                    dbContext.MedicineInformations.Add(medicine);
                    dbContext.SaveChanges();
                }
            }
        }

        // Update an existing MedicineInformation
        public void UpdateMedicineInformation(string medicineId, MedicineInformation medicine)
        {
            using (var dbContext = CreateDbContext())
            {
                MedicineInformation currentMedicine = GetMedicineInformationById(medicineId);
                if (currentMedicine != null)
                {
                    dbContext.MedicineInformations.Update(medicine);
                    dbContext.SaveChanges();
                }
            }
        }

        // Remove a MedicineInformation by Id
        public void RemoveMedicineInformation(string medicineId)
        {
            using (var dbContext = CreateDbContext())
            {
                MedicineInformation currentMedicine = GetMedicineInformationById(medicineId);
                if (currentMedicine != null)
                {
                    dbContext.MedicineInformations.Remove(currentMedicine);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
