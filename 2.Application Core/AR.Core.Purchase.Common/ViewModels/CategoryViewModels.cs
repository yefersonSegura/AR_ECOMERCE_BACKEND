using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.Purchase.Common.ViewModels
{
    public  class CategoryViewModels
    {
        public int categoryID { get; set; }
        public string categoryName { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

    }
}
