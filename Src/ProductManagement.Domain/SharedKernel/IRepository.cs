namespace ProductManagement.Domain.SharedKernel
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task RemoveAsync(Guid id);

        Task<TEntity> GetAsync(Guid id);
    }
}
