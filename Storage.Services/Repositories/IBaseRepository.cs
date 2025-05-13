using Infrastructure.Data.Storage;
using System.Linq.Expressions;

namespace Storage.Services.Repositories
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        TEntity Add(TEntity entity);
        Task<TEntity> GetByIdAsync(TKey key);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> UpdateAsync(TEntity entity, string CacheKey);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }
}
