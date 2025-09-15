using System;
using MassTransit;

namespace Ar.Common.Transaction;

public class ErrorHandlerFilter : IFilter<IFailureContext<ITransactionContext>>
{
    public ErrorHandlerFilter()
    {
    }

    public void Probe(ProbeContext context)
    {
        context.CreateScope(nameof(ErrorHandlerFilter));
    }


    public async Task Send(IFailureContext<ITransactionContext> context, IPipe<IFailureContext<ITransactionContext>> next)
    {
        string errors = string.Join(Environment.NewLine, context.Errors);
        if (context.SendToApproval)
            context.WrappedContext.GetOrAddPayload(() => new TransactionSendToApprovalException());
        else
        {
            context.WrappedContext.GetOrAddPayload(() => new TransactionException(errors));
        }

        await next.Send(context);
    }
}