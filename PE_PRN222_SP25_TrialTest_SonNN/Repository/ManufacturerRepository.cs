using Sp25PharmaceuticalDB_BusinessObjects;
using Sp25PharmaceuticalDB_DAO;

namespace Sp25PharmaceuticalDB_Repository
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        public List<Manufacturer> GetManufacturers()
            => ManufacturerDAO.Instance.GetManufacturers();
    }
}
