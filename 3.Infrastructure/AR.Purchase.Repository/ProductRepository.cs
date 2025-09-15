using AR.Common.Dto;
using AR.Core.Purchase.Common.Dto;
using AR.Core.Purchase.Common.Interfaces;
using AR.Core.Purchase.Common.ViewModels;
using AR.Repository.Base;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;

namespace AR.Purchase.Repository
{
    public class ProductRepository : EntityRepository, IProductRepository , ICategoryRepository
    {
        public ProductRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<ResponseDto<List<ProductDto>>> GetIdProducts(int productID)
        {
            var productList = new ResponseDto<List<ProductDto>>();
            productList.Data = new List<ProductDto>();
            var response = new ResponseDto<List<ProductDto>>();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_listProducts", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    //sql.Parameters.Add("IdCategoria", idCategoria);
                    sql.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
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

                        while (dr.Read())
                        {
                            var model = new ProductDto()
                            {
                                productID = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                                name = dr.IsDBNull(col1) ? string.Empty : dr.GetString(col1),
                                description = dr.IsDBNull(col2) ? string.Empty : dr.GetString(col2),
                                price = dr.IsDBNull(col3) ? 0 : dr.GetDecimal(col3),
                                stock = dr.IsDBNull(col4) ? 0 : dr.GetInt32(col4),
                                categoryID = dr.IsDBNull(col5) ? 0 : dr.GetInt32(col5),
                                urlImage = dr.IsDBNull(col7) ? string.Empty : dr.GetString(col7),
                                categoryName = dr.IsDBNull(col8) ? string.Empty : dr.GetString(col8),

                            };
                            productList.Data.Add(model);
                        }
                    }

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException!).Throw();
            }
            return productList;
        }

        public async Task<BaseResponseDto> SaveProducts(ProductViewModels productViewModels)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_insertProducts", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@IdProducts", productViewModels.productID);
                    sql.Parameters.AddWithValue("@Name", productViewModels.name);
                    sql.Parameters.AddWithValue("@Description", productViewModels.description);
                    sql.Parameters.AddWithValue("@Price", productViewModels.price);
                    sql.Parameters.AddWithValue("@Stock", productViewModels.stock);
                    sql.Parameters.AddWithValue("@CategoryID", productViewModels.categoryID);
                    sql.Parameters.AddWithValue("@UrlImage", productViewModels.urlImage);
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
            return  response;
        }

        public async Task<BaseResponseDto> DeleteProducts(int productID)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_deleteProducts", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@IdProducts", productID);
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


        public async Task<BaseResponseDto> DeleteCategories(int categoryID)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_deleteCategories", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@IdCategory", categoryID);
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

        public async Task<List<CategoryDto>> GetCategory()
        {
            var categoryList = new List<CategoryDto>();
            var response = new ResponseDto<List<CategoryDto>>();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_listCategories", con);
                    sql.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader dr = await sql.ExecuteReaderAsync();

                    if (dr.HasRows)
                    {
                        var col0 = dr.GetOrdinal("CategoryID");
                        var col1 = dr.GetOrdinal("CategoryName");
                        var col2 = dr.GetOrdinal("Description");

                        while (dr.Read())
                        {
                            var model = new CategoryDto()
                            {
                                categoryID = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                                categoryName = dr.IsDBNull(col1) ? string.Empty : dr.GetString(col1),
                                Description = dr.IsDBNull(col2) ? string.Empty : dr.GetString(col2)
                            };
                            categoryList.Add(model);
                        }
                    }

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException!).Throw();
            }
            return categoryList;
        }

        public async Task<BaseResponseDto> SaveCategories(CategoryViewModels categoryViewModels)
        {
            BaseResponseDto response = new BaseResponseDto();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_insertCategories", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@IDCategory", categoryViewModels.categoryID);
                    sql.Parameters.AddWithValue("@Description", categoryViewModels.Description);
                    sql.Parameters.AddWithValue("@CategoryName", categoryViewModels.categoryName);
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

        public async Task<ResponseDto<List<ProductDto>>> ListProducts(int idCategory, bool? isFeatured)
        {
            var productList = new ResponseDto<List<ProductDto>>();
            productList.Data = new List<ProductDto>();
            var response = new ResponseDto<List<ProductDto>>();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("sp_getProducts", con);
                    sql.CommandType = CommandType.StoredProcedure;

                    sql.Parameters.Add("@IdCategoria", SqlDbType.Int).Value = idCategory;
                    sql.Parameters.Add("@IsFeatured", SqlDbType.Bit).Value = isFeatured;
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

                        while (dr.Read())
                        {
                            var model = new ProductDto()
                            {
                                productID = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                                name = dr.IsDBNull(col1) ? string.Empty : dr.GetString(col1),
                                description = dr.IsDBNull(col2) ? string.Empty : dr.GetString(col2),
                                price = dr.IsDBNull(col3) ? 0 : dr.GetDecimal(col3),
                                stock = dr.IsDBNull(col4) ? 0 : dr.GetInt32(col4),
                                categoryID = dr.IsDBNull(col5) ? 0 : dr.GetInt32(col5),
                                urlImage = dr.IsDBNull(col7) ? "" : dr.GetString(col7),
                                categoryName = dr.IsDBNull(col8) ? "" : dr.GetString(col8),
                            };
                            productList.Data.Add(model);
                        }
                    }
                    productList.IsSuccessful = true;

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException!).Throw();
            }
            return productList;
        }

        public async  Task<List<PromotionDto>> GetPromotion(DateTime date)
        {
            var promotionList = new List<PromotionDto>();
            try
            {
                using (var con = new SqlConnection(connectionString))
                {
                    SqlCommand sql = new SqlCommand("GetPromotionByDate", con);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@date", date);

                    con.Open();
                    SqlDataReader dr = await sql.ExecuteReaderAsync();

                    if (dr.HasRows)
                    {
                        var col0 = dr.GetOrdinal("IdPromotion");
                        var col1 = dr.GetOrdinal("Title");
                        var col2 = dr.GetOrdinal("Description");
                        var col3 = dr.GetOrdinal("StartDate");
                        var col4 = dr.GetOrdinal("EndDate");
                        var col5 = dr.GetOrdinal("IdCategory");
                        var col6 = dr.GetOrdinal("IdProduct");
                        var col7 = dr.GetOrdinal("Discount");
                        var col8 = dr.GetOrdinal("Color");
                        var col9 = dr.GetOrdinal("ImageUrl");

                        while (dr.Read())
                        {
                            var model = new PromotionDto()
                            {
                                IdPromotion = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                                Title = dr.IsDBNull(col1) ? string.Empty : dr.GetString(col1),
                                Description = dr.IsDBNull(col2) ? string.Empty : dr.GetString(col2),
                                StartDate= dr.IsDBNull(col3)?null:dr.GetDateTime(col3),
                                EndDate=  dr.IsDBNull(col4)?null:dr.GetDateTime(col4),
                                CategoryId= dr.IsDBNull(col5)?0:dr.GetInt32(col5),
                                ProductId=dr.IsDBNull(col6)?0:dr.GetInt32(col6),
                                Discount=dr.IsDBNull(col7)?0:dr.GetDecimal(col7),
                                Color=dr.IsDBNull(col8)?"":dr.GetString(col8),
                                UrlImage = dr.IsDBNull(col9) ? "" : dr.GetString(col9)
                            };
                            promotionList.Add(model);
                        }
                    }

                    con.Close();

                }
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex.InnerException!).Throw();
            }
            return promotionList;
        }
    }
}
