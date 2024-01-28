using ProductManagement.Domain.Aggregates.Products.ValueObjects;
using ProductManagement.Domain.Services.Products;
using ProductManagement.Domain.SharedKernel;

namespace ProductManagement.Domain.Aggregates.Products.Rules
{
    public class ProductMustBeUniqueRule : IRule
    {
        private readonly ProduceDate _produceDate;
        private readonly ManufactureEmail _manufactureEmail;
        private readonly IProductUniquenessChecker _productUniquenessChecker;

        public ProductMustBeUniqueRule(ProduceDate produceDate, ManufactureEmail manufactureEmail, IProductUniquenessChecker productUniquenessChecker)
        {
            AssertionConcern.AssertArgumentNotNull(produceDate, $"The {nameof(produceDate)} must be provided.");
            AssertionConcern.AssertArgumentNotNull(manufactureEmail, $"The {nameof(manufactureEmail)} must be provided.");
            AssertionConcern.AssertArgumentNotNull(productUniquenessChecker, $"The {nameof(productUniquenessChecker)} must be provided.");

            this._produceDate = produceDate;
            this._manufactureEmail = manufactureEmail;
            this._productUniquenessChecker = productUniquenessChecker;
        }

        public void Assert()
        {
            Assert($@"The product with this produce date:  ""{_produceDate}"" and manufacture email: ""{_manufactureEmail}"" already exists.");
        }

        public void Assert(string message)
        {
            if (!_productUniquenessChecker.IsUnique(_produceDate, _manufactureEmail))
                throw new BusinessRuleViolationException(message ?? "");
        }
    }
}
