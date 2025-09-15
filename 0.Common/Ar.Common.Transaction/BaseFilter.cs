using System;
using MassTransit;

namespace Ar.Common.Transaction;

	public abstract class BaseFilter : IFilter<ITransactionContext>
    {
        private readonly IPipe<IFailureContext<ITransactionContext>> failurePipe;

        public BaseFilter(IPipe<IFailureContext<ITransactionContext>> failurePipe)
        {
            this.failurePipe = failurePipe;
        }

        public virtual void Probe(ProbeContext context)
        {
            context.CreateFilterScope(nameof(BaseFilter));

            failurePipe.Probe(context.CreateScope(nameof(failurePipe)));

        }

        public abstract Task Send(ITransactionContext context, IPipe<ITransactionContext> next);

        protected async Task SendError(ITransactionContext context, string error)
        {
            var ctx = new FailureContext<ITransactionContext>(context).WithError(string.Empty, error);
            await failurePipe.Send(ctx);
        }
    }
