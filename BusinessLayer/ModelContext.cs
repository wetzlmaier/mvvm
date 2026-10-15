using System.ComponentModel.Composition;
using System.Configuration;
using DataAccessLayer.Hibernate;

namespace BusinessLayer
{
    [Export(typeof(IModelContext))]
    public class ModelContext()
        : ModelContextBase(ConfigurationManager.AppSettings["ConnectionString"]!, ["Entity.Hibernate"]);
}
