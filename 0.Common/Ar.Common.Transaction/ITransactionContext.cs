using System;
using AR.Common.domain;
using MassTransit;

namespace Ar.Common.Transaction;

public interface ITransactionContext : PipeContext
{
    public BaseDocument Document
    {
        get; set;
    }

    public TransactionInfo Transaction
    {
        get; set;
    }
    public TDoc GetDocument<TDoc>() where TDoc : BaseDocument;
}
