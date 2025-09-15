using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;
using AR.Common.Dto;
using AR.Core.Purchase.Common.Interfaces;
using AR.Core.Purchase.Common.ViewModels;
using AR.Repository.Base;

namespace AR.Purchase.Repository;

public class PurchaseRepository : EntityRepository, IPurchaseRepository
{
    public PurchaseRepository(string connectionString) : base(connectionString)
    {

    }

    public async Task<BaseResponseDto> SaveDetailOrder(CreateOrderDetailViewModel model)
    {
        BaseResponseDto result = new BaseResponseDto();
        try
        {
            using (var con = new SqlConnection(connectionString))
            {
                SqlCommand sql = new SqlCommand("sp_OrderDetail_Insert", con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@order_id", model.OrderId);
                sql.Parameters.AddWithValue("@product_id", model.ProductId);
                sql.Parameters.AddWithValue("@quantity", model.Quantity);
                sql.Parameters.AddWithValue("@unit_price", model.UnitPrice);
                sql.Parameters.AddWithValue("@subtotal", model.Subtotal);
                sql.Parameters.AddWithValue("@discount", model.Discount);
                sql.Parameters.AddWithValue("@totalDiscount", model.TotalDiscount);
                sql.Parameters.AddWithValue("@LineDiscount", model.LineDiscount);
                sql.Parameters.AddWithValue("@MontoVentaIgv", model.MontoVentaIgv);
                await con.OpenAsync();
                SqlDataReader dr = await sql.ExecuteReaderAsync();
                while (dr.Read())
                {
                    result.Message = dr.GetString(1);
                    result.IsSuccessful = dr.GetBoolean(2);
                }
                await con.CloseAsync();
            }
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
        }
        return result;
    }

    public async Task<ResponseDto<int>> SaveOrder(CreateOrderViewModel model)
    {
        ResponseDto<int> response = new ResponseDto<int>();
        try
        {
            using (var con = new SqlConnection(connectionString))
            {
                SqlCommand sql = new SqlCommand("sp_saveOrder", con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@user_id", model.UserId);
                sql.Parameters.AddWithValue("@order_date", model.OrderDate);
                sql.Parameters.AddWithValue("@total", model.Total);
                sql.Parameters.AddWithValue("@status", model.Status);
                sql.Parameters.AddWithValue("@shipping_address", model.ShippingAddress);
                sql.Parameters.AddWithValue("@payment_method", model.PaymentMethod);
                sql.Parameters.AddWithValue("@GrossTotal", model.GrossTotal);
                sql.Parameters.AddWithValue("@totalDiscount", model.TotalDiscount);
                sql.Parameters.AddWithValue("@TotalIGV", model.TotalIGV);
                sql.Parameters.AddWithValue("@IdOperation", model.IdOperation);
                sql.Parameters.Add("@Serial", SqlDbType.Char, 4).Value = model.Serial ?? "";
                sql.Parameters.AddWithValue("@Number", model.Number);
                sql.Parameters.AddWithValue("@TypeDocumentId", model.TypeDocumentId);
                await con.OpenAsync();
                SqlDataReader dr = await sql.ExecuteReaderAsync();
                while (dr.Read())
                {
                    response.Message = dr.GetString(1);
                    response.IsSuccessful = dr.GetBoolean(2);
                    response.Data = dr.GetInt32(3);
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
