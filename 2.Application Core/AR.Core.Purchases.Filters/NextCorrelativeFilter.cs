using System;
using Ar.Common.Transaction;
using AR.Common;
using AR.Core.Common.Interfaces;
using MassTransit;

namespace AR.Core.Purchases.Filters;

[FilterName("nextCorrelativeFilter")]
public class NextCorrelativeFilter : BaseFilter
{
    private readonly ITransactionRepository transactionRepository;
    public NextCorrelativeFilter(IPipe<IFailureContext<ITransactionContext>> failurePipe, ITransactionRepository transactionRepository) : base(failurePipe)
    {
        this.transactionRepository = transactionRepository;
    }
    public override async Task Send(ITransactionContext context, IPipe<ITransactionContext> next)
    {
        try
        {
            var result = await transactionRepository.NextNumber(context.Document.DocumentId);
            if (!result.IsSuccessful)
            {
                await SendError(context, "Error al  generar correlativo del documento");
                return;
            }
            context.Document.Number = result.Data;
            context.Document.Serial = context.Document.transaction.DocumentType.Serial;
            context.Document.transaction.DocumentType.Number = result.Data;
        }
        catch (Exception ex)
        {
            await SendError(context, ex.Message);
            return;
        }
        await next.Send(context);
    }
}
