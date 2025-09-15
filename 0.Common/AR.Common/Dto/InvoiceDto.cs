using System;
using System.Runtime.CompilerServices;
using AR.Common.domain;

namespace AR.Common.Dto;

public class InvoiceDto : BaseDocument
{
    public int userId { get; set; }
    public DateTime orderDate { get; set; }
    public decimal GrossTotal { get; set; }
    public decimal TotalDiscount { get; set; }
    public decimal TotalIGV { get; set; }
    public decimal TotalGrandPrix { get; set; }
    public string status { get; set; } = string.Empty;
    public string address { get; set; } = string.Empty;
    public string paymentMethod { get; set; } = string.Empty;
    public List<DteailDto> items { get; set; } = new List<DteailDto>();

}
