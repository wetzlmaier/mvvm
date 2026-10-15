namespace DataAccessLayer.Hibernate
{
    public interface IHibernateRepository<in TKey> : IDisposable, IRepository<TKey>
        where TKey : struct
    {
        Task Save<TEntity>(TEntity entity) where TEntity : class, IEntity<TKey>;

        IHibernateUnitOfWork UnitOfWork { get; }
    }
}
