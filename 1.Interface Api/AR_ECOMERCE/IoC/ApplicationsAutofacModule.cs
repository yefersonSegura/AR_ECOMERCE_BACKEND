using Autofac;
using System.Reflection;

namespace AR_ECOMERCE.IoC
{
    public class ApplicationsAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("AR.Core.User.Application"))
            .Where(t => t.Name.EndsWith("Application", StringComparison.InvariantCultureIgnoreCase))
            .AsImplementedInterfaces()
            .SingleInstance()
            .AsSelf();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Core.Employee.Application"))
           .Where(t => t.Name.EndsWith("Application", StringComparison.InvariantCultureIgnoreCase))
           .AsImplementedInterfaces()
           .SingleInstance()
           .AsSelf();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Core.Purchase.Application"))
            .Where(t => t.Name.EndsWith("Application", StringComparison.InvariantCultureIgnoreCase))
            .AsImplementedInterfaces()
            .SingleInstance()
            .AsSelf();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Core.Customer.Application"))
            .Where(t => t.Name.EndsWith("Application", StringComparison.InvariantCultureIgnoreCase))
            .AsImplementedInterfaces()
            .SingleInstance()
            .AsSelf();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Core.UserCustomer.Application"))
            .Where(t => t.Name.EndsWith("Application", StringComparison.InvariantCultureIgnoreCase))
            .AsImplementedInterfaces()
            .SingleInstance()
            .AsSelf();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Core.Promotions.Application"))
            .Where(t => t.Name.EndsWith("Application", StringComparison.InvariantCultureIgnoreCase))
            .AsImplementedInterfaces()
            .SingleInstance()
            .AsSelf();
        }
    }
}
