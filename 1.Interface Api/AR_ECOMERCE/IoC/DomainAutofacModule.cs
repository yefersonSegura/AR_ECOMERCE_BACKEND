using Autofac;
using System.Reflection;

namespace AR_ECOMERCE.IoC
{
    public class DomainAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("AR.Core.Common"))
          .Where(t => t.Name.EndsWith("Entity", StringComparison.InvariantCultureIgnoreCase))
          .PropertiesAutowired();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Core.User.Common"))
            .Where(t => t.Name.EndsWith("Entity", StringComparison.InvariantCultureIgnoreCase))
            .PropertiesAutowired();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Core.Employee.Common"))
            .Where(t => t.Name.EndsWith("Entity", StringComparison.InvariantCultureIgnoreCase))
            .PropertiesAutowired();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Core.Purchase.Common"))
            .Where(t => t.Name.EndsWith("Entity", StringComparison.InvariantCultureIgnoreCase))
            .PropertiesAutowired();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Core.Customer.Common"))
            .Where(t => t.Name.EndsWith("Entity", StringComparison.InvariantCultureIgnoreCase))
            .PropertiesAutowired();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Core.UserCustomer.Common"))
            .Where(t => t.Name.EndsWith("Entity", StringComparison.InvariantCultureIgnoreCase))
            .PropertiesAutowired();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Core.Promotions.Common"))
            .Where(t => t.Name.EndsWith("Entity", StringComparison.InvariantCultureIgnoreCase))
            .PropertiesAutowired();
        }
    }
}
