
namespace AR.Core.Customer.Common.Dto
{
    public class CustomerDto
    {
        public int customerID { get; set; }
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string phone { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string country { get; set; } = string.Empty;
        public string urlImage { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}