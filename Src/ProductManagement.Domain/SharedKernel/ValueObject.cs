namespace ProductManagement.Domain.SharedKernel
{
    public abstract class ValueObject<T>
       where T : ValueObject<T>
    {
        protected abstract IEnumerable<object> GetAtomicValues();

        public bool Equals(T other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
        }

        public override bool Equals(object other)
        {
            return Equals(other as T);
        }

        public override int GetHashCode()
        {
            return GetAtomicValues().Aggregate(1, (current, obj) =>
            {
                unchecked
                {
                    return current * 23 + (obj?.GetHashCode() ?? 0);
                }
            });
        }

        public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            else
                return left.Equals(right);
        }

        public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
        {
            return !(left == right);
        }
    }
}
