using BOs;

namespace Repos
{
    public interface ISupplierRepo
    {
        Task<List<SupplierCompany>> GetList();
    }
}
