using System;

namespace AR.Core.Purchase.Common.ViewModels;

public sealed class CreateOrderDetailViewModel
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal LineDiscount { get; set; }
    public decimal MontoVentaIgv { get; set; }
}
