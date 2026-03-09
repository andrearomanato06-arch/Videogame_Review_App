namespace GameRev.Repository.Generic;

using GameRev.Data;

public interface IGenericCrudRepository<T>  where T : class
{
    Task<T?> AddAsync(T entity, CancellationToken ct);

    Task<bool> UpdateAsync(T entity, CancellationToken ct);

    Task<bool> DeleteAsync(T entity, CancellationToken ct);

    Task<T?> GetByIdAsync(long id, CancellationToken ct);

    Task<List<T>> GetAllAsync (CancellationToken ct);
}