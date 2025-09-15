using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AR.Common;
using AR.Common.Dto;
using AR.Core.UserCustomer.Common.Dto;
using AR.Core.UserCustomer.Common.Interfaces;
using AR.Core.UserCustomer.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AR_ECOMERCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCustomerController : ControllerBase
    {
        private readonly IUserCustomerApplication userCustomerApplication;
        private readonly AppJWT jwt;
        public UserCustomerController(IUserCustomerApplication userCustomerApplication, IOptions<AppJWT> options)
        {
            this.userCustomerApplication = userCustomerApplication;
            this.jwt = options.Value;
        }

        [HttpGet("GetUserCustomers")]
        public Task<List<UserCustomerDto>> GetUserCustomers([FromQuery] int idUserCustomer)
        {
            return userCustomerApplication.GetUserCustomers(idUserCustomer);
        }

        [HttpPost("SaveUserCustomers")]
        public Task<BaseResponseDto> SaveUserCustomers([FromBody] UserCustomerViewModels userCustomerViewModels)
        {
            return userCustomerApplication.SaveUserCustomers(userCustomerViewModels);
        }

        [HttpDelete("DeleteUserCustomers")]
        public Task<BaseResponseDto> DeleteUserCustomers([FromQuery] int userCustomerID)
        {
            return userCustomerApplication.DeleteUserCustomers(userCustomerID);
        }
        
        [HttpGet("LoginUser")]
        public async Task<ResponseDto<UserDtoCustomer>> loginUser([FromQuery] string user, [FromQuery] string pass)
        {
            var userResult = await userCustomerApplication.loginUser(user, pass);
            if (userResult.IsSuccessful)
            {
                userResult.Data.Token = GenerateJWTToken(userResult.Data);
            }
            return userResult!;
        }


        string GenerateJWTToken(UserDtoCustomer user)
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

    }
}