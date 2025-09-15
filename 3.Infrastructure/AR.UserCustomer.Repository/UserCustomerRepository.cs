using AR.Common.Dto;
using AR.Core.UserCustomer.Common.Dto;
using AR.Core.UserCustomer.Common.Interfaces;
using AR.Core.UserCustomer.Common.ViewModels;
using AR.Repository.Base;
using Firebase.Auth;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;

namespace AR.UserCustomer.Repository
{
    public class UserCustomerRepository : EntityRepository, IUserCustomerRepository
    {
        public UserCustomerRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<List<UserCustomerDto>> GetUserCustomers(int idUserCustomer)
        {
            var customerList = new List<UserCustomerDto>();
            var response = new ResponseDto<List<UserCustomerDto>>();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_listUserCustomer", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.Add("@UserID", SqlDbType.Int).Value = idUserCustomer;
                    con.Open();
                    SqlDataReader dr = await sql.ExecuteReaderAsync();

                    if (dr.HasRows)
                    {
                        var col0 = dr.GetOrdinal("userID");
                        var col1 = dr.GetOrdinal("userName");
                        var col2 = dr.GetOrdinal("customerID");

                        while (dr.Read())
                        {
                            var model = new UserCustomerDto()
                            {
                                userID = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                                userName = dr.IsDBNull(col1) ? string.Empty : dr.GetString(col1),
                                customerID = dr.IsDBNull(col2) ? 0 : dr.GetInt32(col2),

                            };
                            customerList.Add(model);
                        }
                    }

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException!).Throw();
            }
            return customerList;
        }

        public async Task<BaseResponseDto> SaveUserCustomers(UserCustomerViewModels userCustomerViewModels)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_insertUserCustomer", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@UserID", userCustomerViewModels.userID);
                    sql.Parameters.AddWithValue("@UserName", userCustomerViewModels.userName);
                    sql.Parameters.AddWithValue("@CustomerID", userCustomerViewModels.customerID);
                    sql.Parameters.AddWithValue("@Password_User", userCustomerViewModels.password_User);
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

        public async Task<BaseResponseDto> DeleteUserCustomers(int userCustomerID)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_deleteUserCustomer", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@UserID", userCustomerID);
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

        public async Task<UserDtoCustomer> loginUser(string user, string pass)
        {
            UserDtoCustomer userDto = new UserDtoCustomer();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("Sp_LoginUserCustomer", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@user", user);
                    sql.Parameters.AddWithValue("@pass", pass);
                    await con.OpenAsync();
                    SqlDataReader dr = await sql.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        userDto.UserID = dr.IsDBNull(dr.GetOrdinal("UserID")) ? 0 : dr.GetInt32(dr.GetOrdinal("UserID"));
                        userDto.Email = dr.IsDBNull(dr.GetOrdinal("Email")) ? string.Empty : dr.GetString(dr.GetOrdinal("Email"));
                        userDto.LastName = dr.IsDBNull(dr.GetOrdinal("LastName")) ? "" : dr.GetString(dr.GetOrdinal("LastName"));
                        userDto.FirstName = dr.IsDBNull(dr.GetOrdinal("FirstName")) ? "" : dr.GetString(dr.GetOrdinal("FirstName"));
                    }
                    con.Close();
                }
            }
            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(new Exception(e.Message)).Throw();
            }
            return userDto;
        }
    }
}