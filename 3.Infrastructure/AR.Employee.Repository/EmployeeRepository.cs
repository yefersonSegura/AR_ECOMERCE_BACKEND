using AR.Common.Dto;
using AR.Core.Employee.Common.Dto;
using AR.Core.Employee.Common.Interfaces;
using AR.Core.Employee.Common.ViewModels;
using AR.Repository.Base;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;

namespace AR.Employee.Repository
{
    public class EmployeeRepository : EntityRepository, IEmployeeRepository
    {
        public EmployeeRepository(string connectionString) : base(connectionString)
        {
        }
        public async Task<List<EmployeeDto>> GetEmployees(int idEmployee)
        {
            var employeeList = new List<EmployeeDto>();
            var response = new ResponseDto<List<EmployeeDto>>();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_listEmployee", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.Add("@IdEmployee", SqlDbType.Int).Value = idEmployee;
                    con.Open();
                    SqlDataReader dr = await sql.ExecuteReaderAsync();

                    if (dr.HasRows)
                    {
                        var col0 = dr.GetOrdinal("employeeID");
                        var col1 = dr.GetOrdinal("firstName");
                        var col2 = dr.GetOrdinal("lastName");
                        var col3 = dr.GetOrdinal("email");
                        var col4 = dr.GetOrdinal("phone");
                        var col5 = dr.GetOrdinal("address");
                        var col6 = dr.GetOrdinal("city");
                        var col7 = dr.GetOrdinal("country");
                        var col8 = dr.GetOrdinal("urlImage");

                        while (dr.Read())
                        {
                            var model = new EmployeeDto()
                            {
                                employeeID = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                                firstName = dr.IsDBNull(col1) ? string.Empty : dr.GetString(col1),
                                lastName = dr.IsDBNull(col2) ? string.Empty : dr.GetString(col2),
                                email = dr.IsDBNull(col3) ? string.Empty : dr.GetString(col3),
                                phone = dr.IsDBNull(col4) ? string.Empty : dr.GetString(col4),
                                address = dr.IsDBNull(col5) ? string.Empty : dr.GetString(col5),
                                city = dr.IsDBNull(col6) ? string.Empty : dr.GetString(col6),
                                country = dr.IsDBNull(col7) ? string.Empty : dr.GetString(col7),
                                urlImage = dr.IsDBNull(col8) ? string.Empty : dr.GetString(col8),
                                userID= dr.IsDBNull(dr.GetOrdinal("userID")) ? 0 : dr.GetInt32(dr.GetOrdinal("userID")),
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

        public async Task<BaseResponseDto> SaveEmployee(EmployeeViewModels employeeViewModels)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_insertEmployee", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@IdEmployee", employeeViewModels.employeeID);
                    sql.Parameters.AddWithValue("@FirstName", employeeViewModels.firstName);
                    sql.Parameters.AddWithValue("@LastName", employeeViewModels.lastName);
                    sql.Parameters.AddWithValue("@Email", employeeViewModels.email);
                    sql.Parameters.AddWithValue("@Phone", employeeViewModels.phone);
                    sql.Parameters.AddWithValue("@Address", employeeViewModels.address);
                    sql.Parameters.AddWithValue("@City", employeeViewModels.city);
                    sql.Parameters.AddWithValue("@Country", employeeViewModels.country);
                    sql.Parameters.AddWithValue("@UrlImage", employeeViewModels.urlImage);
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

        public async Task<BaseResponseDto> DeleteEmployee(int employeeID)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_deleteEmployee", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@EmployeeID", employeeID);
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
    }
}
