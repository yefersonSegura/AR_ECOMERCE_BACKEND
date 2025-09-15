using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.Purchase.Common.ViewModels
{
    public class ShoppingCartViewModel
    {
        public int ProductId { get; set;}
        public int CustomerId { get; set;}
        public int Quantity { get; set; }
    }
}
