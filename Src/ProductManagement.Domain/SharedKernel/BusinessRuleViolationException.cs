namespace ProductManagement.Domain.SharedKernel
{
    public class BusinessRuleViolationException : Exception
    {
        public BusinessRuleViolationException(string message)
            : base(message)
        {
            Source = GetType().Name;
        }
    }
}
