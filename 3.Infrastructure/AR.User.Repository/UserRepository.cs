using AR.Core.User.Common.Dto;
using AR.Core.User.Common.Interfaces;
using AR.Repository.Base;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Runtime.ExceptionServices;
using AR.Common.Dto;
using AR.Core.User.Common.ViewModels;

namespace AR.User.Repository
{
    public class UserRepository : EntityRepository, IUserRepository
    {
        public UserRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<UserDto> loginUser(string user, string pass)
        {
            UserDto userDto = new UserDto();
            try
            {
                using var con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("Sp_LoginUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@user", user);
                cmd.Parameters.AddWithValue("@pass", pass);
                await con.OpenAsync();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    var cold0 = reader.GetOrdinal("UserID");
                    var cold1 = reader.GetOrdinal("UserName");
                    var cold2 = reader.GetOrdinal("Email");
                    var cold3 = reader.GetOrdinal("FirstName");
                    var cold4 = reader.GetOrdinal("LastName");
                    while (reader.Read())
                    {
                        userDto = new UserDto()
                        {
                            UserID = reader.GetInt32(cold0),
                            UserName = reader.GetString(cold1),
                            Email = reader.GetString(cold2),
                            FirstName = reader.GetString(cold3),
                            LastName = reader.GetString(cold4),
                        };
                    }
                }
                await con.CloseAsync();
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return userDto;
        }



        public async Task<List<UserAdminDto>> GetUserAdmins(int idUserCustomer)
        {
            var employeeList = new List<UserAdminDto>();
            var response = new ResponseDto<List<UserAdminDto>>();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_listUserAdmin", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.Add("@UserID", SqlDbType.Int).Value = idUserCustomer;
                    con.Open();
                    SqlDataReader dr = await sql.ExecuteReaderAsync();

                    if (dr.HasRows)
                    {
                        var col0 = dr.GetOrdinal("userID");
                        var col1 = dr.GetOrdinal("userName");
                        var col2 = dr.GetOrdinal("employeeID");

                        while (dr.Read())
                        {
                            var model = new UserAdminDto()
                            {
                                userID = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                                userName = dr.IsDBNull(col1) ? string.Empty : dr.GetString(col1),
                                employeeID = dr.IsDBNull(col2) ? 0 : dr.GetInt32(col2),

                            };
                            employeeList.Add(model);
                        }
                    }

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException!).Throw();
            }
            return employeeList;
        }

        public async Task<BaseResponseDto> SaveUserAdmin(UserAdminViewModels userAdminViewModels)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_insertUserAdmin", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@UserID", userAdminViewModels.userID);
                    sql.Parameters.AddWithValue("@UserName", userAdminViewModels.userName);
                    sql.Parameters.AddWithValue("@EmployeeID", userAdminViewModels.employeeID);
                    sql.Parameters.AddWithValue("@Password", userAdminViewModels.password_User);
                    sql.Parameters.AddWithValue("@RoleID", userAdminViewModels.RoleID);
                    await con.OpenAsync();
                    SqlDataReader dr = await sql.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        response.Message = dr.GetString(dr.GetOrdinal("Mensaje"));
                        response.IsSuccessful = dr.GetBoolean(dr.GetOrdinal("ESTADO"));
                    }
                    await con.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return response;
        }

        public async Task<BaseResponseDto> DeleteUserAdmin(int userEmployeeID)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_deleteUserAdmin", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@UserID", userEmployeeID);
                    await con.OpenAsync();
                    SqlDataReader dr = await sql.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        response.Message = dr.GetString(1);
                        response.IsSuccessful = dr.GetInt32(0) == 0 ? true : false;
                    }
                    await con.CloseAsync();
                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return response;
        }

        public async Task<List<RoleDto>> GetRole(int idRole)
        {
            List<RoleDto> roleDto = new List<RoleDto>();
            try
            {
                using var con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand("sp_getRoles", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idRole", idRole);
                await con.OpenAsync();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    var cold0 = reader.GetOrdinal("roleID");
                    var cold1 = reader.GetOrdinal("roleName");
                    while (reader.Read())
                    {
                        roleDto.Add(new RoleDto()
                        {
                            RoleID = reader.GetInt32(cold0),
                            RoleName = reader.GetString(cold1),
                        });
                    }
                }
                await con.CloseAsync();
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return roleDto;
        }
    }
}
