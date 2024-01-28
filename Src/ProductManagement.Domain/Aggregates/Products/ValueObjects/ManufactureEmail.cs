using ProductManagement.Domain.SharedKernel;
using ProductManagement.Domain.Tools;

namespace ProductManagement.Domain.Aggregates.Products.ValueObjects
{
    public class ManufactureEmail : ValueObject<ManufactureEmail>
    {
        public ManufactureEmail()
        {

        }
        public ManufactureEmail(string manufactureEmail)
        {
            AssertionConcern.AssertArgumentNotEmpty(manufactureEmail, $"The {nameof(manufactureEmail)} must be provided.");
            AssertionConcern.AssertArgumentLength(manufactureEmail, 50, $"The {nameof(manufactureEmail)} length must be 50 characters or less.");
            AssertionConcern.AssertArgumentMatches(manufactureEmail.ToLower(), RegExHelper.Patterns.Email, $"The {nameof(manufactureEmail)} format is invalid.");

            Value = manufactureEmail.Trim().ToLower();
        }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
