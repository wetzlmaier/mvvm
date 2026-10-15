namespace DataAccessLayer
{
    public interface IUnitOfWork : IDisposable
    {
        bool IsInTransaction { get; }

        void BeginTransaction();

        void RollBackTransaction();

        void CommitTransaction();
    }
}
