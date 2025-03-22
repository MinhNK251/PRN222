using BusinessObjectLayer;
using DataAccessLayer;

namespace RepositoryLayer
{
    public class MedicineInformationRepository : IMedicineInformationRepository
    {
        public MedicineInformation GetMedicineInformationById(string? ManufacturerId)
            => MedicineInformationDAO.Instance.GetMedicineInformationById(ManufacturerId);

        // Get MedicineInformation by Name
        public MedicineInformation GetMedicineInformationByName(string? name)
            => MedicineInformationDAO.Instance.GetMedicineInformationByName(name);

        // Get all MedicineInformations
        public List<MedicineInformation> GetMedicineInformations()
            => MedicineInformationDAO.Instance.GetMedicineInformations();

        // Add a new MedicineInformation
        public void AddMedicineInformation(MedicineInformation medicine)
            => MedicineInformationDAO.Instance.AddMedicineInformation(medicine);

        // Update an existing MedicineInformation
        public void UpdateMedicineInformation(string medicineId, MedicineInformation medicine)
            => MedicineInformationDAO.Instance.UpdateMedicineInformation(medicineId, medicine);

        // Remove a MedicineInformation by Id
        public void RemoveMedicineInformation(string medicineId)
            => MedicineInformationDAO.Instance.RemoveMedicineInformation(medicineId);
    }
}
