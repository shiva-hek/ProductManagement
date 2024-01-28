using ProductManagement.Domain.Aggregates.Products;
using ProductManagement.Domain.Aggregates.Products.ValueObjects;

namespace ProductManagement.Domain.Services.Products
{
    public class ProductUniquenessChecker : IProductUniquenessChecker
    {
        private readonly IProductRepository _productRepository;

        public ProductUniquenessChecker(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public bool IsUnique(ProduceDate produceDate, ManufactureEmail manufactureEmail)
        {
            return _productRepository.Get(produceDate, manufactureEmail) == null;
        }
    }
}
