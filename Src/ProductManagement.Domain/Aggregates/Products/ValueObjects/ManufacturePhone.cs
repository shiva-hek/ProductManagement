using ProductManagement.Domain.SharedKernel;

namespace ProductManagement.Domain.Aggregates.Products.ValueObjects
{
    public class ManufacturePhone : ValueObject<Name>
    {
        private ManufacturePhone()
        {

        }

        public ManufacturePhone(string manufacturePhone)
        {
            AssertionConcern.AssertArgumentMatches(manufacturePhone, @"^[1-9]\d{2,12}$", $"The format of {nameof(manufacturePhone)} is incorrect.");
            Value = manufacturePhone;
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
