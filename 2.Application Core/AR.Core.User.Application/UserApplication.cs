using AR.Common.Dto;
using AR.Core.User.Common.Dto;
using AR.Core.User.Common.Interfaces;
using AR.Core.User.Common.ViewModels;
using System.Runtime.ExceptionServices;

namespace AR.Core.User.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserRepository userRepository;
        public UserApplication(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<ResponseDto<UserDto>> loginUser(string user, string pass)
        {
            ResponseDto<UserDto> result = new ResponseDto<UserDto>();
            try
            {
                if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                {
                    result.Message = "Ingrese usuario y intraseña";
                    return result;
                }

                result.Data = await userRepository.loginUser(user, pass);
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

        public async Task<List<UserAdminDto>> GetUserAdmins(int idUserCustomer)
        {
            List<UserAdminDto> result = new List<UserAdminDto>();
            try
            {
                result = await userRepository.GetUserAdmins(idUserCustomer);
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }

        public async Task<BaseResponseDto> SaveUserAdmin(UserAdminViewModels userAdminViewModels)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                result = await userRepository.SaveUserAdmin(userAdminViewModels);
            }
            catch (Exception ex)
            {
                result.Status = ex.HResult;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<BaseResponseDto> DeleteUserAdmin(int userEmployeeID)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                result = await userRepository.DeleteUserAdmin(userEmployeeID);
            }
            catch (Exception ex)
            {
                result.Status = ex.HResult;
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<ResponseDto<List<RoleDto>>> GetRole(int idRole)
        {
            ResponseDto<List<RoleDto>> result = new ResponseDto<List<RoleDto>>();
            try
            {
                result.Data = await userRepository.GetRole(idRole);
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