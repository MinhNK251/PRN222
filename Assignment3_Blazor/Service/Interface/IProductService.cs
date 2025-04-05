
using Service.DTO;

namespace Service.Interface
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<ProductDTO> CreateProductAsync(ProductDTO productDTO);
        Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO);
        Task DeleteProductAsync(int id);
    }
}