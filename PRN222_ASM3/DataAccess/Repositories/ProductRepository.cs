using BusinessObject.Entities;
using DataAccess.Data;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly eStoreDuckContext _context;
        private readonly DbSet<Product> _dbSet;

        public ProductRepository(eStoreDuckContext context)
        {
            _context = context;
            _dbSet = context.Set<Product>();
        }

        public async Task<List<Product>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<Product> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        public async Task AddAsync(Product product)
        {
            await _dbSet.AddAsync(product); await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var existingProduct = await _dbSet.FirstOrDefaultAsync(p => p.ProductId == product.ProductId);
            if (existingProduct is not null)
            {
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.ProductName = product.ProductName;
                existingProduct.Weight = product.Weight;
                existingProduct.UnitPrice = product.UnitPrice;
                existingProduct.UnitsInStock = product.UnitsInStock;

                await _context.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _dbSet.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAllWithCategoryAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }
    }
}