using System;

namespace AR.Core.Purchase.Common.ViewModels;

public sealed class CreateOrderViewModel
{
    public int UserId { get; set; }
    public DateTime? OrderDate { get; set; }
    public decimal Total { get; set; }
    public string Status { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public string PaymentMethod { get; set; } = string.Empty;
    public decimal GrossTotal { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalIGV { get; set; }
    public int IdOperation { get; set; }
    public string Serial { get; set; } = string.Empty;
    public int Number { get; set; }
    public int TypeDocumentId { get; set; }
}
