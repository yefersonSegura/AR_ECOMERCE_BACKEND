using AR.Common.Dto;
using AR.Core.Customer.Common.Dto;
using AR.Core.Customer.Common.Interfaces;
using AR.Core.Customer.Common.ViewModels;
using AR.Repository.Base;
using Firebase.Auth;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;

namespace AR.Customer.Repository
{
    public class CustomerRepository : EntityRepository, ICustomerRepository
    {
        public CustomerRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<List<CustomerDto>> GetCustomers(int idCustomer)
        {
            var customerList = new List<CustomerDto>();
            var response = new ResponseDto<List<CustomerDto>>();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_listCustomer", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.Add("@IdCustomer", SqlDbType.Int).Value = idCustomer;
                    con.Open();
                    SqlDataReader dr = await sql.ExecuteReaderAsync();

                    if (dr.HasRows)
                    {
                        var col0 = dr.GetOrdinal("customerID");
                        var col1 = dr.GetOrdinal("firstName");
                        var col2 = dr.GetOrdinal("lastName");
                        var col3 = dr.GetOrdinal("email");
                        var col4 = dr.GetOrdinal("phone");
                        var col5 = dr.GetOrdinal("address");
                        var col6 = dr.GetOrdinal("city");
                        var col7 = dr.GetOrdinal("country");
                        var col8 = dr.GetOrdinal("urlImage");
                        var col9 = dr.GetOrdinal("UserId");

                        while (dr.Read())
                        {
                            var model = new CustomerDto()
                            {
                                customerID = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                                firstName = dr.IsDBNull(col1) ? string.Empty : dr.GetString(col1),
                                lastName = dr.IsDBNull(col2) ? string.Empty : dr.GetString(col2),
                                email = dr.IsDBNull(col3) ? string.Empty : dr.GetString(col3),
                                phone = dr.IsDBNull(col4) ? string.Empty : dr.GetString(col4),
                                address = dr.IsDBNull(col5) ? string.Empty : dr.GetString(col5),
                                city = dr.IsDBNull(col6) ? string.Empty : dr.GetString(col6),
                                country = dr.IsDBNull(col7) ? string.Empty : dr.GetString(col7),
                                urlImage = dr.IsDBNull(col8) ? string.Empty : dr.GetString(col8),
                                UserId= dr.IsDBNull(col9)?0: dr.GetInt32(col9)
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

        public async Task<BaseResponseDto> SaveCustomers(CustomerViewModels customerViewModels)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_insertCustomer", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@IdCustomer", customerViewModels.customerID);
                    sql.Parameters.AddWithValue("@FirstName", customerViewModels.firstName);
                    sql.Parameters.AddWithValue("@LastName", customerViewModels.lastName);
                    sql.Parameters.AddWithValue("@Email", customerViewModels.email);
                    sql.Parameters.AddWithValue("@Phone", customerViewModels.phone);
                    sql.Parameters.AddWithValue("@Address", customerViewModels.address);
                    sql.Parameters.AddWithValue("@City", customerViewModels.city);
                    sql.Parameters.AddWithValue("@Country", customerViewModels.country);
                    sql.Parameters.AddWithValue("@UrlImage", customerViewModels.urlImage);
                    sql.Parameters.AddWithValue("@Password",customerViewModels.password);
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

        public async Task<BaseResponseDto> DeleteCustomers(int customerID)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_deleteCustomer", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@CustomerID", customerID);
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