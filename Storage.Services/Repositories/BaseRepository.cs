using Infrastructure.Data.Storage;
using Microsoft.EntityFrameworkCore;
using Storage.Data;
using System.Linq.Expressions;

namespace Storage.Services.Repositories;

public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
{
    readonly ApplicationDbContext _context;

    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public TEntity Add(TEntity entity)
    {
        return _context.Set<TEntity>().Add(entity).Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, string cacheKey)
    {
        if (entity == null)
            throw new ArgumentNullException("entity");

        _context.Update(entity);
        return entity;
    }

    public bool Any(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().Any(predicate);
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AnyAsync(predicate);
    }
    public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().Where(predicate).ExecuteDeleteAsync();
    }

    public async Task<TEntity> GetByIdAsync(TKey key)
    {
        return await _context.Set<TEntity>().FindAsync(key);
    }

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().Where(predicate);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
    }
}

