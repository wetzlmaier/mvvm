using DataAccessLayer;

namespace Entity.Hibernate
{
    public class User : Entity<int>
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual bool IsActive { get; set; }

        // A parameterless constructor is required by NHibernate
        protected User()
        {
            FirstName = null!;
            LastName = null!;
        }

        // A constructor for creating new users in your application
        public User(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            IsActive = true;
        }
    }
}
