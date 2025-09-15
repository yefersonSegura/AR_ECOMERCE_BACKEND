using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Common.Dto
{
    public class DteailDto
    {
        public int productID { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public decimal price { get; set; }
        public int stock { get; set; }
        public int categoryID { get; set; }
        public string urlImage { get; set; } = string.Empty;
        public string categoryName { get; set; } = string.Empty;
        public int quantity { get; set; }
        public decimal discount { get; set; }
        public decimal SubtotalLine => quantity * price;
        public decimal LineDiscount => SubtotalLine * discount / 100;
        public decimal MontoVenta => SubtotalLine - LineDiscount;
        public decimal MontoVentaIgv => (MontoVenta * (0.18m / 100)) + MontoVenta;

    }
    public class ProductDto
    {
        public int productID { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public decimal price { get; set; }
        public int stock { get; set; }
        public int categoryID { get; set; }
        public string urlImage { get; set; } = string.Empty;
        public string categoryName { get; set; } = string.Empty;
        public int quantity { get; set; }
        public decimal discount { get; set; }
    }
}