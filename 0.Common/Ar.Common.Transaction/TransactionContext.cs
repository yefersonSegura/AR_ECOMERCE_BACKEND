using System;
using AR.Common.domain;
using AR.Common.Dto;
using MassTransit.Middleware;

namespace Ar.Common.Transaction;

public class TransactionContext : BasePipeContext, ITransactionContext
{
    public TransactionContext(BaseDocument document)
    {
        this.Document = document;

    }
    public  BaseDocument Document { get; set; }
    public TransactionInfo Transaction { get; set; } = new TransactionInfo();

    // Getter seguro que evita casts en el consumidor
    public TDoc GetDocument<TDoc>() where TDoc : BaseDocument
        => Document as TDoc ?? throw new InvalidCastException(
           $"El Document no es del tipo {typeof(TDoc).Name} (es {Document?.GetType().Name ?? "null"}).");
}