using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.Purchase.Common.Dto
{
    public class CategoryDto
    {
        public int categoryID { get; set; }
        public string categoryName { get; set; } = string.Empty;
        public string? Description {  get; set; } = string.Empty;
    }
}
