using ProductManagement.Domain.Aggregates.Accounts;
using ProductManagement.Domain.Aggregates.Products.ValueObjects;
using ProductManagement.Domain.Services.Products;

namespace ProductManagement.Domain.Aggregates.Products.Factories
{
    public class ProductFactory
    {
        private readonly IProductUniquenessChecker _productUniquenessChecker;

        public ProductFactory(IProductUniquenessChecker productUniquenessChecker)
        {
            this._productUniquenessChecker = productUniquenessChecker;
        }

        public Product Create(
            string name,
            DateTime produceDate,
            string manufacturePhone,
            string manufactureEmail,
            bool isAvailable,
            string creatorId)
        {
            var id = Guid.NewGuid();
            var productName = new Name(name);
            var productProduceDate = new ProduceDate(produceDate);
            var productManufacturePhone = new ManufacturePhone(manufacturePhone);
            var productManufactureEmail = new ManufactureEmail(manufactureEmail);

            return new Product(
                id,
                productName,
                productProduceDate,
                productManufacturePhone,
                productManufactureEmail,
                isAvailable,
                creatorId,
                _productUniquenessChecker);
        }
    }
}
