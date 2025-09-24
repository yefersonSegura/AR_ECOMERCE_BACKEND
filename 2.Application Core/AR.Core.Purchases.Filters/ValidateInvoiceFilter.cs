using Ar.Common.Transaction;
using AR.Common;
using AR.Common.Dto;
using MassTransit;

namespace AR.Core.Purchases.Filters;

[FilterName("validateInvoiceFilter")]
public class ValidateInvoiceFilter : BaseFilter
{
    public ValidateInvoiceFilter(IPipe<IFailureContext<ITransactionContext>> failurePipe) : base(failurePipe)
    {
    }
    public override async Task Send(ITransactionContext context, IPipe<ITransactionContext> next)
    {
        try
        {
            var model = context.GetDocument<InvoiceDto>();
            if (model.transaction.Operation.id == 0)
            {
                await SendError(context, "No se encuentra la operación para continuar con el  proceso");
                return;
            }
            if (model.transaction.DocumentType.DocumentTypeId == 0)
            {
                await SendError(context, "No se encuentra el  tipo de documento a generar");
                return;
            }
            if (!model.items.Any())
            {
                await SendError(context, "No se encontro productos para generar la operación");
                return;
            }
        }
        catch (Exception ex)
        {
            await SendError(context, ex.Message);
            return;
        }
        await next.Send(context);
    }
}
