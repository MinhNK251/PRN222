using BOs;
using DAOs;

namespace Repos
{
    public class SupplierRepo : ISupplierRepo
    {
        public async Task<List<SupplierCompany>> GetList()
        {
            return await OilPaintingArtDAO.Instance.GetSuppilers();
        }
    }
}
