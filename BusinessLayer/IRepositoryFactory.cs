using DataAccessLayer.Hibernate;

namespace BusinessLayer
{
    public interface IRepositoryFactory : IHibernateRepositoryFactory<int>
    {
    }
}
