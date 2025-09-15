using AR.Common.Dto;
using AR.Core.UserCustomer.Common.Dto;
using AR.Core.UserCustomer.Common.Interfaces;
using AR.Core.UserCustomer.Common.ViewModels;
using System.Runtime.ExceptionServices;

namespace AR.Core.UserCustomer.Application
{
    public class UserCustomerApplication : IUserCustomerApplication
    {
        private readonly IUserCustomerRepository userCustomerRepository;
        public UserCustomerApplication(IUserCustomerRepository userCustomerRepository)
        {
            this.userCustomerRepository = userCustomerRepository;
        }

        public async Task<List<UserCustomerDto>> GetUserCustomers(int idUserCustomer)
        {
            List<UserCustomerDto> result = new List<UserCustomerDto>();
            try
            {
                result = await userCustomerRepository.GetUserCustomers(idUserCustomer);
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }

        public async Task<BaseResponseDto> SaveUserCustomers(UserCustomerViewModels userCustomerViewModels)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                result = await userCustomerRepository.SaveUserCustomers(userCustomerViewModels);
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }

        public async Task<BaseResponseDto> DeleteUserCustomers(int userCustomerID)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                result = await userCustomerRepository.DeleteUserCustomers(userCustomerID);
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }
        public async Task<ResponseDto<UserDtoCustomer>> loginUser(string user, string pass)
        {
            ResponseDto<UserDtoCustomer> result = new ResponseDto<UserDtoCustomer>();
            try
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                {
                    result.Message = "Ingrese usuario y intraseña";
                    return result;
                }

                result.Data = await userCustomerRepository.loginUser(user, pass);
                if (result.Data.UserID == 0)
                {
                    result.Message = "El usuario no extiste";
                    return result;
                }
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Status = ex.HResult;
            }
            return result;
        }
    }
}