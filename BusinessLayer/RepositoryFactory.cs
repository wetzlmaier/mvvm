using System.ComponentModel.Composition;
using DataAccessLayer.Hibernate;

namespace BusinessLayer
{
    [Export(typeof(IRepositoryFactory))]
    public class RepositoryFactory: HibernateRepositoryFactory<int>, IRepositoryFactory
    {
        [ImportingConstructor]
        public RepositoryFactory(IModelContext context) : base(context.ConnectionString, context.MappingAssemblies)
        {
        }
    }
}
