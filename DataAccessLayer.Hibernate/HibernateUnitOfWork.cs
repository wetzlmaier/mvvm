using System.Data;
using NHibernate;

namespace DataAccessLayer.Hibernate
{
    public class HibernateUnitOfWork : IDisposable, IHibernateUnitOfWork
    {
        private ITransaction? _transaction;
        private readonly IStatelessSession _session;
        private bool _isDisposed;

        public HibernateUnitOfWork(IStatelessSession session)
        {
            _session = session;
        }

        public bool IsInTransaction => _transaction != null;

        public void BeginTransaction()
        {
            BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (IsInTransaction)
            {
                throw new InvalidOperationException("Cannot begin a new transaction while an existing transaction is still running. " +
                                                "Please commit or rollback the existing transaction before starting a new one.");
            }

            _transaction = _session.BeginTransaction(isolationLevel);
        }
        
        public void RollBackTransaction()
        {
            if (!IsInTransaction)
            {
                throw new InvalidOperationException("Cannot roll back a transaction while there is no transaction running.");
            }

            _transaction?.Rollback();
            ReleaseCurrentTransaction();
        }

        public void CommitTransaction()
        {
            if (!IsInTransaction)
            {
                throw new InvalidOperationException("Cannot roll back a transaction while there is no transaction running.");
            }

            try
            {
                _transaction?.Commit();
                ReleaseCurrentTransaction();
            }
            catch (Exception)
            {
                RollBackTransaction();
                throw;
            }
        }

        public virtual void Dispose()
        {
            if (!_isDisposed && _transaction != null)
            {
                _transaction.Dispose();
                _isDisposed = true;
            }

        }

        private void ReleaseCurrentTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
