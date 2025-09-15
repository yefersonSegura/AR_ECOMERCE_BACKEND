using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.Purchase.Common.ViewModels
{
    public class ProductViewModels
    {
        public int productID { get; set; }
        public string name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public decimal price { get; set; }
        public int stock { get; set; }
        public int categoryID { get; set; }
        public string base64Url { get; set; } = string.Empty;
        public string urlImage { get; set; } = string.Empty;
    }
}