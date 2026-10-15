namespace DataAccessLayer.Hibernate
{
    public interface IHibernateRepositoryFactory<TKey> : IRepositoryFactory<IHibernateRepository<TKey>, TKey>
        where TKey : struct
    {
        new IHibernateRepository<TKey> Create();
    }
}
