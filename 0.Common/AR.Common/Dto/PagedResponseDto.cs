using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Common.Dto
{
    public class PagedResponseDto<T> : BaseResponseDto
    {
        public long Total { get; set; }

        public List<T> Data { get; set; } = new List<T>();
    }
}
