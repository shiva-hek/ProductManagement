namespace ProductManagement.Domain.SharedKernel
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task InsertAsync(TEntity entity,CancellationToken cancellationToken = default);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

        Task RemoveAsync(Guid id, CancellationToken cancellationToken = default);

        Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
