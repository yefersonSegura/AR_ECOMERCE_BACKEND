using AR.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.Common.ViewModels
{
    public class QueryCategoriesViewModel: PagedRequestDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
