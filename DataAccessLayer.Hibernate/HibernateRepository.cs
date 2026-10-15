using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;

namespace DataAccessLayer.Hibernate
{
    public class HibernateRepository<TKey> : IHibernateRepository<TKey>
        where TKey : struct
    {
        private bool _disposed;
        private IHibernateUnitOfWork? _unitOfWork;
        private readonly IStatelessSession _session;

        public HibernateRepository(IStatelessSession session)
        {
            _session = session;
        }

        private IQueryable<TEntity> GetQuery<TEntity>() where TEntity : class, IEntity<TKey>
        {
            return _session.Query<TEntity>();
        }

        public IQueryable<TEntity> GetQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, IEntity<TKey>
        {
            return GetQuery<TEntity>().Where(predicate);
        }

        public IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class, IEntity<TKey>
        {
            return GetQuery<TEntity>().AsQueryable();
        }

        public TEntity Single<TEntity>(Expression<Func<TEntity, bool>> criteria)
            where TEntity : class, IEntity<TKey>
        {
            return GetQuery<TEntity>().Single(criteria);
        }

        public TEntity? SingleOrDefault<TEntity>(Expression<Func<TEntity, bool>> criteria)
            where TEntity : class, IEntity<TKey>
        {
            return GetQuery<TEntity>().SingleOrDefault(criteria);
        }

        public Task<List<TEntity>> GetAllAsync<TEntity>() where TEntity : class, IEntity<TKey>
        {
            return GetQuery<TEntity>().ToListAsync();
        }
        
        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : class, IEntity<TKey>
        {
            return GetQuery<TEntity>().AsEnumerable();
        }

        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> criteria)
            where TEntity : class, IEntity<TKey>
        {
            return GetQuery<TEntity>().Where(criteria);
        }

        public TEntity? FindOne<TEntity>(Expression<Func<TEntity, bool>> criteria)
            where TEntity : class, IEntity<TKey>
        {
            return GetQuery<TEntity>().Where(criteria).FirstOrDefault();
        }

        public virtual void Dispose()
        {
            if (!_disposed)
            {
                if (_unitOfWork != null)
                    _unitOfWork.Dispose();

                _disposed = true;
            }
        }

        public async Task Save<TEntity>(TEntity entity) where TEntity : class, IEntity<TKey>
        {
            await _session.InsertAsync(entity);
        }

        public IHibernateUnitOfWork UnitOfWork
        {
            get { return _unitOfWork ??= new HibernateUnitOfWork(_session); }
        }
    }
}
