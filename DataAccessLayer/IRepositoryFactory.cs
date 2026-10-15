namespace DataAccessLayer
{
    public interface IRepositoryFactory<out TRepository, TKey>
        where TRepository : IRepository<TKey>
        where TKey : struct
    {
        TRepository Create();

        void Drop();
    }
}
