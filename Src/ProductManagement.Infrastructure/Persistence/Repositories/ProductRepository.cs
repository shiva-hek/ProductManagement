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

        public async Task<Product> GetAsync(ProduceDate produceDate, ManufactureEmail manufactureEmail, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.ProduceDate.Value == produceDate.Value && p.ManufactureEmail.Value == manufactureEmail.Value);
        }

        public async Task<Product> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task InsertAsync(Product entity, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Product entity, CancellationToken cancellationToken = default)
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllAsync(string name = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Product> query = _context.Products.Include(p => p.Creator);

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Creator.FirstName.Contains(name) || p.Creator.LastName.Contains(name));
            }

            return await query.ToListAsync();
        }
    }

}
