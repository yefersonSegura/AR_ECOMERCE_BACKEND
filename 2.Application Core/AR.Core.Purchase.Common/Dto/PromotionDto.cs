using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.Purchase.Common.Dto
{
    public class PromotionDto
    {
        public int IdPromotion { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public decimal Discount { get; set; }
        public string Color { get; set; } = string.Empty;
        public string UrlImage { get; set; }= string.Empty;
    }
}
