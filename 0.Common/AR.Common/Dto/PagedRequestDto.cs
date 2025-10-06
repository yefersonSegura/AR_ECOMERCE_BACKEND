using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Common.Dto
{
    public abstract class PagedRequestDto: RequestDto
    {
        protected PagedRequestDto()
        {
            Page = 0;
            PageSize = 50;
        }

        public int PageSize { get; set; }

        public int Page { get; set; }

        public string SortField { get; set; }= string.Empty;

        public int SortOrder { get; set; }

    }
}
