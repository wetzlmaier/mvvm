using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

namespace DataAccessLayer.Hibernate
{
    public static class SessionFactoryBuilder
    {
        public static ISessionFactory BuildSessionFactory(string connectionString, string[] mappingAssemblies)
        {
            var config = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString(connectionString)
                    .Driver<MicrosoftDataSqlClientDriver>()
                    .ShowSql());

            foreach (var mappingAssembly in mappingAssemblies)
            {
                // 2. Tell it where to find your mappings (like UserMap)
                config = config.Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.Load(mappingAssembly)));
            }

            // 3. (Optional) Auto-update the database schema based on your mappings
            return config.ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                // 4. Build the expensive factory
                .BuildSessionFactory();
        }
    }
}
