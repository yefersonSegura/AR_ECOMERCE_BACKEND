using AR.Common;
using AR.Common.Dto;
using AR.Core.Purchase.Application;
using AR.Core.Purchase.Common.ViewModels;
using AR.Core.User.Application;
using AR.Core.User.Common.Dto;
using AR.Core.User.Common.Interfaces;
using AR.Core.User.Common.ViewModels;
using AR.Core.UserCustomer.Application;
using AR.Core.UserCustomer.Common.Dto;
using AR.Core.UserCustomer.Common.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AR_ECOMERCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserApplication userApplication;
        private readonly AppJWT jwt;
        public UserController(IUserApplication userApplication, IOptions<AppJWT> options)
        {
            this.userApplication = userApplication;
            this.jwt = options.Value;
        }

        [HttpGet("LoginUser")]
        public async Task<ResponseDto<UserDto>> loginUser([FromQuery]string user, [FromQuery] string pass)
        {
            var userResult = await userApplication.loginUser(user,pass);
            if (userResult.IsSuccessful)
            {
                userResult.Data.Token = GenerateJWTToken(userResult.Data);
            }
            return userResult!;
        }
       

        string GenerateJWTToken(UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.FirstName!),
            new Claim("userName", user.Email??""),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            claims: claims,
            expires: DateTime.Now.AddHours(4),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("GetUserAdmin")]
        public Task<List<UserAdminDto>> GetUserAdmins(int idUserCustomer)
        {
            return userApplication.GetUserAdmins(idUserCustomer);
        }

        [HttpPost("SaveUserAdmin")]
        public Task<BaseResponseDto> SaveUserAdmin(UserAdminViewModels userAdminViewModels)
        {
            return userApplication.SaveUserAdmin(userAdminViewModels);
        }

        [HttpDelete("DeleteUserAdmin")]
        public Task<BaseResponseDto> DeleteUserAdmin(int userEmployeeID)
        {
            return userApplication.DeleteUserAdmin(userEmployeeID);
        }
        [HttpGet("GetRole")]
        public Task<ResponseDto<List<RoleDto>>> GetRole(int idRole)
        {
            return userApplication.GetRole(idRole);
        }

    }
}