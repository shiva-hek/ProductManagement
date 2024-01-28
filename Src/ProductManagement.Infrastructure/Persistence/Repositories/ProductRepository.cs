using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Aggregates.Products;
using ProductManagement.Domain.Aggregates.Products.ValueObjects;

namespace ProductManagement.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Get(ProduceDate produceDate, ManufactureEmail manufactureEmail)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProduceDate == produceDate && p.ManufactureEmail == manufactureEmail);
        }

        public async Task<Product> GetAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task InsertAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Product entity)
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

}
