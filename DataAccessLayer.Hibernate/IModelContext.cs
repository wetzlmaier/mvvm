namespace DataAccessLayer.Hibernate
{
    public interface IModelContext
    {
        string[] MappingAssemblies { get; }

        string ConnectionString { get; }
    }
}
