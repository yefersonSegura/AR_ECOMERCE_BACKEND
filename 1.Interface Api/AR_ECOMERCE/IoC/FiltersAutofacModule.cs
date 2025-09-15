using System;
using Ar.Common.Transaction;
using AR.Common;
using Autofac;
using MassTransit;

namespace AR_ECOMERCE.IoC;

public class FiltersAutofacModule : Autofac.Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<TransactionManager>()
      .As<ITransactionManager>()
      .AsImplementedInterfaces()
      .AsSelf();
    builder.RegisterType<ErrorHandlerFilter>()
    .As<IFilter<IFailureContext<ITransactionContext>>>()
    .AsImplementedInterfaces()
    .AsSelf();
    builder.RegisterAssemblyTypes(System.Reflection.Assembly.Load("AR.Core.Purchases.Filters"))
          .Where(t => t.Name.EndsWith("Filter", StringComparison.InvariantCultureIgnoreCase))
          .Named<IFilter<ITransactionContext>>(t => (t.GetCustomAttributes(typeof(FilterNameAttribute), false).FirstOrDefault() as FilterNameAttribute)!.Name)
          .AsImplementedInterfaces()
          .SingleInstance()
          .AsSelf();
  }
}
