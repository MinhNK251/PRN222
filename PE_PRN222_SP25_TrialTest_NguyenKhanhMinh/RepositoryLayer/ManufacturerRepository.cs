using BusinessObjectLayer;
using DataAccessLayer;

namespace RepositoryLayer
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        public List<Manufacturer> GetManufacturers()
            => ManufacturerDAO.Instance.GetManufacturers();
    }
}
