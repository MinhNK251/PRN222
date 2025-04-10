using BusinessObject.Entities;
using DataAccess.DTO;

namespace Service.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsWithCategoryAsync();

        Task<List<LowStockAlertDTO>> GetLowStockAlertsAsync();

        Task<List<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int id);

        Task AddProductAsync(Product product);

        Task UpdateProductAsync(Product product);

        Task DeleteProductAsync(int id);
    }
}