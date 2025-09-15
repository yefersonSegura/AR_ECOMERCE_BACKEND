using AR.Common.Dto;
using AR.Core.Purchase.Common.Dto;
using AR.Core.Purchase.Common.Interfaces;
using AR.Core.Purchase.Common.ViewModels;
using AR.Repository.Base;
using Azure;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace AR.Purchase.Repository
{
    public class ShoppingCartRepository : EntityRepository, IShoppingCartRepository
    {
        public ShoppingCartRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<BaseResponseDto> AddProductToShoppingCart(int customerId, int productId,int quantity)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_addItemsCart", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@ProductId", productId);
                    sql.Parameters.AddWithValue("@CustomerId", customerId);
                    sql.Parameters.AddWithValue("@Quantity", quantity);

                    await con.OpenAsync();
                    SqlDataReader dr = await sql.ExecuteReaderAsync();
                    while (dr.Read())
                    {
                        response.Message = dr.GetString(dr.GetOrdinal("Mensaje"));
                        response.IsSuccessful = dr.GetBoolean(dr.GetOrdinal("Estado"));
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

        public async Task<List<ProductDto>> GetShoppingCartByCustomer(int CustomerId)
        {
            List<ProductDto> items = new List<ProductDto>();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_getProductShopingCart", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.Add("@CustomerId", SqlDbType.Int).Value = CustomerId;
                    con.Open();
                    SqlDataReader dr = await sql.ExecuteReaderAsync();

                    if (dr.HasRows)
                    {
                        var col0 = dr.GetOrdinal("ProductID");
                        var col1 = dr.GetOrdinal("Name");
                        var col2 = dr.GetOrdinal("Description");
                        var col3 = dr.GetOrdinal("Price");
                        var col4 = dr.GetOrdinal("Stock");
                        var col5 = dr.GetOrdinal("CategoryID");
                        var col6 = dr.GetOrdinal("CreatedAt");
                        var col7 = dr.GetOrdinal("UrlImage");
                        var col8 = dr.GetOrdinal("CategoryName");
                        var col9 = dr.GetOrdinal("Quantity");

                        while (dr.Read())
                        {
                          var   result = new ProductDto()
                            {
                                productID = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                                name = dr.IsDBNull(col1) ? string.Empty : dr.GetString(col1),
                                description = dr.IsDBNull(col2) ? string.Empty : dr.GetString(col2),
                                price = dr.IsDBNull(col3) ? 0 : dr.GetDecimal(col3),
                                stock = dr.IsDBNull(col4) ? 0 : dr.GetInt32(col4),
                                categoryID = dr.IsDBNull(col5) ? 0 : dr.GetInt32(col5),
                                urlImage = dr.IsDBNull(col7) ? string.Empty : dr.GetString(col7),
                                categoryName = dr.IsDBNull(col8) ? string.Empty : dr.GetString(col8),
                                quantity = dr.IsDBNull(col9) ? 0 : dr.GetInt32(col9),
                            };
                            items.Add(result);
                        }
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException!).Throw();
            }
            return items;
        }

        public async  Task<BaseResponseDto> RemoveProductOfShoppingCart(int coustomerId, int productId)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_removeItemsCart", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@ProductId", productId);
                    sql.Parameters.AddWithValue("@CustomerId", coustomerId);

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
