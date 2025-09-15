

namespace AR.Core.Promotions.Common.ViewModels
{
    public class PromotionsViewModels
    {
        public int idPromotion { get; set; }
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
        public string base64Url { get; set; } = string.Empty;
        public decimal discount { get; set; }
        public int idProduct { get; set; }
        public int idCategory { get; set; }
        public bool state { get; set; }
        public string color { get; set; } = string.Empty;
    }
}