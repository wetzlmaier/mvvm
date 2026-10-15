namespace DataAccessLayer.Hibernate
{
    public abstract class ModelContextBase : IModelContext
    {
        protected ModelContextBase(string connectionString, string[] mappingAssemblies)
        {
            ConnectionString = connectionString;
            var list = new List<string>(mappingAssemblies) { /*"DataAccessLayer", "DataAccessLayer.Hibernate" */};
            MappingAssemblies = list.ToArray();
        }

        public string[] MappingAssemblies { get; }

        public string ConnectionString { get; }
    }
}
