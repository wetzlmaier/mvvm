using System.Data;

namespace DataAccessLayer.Hibernate
{
    public interface IHibernateUnitOfWork : IUnitOfWork
    {
        void BeginTransaction(IsolationLevel isolationLevel);
    }
}
