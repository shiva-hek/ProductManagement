namespace ProductManagement.Domain.SharedKernel
{
    public interface IRule
    {
        void Assert();
        void Assert(string message);
    }
}
