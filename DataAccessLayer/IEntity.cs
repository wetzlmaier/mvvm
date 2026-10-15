namespace DataAccessLayer
{
    public interface IEntity<out TKey> where TKey : struct
    {
        TKey Id { get; }
    }
}
