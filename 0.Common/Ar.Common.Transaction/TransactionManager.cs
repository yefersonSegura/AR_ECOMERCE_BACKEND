using System;
using AR.Core.Common.Interfaces;
using Autofac;

namespace Ar.Common.Transaction;

public class TransactionManager : BaseTransactionManager, ITransactionManager
{
    public TransactionManager(IComponentContext context,
       ITransactionRepository transactionRepository
        ) : base(context, transactionRepository)
    {
    }
}
