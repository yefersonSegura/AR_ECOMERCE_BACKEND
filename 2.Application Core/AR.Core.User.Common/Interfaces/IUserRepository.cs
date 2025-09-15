using AR.Common.Dto;
using AR.Core.User.Common.Dto;
using AR.Core.User.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.User.Common.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> loginUser(string user, string pass);
        Task<List<UserAdminDto>> GetUserAdmins(int idUserCustomer);
        Task<BaseResponseDto> SaveUserAdmin(UserAdminViewModels userAdminViewModels);
        Task<BaseResponseDto> DeleteUserAdmin(int userEmployeeID);
        Task<List<RoleDto>> GetRole(int idRole);
    }
}
