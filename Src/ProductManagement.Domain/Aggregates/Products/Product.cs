using ProductManagement.Domain.Aggregates.Accounts;
using ProductManagement.Domain.Aggregates.Products.Rules;
using ProductManagement.Domain.Aggregates.Products.ValueObjects;
using ProductManagement.Domain.Services.Products;
using ProductManagement.Domain.SharedKernel;

namespace ProductManagement.Domain.Aggregates.Products
{
    public class Product : Entity<Guid>, IAggregateRoot
    {
        public Name Name { get; private set; }
        public ProduceDate ProduceDate { get; private set; }
        public ManufacturePhone ManufacturePhone { get; private set; }
        public ManufactureEmail ManufactureEmail { get; private set; }
        public bool IsAvailable { get; private set; }
        public string CreatorId { get; private set; }
        public User Creator { get; private set; }
        
        private Product() { }

        public Product(
            Guid id,
            Name name,
            ProduceDate produceDate,
            ManufacturePhone manufacturePhone,
            ManufactureEmail manufactureEmail,
            bool isAvailable,
            string creatorId,
            User creator,
            IProductUniquenessChecker productUniquenessChecker)
        {
            AssertionConcern.AssertArgumentNotNull(name, $"The {nameof(name)} must be provided.");
            AssertionConcern.AssertArgumentNotNull(produceDate, $"The {nameof(produceDate)} must be provided.");
            AssertionConcern.AssertArgumentNotNull(manufacturePhone, $"The {nameof(manufacturePhone)} must be provided.");
            AssertionConcern.AssertArgumentNotNull(manufactureEmail, $"The {nameof(manufactureEmail)} must be provided.");
            AssertionConcern.AssertArgumentNotNull(creatorId, $"The {nameof(creatorId)} must be provided.");
            AssertionConcern.AssertRuleNotBroken(new ProductMustBeUniqueRule(produceDate, manufactureEmail, productUniquenessChecker));

            Id = id;
            Name = name;
            ProduceDate = produceDate;
            ManufacturePhone = manufacturePhone;
            ManufactureEmail = manufactureEmail;
            IsAvailable = isAvailable;
            CreatorId = creatorId;
            Creator = creator;
        }

        public void SetProduceDateAndManufactureEmail(DateTime produceDate,string manufactureEmail, IProductUniquenessChecker productUniquenessChecker)
        {
            var newProduceDate = new ProduceDate(produceDate);
            var newManufactureEmail = new ManufactureEmail(manufactureEmail);

            if (newProduceDate == ProduceDate && newManufactureEmail == ManufactureEmail)
                return;

            AssertionConcern.AssertRuleNotBroken(new ProductMustBeUniqueRule(newProduceDate, newManufactureEmail, productUniquenessChecker));

            ProduceDate = newProduceDate;
            ManufactureEmail = newManufactureEmail;
        }
    }
}
