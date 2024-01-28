using ProductManagement.Domain.SharedKernel;

namespace ProductManagement.Domain.Aggregates.Products.ValueObjects
{
    public class ProduceDate : ValueObject<ProduceDate>
    {
        private ProduceDate()
        {

        }

        public ProduceDate(DateTime produceDate)
        {
            AssertionConcern.AssertArgumentIsTrue(produceDate < DateTime.Now, $"The {nameof(produceDate)} must be less than current date.");
            Value = produceDate;
        }

        public DateTime Value { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
