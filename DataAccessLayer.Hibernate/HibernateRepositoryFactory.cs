using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

namespace DataAccessLayer.Hibernate
{
    public class HibernateRepositoryFactory<TKey> : IHibernateRepositoryFactory<TKey>
        where TKey : struct
    {
        private static bool IsInitialized;
        private readonly ISessionFactory _sessionFactory;

        protected HibernateRepositoryFactory(string connectionString, string[] mappingAssemblies)
        {
            if (IsInitialized)
                throw new InvalidOperationException("Already initialized");

            try
            {
                var configuration = Fluently.Configure()
                    .Database(MsSqlConfiguration.MsSql2012
                        .ConnectionString(connectionString)
                        .Driver<MicrosoftDataSqlClientDriver>()
                        .ShowSql())
                    .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true));

                foreach (var assembly in mappingAssemblies)
                {
                    configuration = configuration.Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.Load(assembly)));
                }

                _sessionFactory = configuration.BuildSessionFactory();

                IsInitialized = true;
            }
            catch (Exception e)
            {
                throw new DataAccessException("Error initializing Hibernate repository factory", e);
            }
        }

        public IHibernateRepository<TKey> Create()
        {
            return new HibernateRepository<TKey>(_sessionFactory.OpenStatelessSession());
        }

        IHibernateRepository<TKey> IRepositoryFactory<IHibernateRepository<TKey>, TKey>.Create()
        {
            return Create();
        }

        public void Drop()
        {
            throw new NotImplementedException();
        }
    }
}
