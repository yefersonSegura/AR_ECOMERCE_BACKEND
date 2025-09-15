
namespace AR.Core.UserCustomer.Common.Dto
{
    public class UserCustomerDto
    {
        public int userID { get; set; }
        public string userName { get; set; } = string.Empty;
        public int customerID { get; set; }
    }
    public class UserDtoCustomer
    {
        public int UserID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}