using AR.Common.Dto;
using AR.Core.UserCustomer.Common.Dto;
using AR.Core.UserCustomer.Common.ViewModels;

namespace AR.Core.UserCustomer.Common.Interfaces
{
    public interface IUserCustomerRepository
    {
        Task<List<UserCustomerDto>> GetUserCustomers(int idUserCustomer);
        Task<BaseResponseDto> SaveUserCustomers(UserCustomerViewModels userCustomerViewModels);
        Task<BaseResponseDto> DeleteUserCustomers(int userCustomerID);
        Task<UserDtoCustomer> loginUser(string user, string pass);
    }
}