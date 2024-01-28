using ProductManagement.Domain.Aggregates.Products.ValueObjects;
using ProductManagement.Domain.SharedKernel;

namespace ProductManagement.Domain.Services.Products
{
    public interface IProductUniquenessChecker : IDomainService
    {
        bool IsUnique(ProduceDate produceDate, ManufactureEmail manufactureEmail);
    }
}
