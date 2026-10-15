using System.Linq.Expressions;

namespace DataAccessLayer
{
    public interface IRepository<in TKey>
        where TKey : struct
    {
        IQueryable<TEntity> GetQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IEntity<TKey>;

        IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class, IEntity<TKey>;

        TEntity Single<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class, IEntity<TKey>;

        TEntity? SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class, IEntity<TKey>;

        IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class, IEntity<TKey>;

        TEntity? FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class, IEntity<TKey>;

        IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity<TKey>;
    }
}
