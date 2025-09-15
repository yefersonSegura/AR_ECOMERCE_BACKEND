using System.Data;
using System.Data.SqlClient;
using System.Runtime.ExceptionServices;
using AR.Common.Dto;
using AR.Common.ViewModels;
using AR.Core.Common.Dto;
using AR.Core.Common.Interfaces;
using AR.Repository.Base;

namespace AR.Transaction.Repository;

public class TransactionRepository : EntityRepository, ITransactionRepository
{
    public TransactionRepository(string connectionString) : base(connectionString)
    {
    }

    public async Task<List<TransactionActivityDto>> GetActivities(int transactionId)
    {
        List<TransactionActivityDto> result = new List<TransactionActivityDto>();
        try
        {
            using (var con = new SqlConnection(connectionString))
            {
                SqlCommand sql = new SqlCommand("sp_GetActivities", con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@transactionId", transactionId);
                con.Open();
                SqlDataReader dr = await sql.ExecuteReaderAsync();
                if (dr.HasRows)
                {
                    var col0 = dr.GetOrdinal("ActivityId");
                    var col1 = dr.GetOrdinal("TransactionId");
                    var col2 = dr.GetOrdinal("TransactionName");
                    var col3 = dr.GetOrdinal("GroupKey");
                    var col4 = dr.GetOrdinal("sort");
                    var col5 = dr.GetOrdinal("ActivityKey");
                    var col6 = dr.GetOrdinal("ActivityName");

                    while (dr.Read())
                    {
                        var model = new TransactionActivityDto()
                        {
                            ActivityId = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                            ActivityKey = dr.IsDBNull(col5) ? string.Empty : dr.GetString(col5),
                            ActivityName = dr.IsDBNull(col6) ? string.Empty : dr.GetString(col6),
                            Sort = dr.IsDBNull(col4) ? 0 : dr.GetInt32(col4),
                            TransactionId = dr.IsDBNull(col1) ? 0 : dr.GetInt32(col1),
                            TransactionName = dr.IsDBNull(col2) ? "" : dr.GetString(col2)
                        };
                        result.Add(model);
                    }
                }
                con.Close();

            }
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
        }
        return result;
    }

    public async Task<ResponseDto<int>> NextNumber(int documentTypeId)
    {
        ResponseDto<int> result = new ResponseDto<int>();
        try
        {
            using (var con = new SqlConnection(connectionString))
            {
                SqlCommand sql = new SqlCommand("sp_IncrementNumber", con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@id_typeDocument", documentTypeId);
                con.Open();
                SqlDataReader dr = await sql.ExecuteReaderAsync();
                if (dr.HasRows)
                {
                    var col0 = dr.GetOrdinal("Codigo");
                    var col1 = dr.GetOrdinal("Mensaje");
                    var col2 = dr.GetOrdinal("ESTADO");
                    var col3 = dr.GetOrdinal("CurrentNumber");
                    while (dr.Read())
                    {
                        result = new ResponseDto<int>()
                        {
                            Message = dr.IsDBNull(col1) ? "" : dr.GetString(col1),
                            Data = dr.IsDBNull(col3) ? 0 : dr.GetInt32(col3),
                            IsSuccessful = dr.IsDBNull(col2) ? false : dr.GetBoolean(col2),
                        };
                    }
                }
                con.Close();

            }
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
        }
        return result;
    }

    public async Task<DocumentTypeByOperationDto> GetDocumentTransaction(int idOperation, string codeDocumentType)
    {
        DocumentTypeByOperationDto result = new DocumentTypeByOperationDto();
        try
        {
            using (var con = new SqlConnection(connectionString))
            {
                SqlCommand sql = new SqlCommand("sp_GetDocumentTypeByOperation", con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@IdOperation", idOperation);
                sql.Parameters.AddWithValue("@code", codeDocumentType);
                con.Open();
                SqlDataReader dr = await sql.ExecuteReaderAsync();
                if (dr.HasRows)
                {
                    var col0 = dr.GetOrdinal("id");
                    var col1 = dr.GetOrdinal("serial");
                    var col2 = dr.GetOrdinal("number");
                    var col3 = dr.GetOrdinal("DocumentTypeId");
                    var col4 = dr.GetOrdinal("DocumentTypeCode");
                    var col5 = dr.GetOrdinal("name_document");
                    while (dr.Read())
                    {
                        result = new DocumentTypeByOperationDto
                        {
                            Id = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                            Serial = dr.IsDBNull(col1) ? "" : dr.GetString(col1),
                            Number = dr.IsDBNull(col2) ? 0 : dr.GetInt32(col2),
                            DocumentTypeCode = dr.IsDBNull(col4) ? "" : dr.GetString(col4),
                            DocumentTypeId = dr.IsDBNull(col3) ? 0 : dr.GetInt32(col3),
                            NameDocument = dr.IsDBNull(col5) ? "" : dr.GetString(col5),
                        };
                    }
                }
                con.Close();

            }
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
        }
        return result;
    }

    public async Task<TransactionDto> GetTransaction(int transactionId)
    {
        TransactionDto result = new TransactionDto();
        try
        {
            using (var con = new SqlConnection(connectionString))
            {
                SqlCommand sql = new SqlCommand("sp_GetOperationById", con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@transactionId", transactionId);
                con.Open();
                SqlDataReader dr = await sql.ExecuteReaderAsync();
                if (dr.HasRows)
                {
                    var col0 = dr.GetOrdinal("TransactionId");
                    var col1 = dr.GetOrdinal("Name");
                    var col2 = dr.GetOrdinal("GroupKey");
                    while (dr.Read())
                    {
                        result = new TransactionDto()
                        {
                            TransactionId = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                            Name = dr.IsDBNull(col1) ? string.Empty : dr.GetString(col1),
                            GroupKey = dr.IsDBNull(col2) ? string.Empty : dr.GetString(col2),
                        };
                    }
                }
                con.Close();

            }
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
        }
        return result;
    }

    public async Task<DropdownListItemViewModel> GetTransactionByGroupKey(string groupKey)
    {
        DropdownListItemViewModel result = new DropdownListItemViewModel();
        try
        {
            using (var con = new SqlConnection(connectionString))
            {
                SqlCommand sql = new SqlCommand("sp_GetOperationByGroupKey", con);
                sql.CommandType = CommandType.StoredProcedure;
                sql.Parameters.AddWithValue("@groupKey", groupKey);
                con.Open();
                SqlDataReader dr = await sql.ExecuteReaderAsync();
                if (dr.HasRows)
                {
                    var col0 = dr.GetOrdinal("TransactionId");
                    var col1 = dr.GetOrdinal("Name");
                    var col2 = dr.GetOrdinal("Code");
                    while (dr.Read())
                    {
                        result = new DropdownListItemViewModel()
                        {
                            Value = dr.IsDBNull(col0) ? 0 : dr.GetInt32(col0),
                            Code = dr.IsDBNull(col1) ? string.Empty : dr.GetString(col1),
                            Label = dr.IsDBNull(col2) ? string.Empty : dr.GetString(col2),
                        };
                    }
                }
                con.Close();

            }
        }
        catch (Exception ex)
        {
            ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
        }
        return result;
    }
}
