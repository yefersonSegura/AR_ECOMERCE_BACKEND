using Autofac;
using System.Reflection;

namespace AR_ECOMERCE.IoC
{
    public class RepositoryAutofacModule : Autofac.Module
    {
        private readonly string connectionString;
        public RepositoryAutofacModule(string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("AR.Transaction.Repository"))
            .Where(t => t.Name.EndsWith("Repository", StringComparison.InvariantCultureIgnoreCase))
            .WithParameter("connectionString", connectionString)
            .AsImplementedInterfaces()
            .SingleInstance()
            .AsSelf();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.User.Repository"))
            .Where(t => t.Name.EndsWith("Repository", StringComparison.InvariantCultureIgnoreCase))
            .WithParameter("connectionString", connectionString)
            .AsImplementedInterfaces()
            .SingleInstance()
            .AsSelf();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Employee.Repository"))
            .Where(t => t.Name.EndsWith("Repository", StringComparison.InvariantCultureIgnoreCase))
            .WithParameter("connectionString", connectionString)
            .AsImplementedInterfaces()
            .SingleInstance()
            .AsSelf();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Purchase.Repository"))
            .Where(t => t.Name.EndsWith("Repository", StringComparison.InvariantCultureIgnoreCase))
            .WithParameter("connectionString", connectionString)
            .AsImplementedInterfaces()
            .SingleInstance()
            .AsSelf();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Customer.Repository"))
            .Where(t => t.Name.EndsWith("Repository", StringComparison.InvariantCultureIgnoreCase))
            .WithParameter("connectionString", connectionString)
            .AsImplementedInterfaces()
            .SingleInstance()
            .AsSelf();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.UserCustomer.Repository"))
            .Where(t => t.Name.EndsWith("Repository", StringComparison.InvariantCultureIgnoreCase))
            .WithParameter("connectionString", connectionString)
            .AsImplementedInterfaces()
            .SingleInstance()
            .AsSelf();

            builder.RegisterAssemblyTypes(Assembly.Load("AR.Promotions.Repository"))
            .Where(t => t.Name.EndsWith("Repository", StringComparison.InvariantCultureIgnoreCase))
            .WithParameter("connectionString", connectionString)
            .AsImplementedInterfaces()
            .SingleInstance()
            .AsSelf();
        }
    }
}
