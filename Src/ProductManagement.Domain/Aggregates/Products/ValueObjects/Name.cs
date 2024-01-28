using ProductManagement.Domain.SharedKernel;

namespace ProductManagement.Domain.Aggregates.Products.ValueObjects
{
    public class Name : ValueObject<Name>
    {
        private Name()
        {

        }
        
        public Name(string name)
        {
            AssertionConcern.AssertArgumentNotEmpty(name, $"The {nameof(name)} must be provided.");
            AssertionConcern.AssertArgumentLength(name, 30, $"The {nameof(name)} length must be 50 characters or less.");

            Value = name;
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
