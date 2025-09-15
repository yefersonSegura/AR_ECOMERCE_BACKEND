using AR.Common.Dto;
using AR.Core.Promotions.Common.Dto;
using AR.Core.Promotions.Common.Interfaces;
using AR.Core.Promotions.Common.ViewModels;
using AR.Repository.Base;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Runtime.ExceptionServices;

namespace AR.Promotions.Repository
{
    internal class PromotionsRepository : EntityRepository, IPromotionsRepository
    {
        public PromotionsRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<BaseResponseDto> DeletePromotions(int promotionsID)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_deletePromotions", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@IdPromotion", promotionsID);
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

        public async Task<List<PromotionsDto>> GetPromotions(int idPromotions)
        {
            var promotionsList = new List<PromotionsDto>();
            var response = new ResponseDto<List<PromotionsDto>>();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_listPromotions", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.Add("@IdPromotions", SqlDbType.Int).Value = idPromotions;
                    con.Open();
                    SqlDataReader dr = await sql.ExecuteReaderAsync();

                    if (dr.HasRows)
                    {
                        var col0 = dr.GetOrdinal("IdPromotion");
                        var col1 = dr.GetOrdinal("Title");
                        var col2 = dr.GetOrdinal("Description");
                        var col3 = dr.GetOrdinal("StartDate");
                        var col4 = dr.GetOrdinal("EndDate");
                        var col5 = dr.GetOrdinal("ImageUrl");
                        var col6 = dr.GetOrdinal("Discount");
                        var col7 = dr.GetOrdinal("IdProduct");
                        var col8 = dr.GetOrdinal("IdCategory");
                        var col9 = dr.GetOrdinal("State");
                        var col10 = dr.GetOrdinal("Color");

                        while (dr.Read())
                        {
                            var model = new PromotionsDto()
                            {
                                idPromotion = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                                title = dr.IsDBNull(col1) ? string.Empty : dr.GetString(col1),
                                description = dr.IsDBNull(col2) ? string.Empty : dr.GetString(col2),
                                startDate = dr.IsDBNull(col3) ? DateTime.Now : dr.GetDateTime(col3),
                                endDate = dr.IsDBNull(col4) ? DateTime.Now : dr.GetDateTime(col4),
                                imageUrl = dr.IsDBNull(col5) ? string.Empty : dr.GetString(col5),
                                discount = dr.IsDBNull(col6) ? 0 : dr.GetDecimal(col6),
                                idProduct = dr.IsDBNull(col7) ? 0 : dr.GetInt32(col7),
                                idCategory = dr.IsDBNull(col8) ? 0 : dr.GetInt32(col8),
                                state = dr.IsDBNull(col9) ? false : dr.GetBoolean(col9),
                                color = dr.IsDBNull(col10) ? string.Empty : dr.GetString(col10)
                            };
                            promotionsList.Add(model);
                        }
                    }

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException!).Throw();
            }
            return promotionsList;
        }

        public async Task<List<PromotionsDto>> GetPromotionsByProductId(int idProduct)
        {
            var promotionsList = new List<PromotionsDto>();
            var response = new ResponseDto<List<PromotionsDto>>();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_listPromotionsByProduct", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.Add("@productId", SqlDbType.Int).Value = idProduct;
                    con.Open();
                    SqlDataReader dr = await sql.ExecuteReaderAsync();

                    if (dr.HasRows)
                    {
                        var col0 = dr.GetOrdinal("promotion_id");
                        var col1 = dr.GetOrdinal("title");
                        var col2 = dr.GetOrdinal("description");
                        var col3 = dr.GetOrdinal("start_date");
                        var col4 = dr.GetOrdinal("end_date");
                        var col5 = dr.GetOrdinal("discount");
                        var col6 = dr.GetOrdinal("color");
                        var col7 = dr.GetOrdinal("image_url");
                        var col8 = dr.GetOrdinal("product_id");

                        while (dr.Read())
                        {
                            var model = new PromotionsDto()
                            {
                                idPromotion = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                                title = dr.IsDBNull(col1) ? string.Empty : dr.GetString(col1),
                                description = dr.IsDBNull(col2) ? string.Empty : dr.GetString(col2),
                                startDate = dr.IsDBNull(col3) ? DateTime.Now : dr.GetDateTime(col3),
                                endDate = dr.IsDBNull(col4) ? DateTime.Now : dr.GetDateTime(col4),
                                discount = dr.IsDBNull(col5) ? 0 : dr.GetDecimal(col5),
                                color = dr.IsDBNull(col6) ? string.Empty : dr.GetString(col6),
                                imageUrl = dr.IsDBNull(col7) ? string.Empty : dr.GetString(col7),
                                idProduct = dr.IsDBNull(col8) ? 0 : dr.GetInt32(col8),

                            };
                            promotionsList.Add(model);
                        }
                    }

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException!).Throw();
            }
            return promotionsList;
        }

        public async Task<BaseResponseDto> SavePromotions(PromotionsViewModels promotionsViewModels)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_insertPromotions", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@IdPromotion", promotionsViewModels.idPromotion);
                    sql.Parameters.AddWithValue("@Title", promotionsViewModels.title);
                    sql.Parameters.AddWithValue("@Description", promotionsViewModels.description);
                    sql.Parameters.AddWithValue("@StartDate", promotionsViewModels.startDate);
                    sql.Parameters.AddWithValue("@EndDate", promotionsViewModels.endDate);
                    sql.Parameters.AddWithValue("@ImageUrl", promotionsViewModels.base64Url);
                    sql.Parameters.AddWithValue("@Discount", promotionsViewModels.discount);
                    sql.Parameters.AddWithValue("@IdProduct", promotionsViewModels.idProduct);
                    sql.Parameters.AddWithValue("@IdCategory", promotionsViewModels.idCategory);
                    sql.Parameters.AddWithValue("@State", promotionsViewModels.state);
                    sql.Parameters.AddWithValue("@Color", promotionsViewModels.color);
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