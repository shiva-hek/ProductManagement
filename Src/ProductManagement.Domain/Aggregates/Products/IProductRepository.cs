using ProductManagement.Domain.Aggregates.Products.ValueObjects;
using ProductManagement.Domain.SharedKernel;

namespace ProductManagement.Domain.Aggregates.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetAsync(ProduceDate produceDate , ManufactureEmail manufactureEmail, CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllAsync(string name = null, CancellationToken cancellationToken = default);
    }
}
