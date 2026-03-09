
using Microsoft.EntityFrameworkCore;
using GameRev.Data;

namespace GameRev.Repository.Generic;

public class GenericCrudRepository<T> : IGenericCrudRepository<T> where T : class
{

    protected readonly AppDbContext context;

    public GenericCrudRepository(AppDbContext context)
    {
        this.context = context;
    }

    public async Task<T?> AddAsync(T entity, CancellationToken ct)
    {
        await context.Set<T>().AddAsync(entity,ct);
        bool success = await context.SaveChangesAsync(ct) > 0;
        return success ? entity : null;
    }

    public async Task<bool> DeleteAsync(T entity, CancellationToken ct)
    {
        context.Set<T>().Remove(entity);
        return await context.SaveChangesAsync(ct) > 0;
    }

    public async Task<List<T>> GetAllAsync(CancellationToken ct)
    {
        return await context.Set<T>().ToListAsync(ct);
    }

    public async Task<T?> GetByIdAsync(long id, CancellationToken ct)
    {
        return await context.Set<T>().FindAsync([id, ct], cancellationToken: ct);   
    }

    public async Task<bool> UpdateAsync(T entity, CancellationToken ct)
    {
        context.Update(entity);
        return await context.SaveChangesAsync(ct) > 0;
    }
}
