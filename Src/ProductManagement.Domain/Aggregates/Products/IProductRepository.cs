using ProductManagement.Domain.Aggregates.Products.ValueObjects;
using ProductManagement.Domain.SharedKernel;

namespace ProductManagement.Domain.Aggregates.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> Get(ProduceDate produceDate , ManufactureEmail manufactureEmail);
    }
}
