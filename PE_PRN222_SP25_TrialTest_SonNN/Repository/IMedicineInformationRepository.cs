using Sp25PharmaceuticalDB_BusinessObjects;

namespace Sp25PharmaceuticalDB_Repository
{
    public interface IMedicineInformationRepository
    {
        // Get MedicineInformation by Id
        public MedicineInformation GetMedicineInformationById(string? medicineId);

        // Get MedicineInformation by Name
        public MedicineInformation GetMedicineInformationByName(string? name);

        // Get all MedicineInformations
        public List<MedicineInformation> GetMedicineInformations();

        // Add a new MedicineInformation
        public void AddMedicineInformation(MedicineInformation medicine);

        // Update an existing MedicineInformation
        public void UpdateMedicineInformation(string medicineId, MedicineInformation medicine);

        // Remove a MedicineInformation by Id
        public void RemoveMedicineInformation(string medicineId);
    }
}
