using System.ComponentModel;

namespace DataAccessLayer
{
    /// <summary>
    /// Base class for entities.
    /// </summary>
    [Serializable]
    public abstract class Entity<TKey> : IEntity<TKey> where TKey : struct
    {
        protected Entity()
        {
            Id = default!;
        }

        [Browsable(false)]
        public virtual TKey Id { get; protected set; }
    }
}
