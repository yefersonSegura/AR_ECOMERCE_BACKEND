using System;
using Ar.Common.Transaction;
using AR.Common;
using AR.Common.Dto;
using AR.Core.Purchase.Common.Interfaces;
using AR.Core.Purchase.Common.ViewModels;
using MassTransit;

namespace AR.Core.Purchases.Filters;


[FilterName("saveInvoiceFilter")]
public class SaveInvoiceFilter : BaseFilter
{
    private readonly IPurchaseRepository purchaseRepository;
    public SaveInvoiceFilter(IPipe<IFailureContext<ITransactionContext>> failurePipe, IPurchaseRepository purchaseRepository) : base(failurePipe)
    {
        this.purchaseRepository = purchaseRepository;
    }
    public override async Task Send(ITransactionContext context, IPipe<ITransactionContext> next)
    {
        try
        {
            var model = context.GetDocument<InvoiceDto>();
            var vheaderModel = new CreateOrderViewModel()
            {
                GrossTotal = model.GrossTotal,
                Number = model.Number,
                OrderDate = DateTime.Now,
                PaymentMethod = model.paymentMethod,
                Serial = model.Serial,
                ShippingAddress = model.address,
                Status = "E-ING",
                Total = model.TotalGrandPrix,
                TotalDiscount = model.TotalDiscount,
                TotalIGV = model.TotalIGV,
                UserId = model.userId,
                IdOperation = model.transaction.Operation.id,
                TypeDocumentId= model.transaction.DocumentType.Id
            };

            var saveHeader = await purchaseRepository.SaveOrder(vheaderModel);
            if (!saveHeader.IsSuccessful)
            {
                await SendError(context, "Error al procesar el pedido");
                return;
            }

            foreach (var item in model.items)
            {
                var detail = new CreateOrderDetailViewModel()
                {
                    OrderId = saveHeader.Data,
                    Discount = item.discount,
                    LineDiscount = item.LineDiscount,
                    MontoVentaIgv = item.MontoVentaIgv,
                    ProductId = item.productID,
                    Quantity = item.quantity,
                    Subtotal = item.SubtotalLine,
                    TotalDiscount = 0,
                    UnitPrice = item.price,
                };
                await purchaseRepository.SaveDetailOrder(detail);
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