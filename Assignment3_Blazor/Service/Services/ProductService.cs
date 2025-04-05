
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Models;
using Service.DTO;
using Service.Interface;

namespace Service.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductDTO>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            return products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.CategoryName,
                ProductName = p.ProductName,
                Weight = p.Weight,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock
            }).ToList();
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found");

            return new ProductDTO
            {
                ProductId = product.ProductId,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.CategoryName,
                ProductName = product.ProductName,
                Weight = product.Weight,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock
            };
        }

        public async Task<ProductDTO> CreateProductAsync(ProductDTO productDTO)
        {
            // Validate if category exists
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == productDTO.CategoryId);
            if (!categoryExists)
                throw new KeyNotFoundException($"Category with ID {productDTO.CategoryId} not found");

            var product = new Product
            {
                CategoryId = productDTO.CategoryId,
                ProductName = productDTO.ProductName,
                Weight = productDTO.Weight,
                UnitPrice = productDTO.UnitPrice,
                UnitsInStock = productDTO.UnitsInStock
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            productDTO.ProductId = product.ProductId;
            return productDTO;
        }

        public async Task<ProductDTO> UpdateProductAsync(ProductDTO productDTO)
        {
            // Validate if category exists
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == productDTO.CategoryId);
            if (!categoryExists)
                throw new KeyNotFoundException($"Category with ID {productDTO.CategoryId} not found");

            var product = await _context.Products.FindAsync(productDTO.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {productDTO.ProductId} not found");

            product.CategoryId = productDTO.CategoryId;
            product.ProductName = productDTO.ProductName;
            product.Weight = productDTO.Weight;
            product.UnitPrice = productDTO.UnitPrice;
            product.UnitsInStock = productDTO.UnitsInStock;

            await _context.SaveChangesAsync();
            return productDTO;
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} not found");

            // Check if product has order details
            var hasOrderDetails = await _context.OrderDetails.AnyAsync(od => od.ProductId == id);
            if (hasOrderDetails)
                throw new InvalidOperationException("Cannot delete product because it has associated orders");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}