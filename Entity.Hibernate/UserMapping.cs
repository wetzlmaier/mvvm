using FluentNHibernate.Mapping;

namespace Entity.Hibernate
{
    public class UserMapping : ClassMap<User>
    {
        public UserMapping()
        {
            // Maps to a table named "Users"
            Table("Users");

            // Defines the primary key and how it is generated
            Id(x => x.Id).GeneratedBy.Identity();

            // Maps the standard columns
            Map(x => x.FirstName).Not.Nullable().Length(50);
            Map(x => x.LastName).Not.Nullable().Length(50);
            Map(x => x.IsActive).Not.Nullable();
        }
    }
}
