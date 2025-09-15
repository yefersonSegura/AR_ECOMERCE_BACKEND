using System;
using AR.Common.Dto;
using AR.Common.ViewModels;
using AR.Core.Common.Dto;

namespace AR.Core.Common.Interfaces;

public interface ITransactionRepository
{
    Task<TransactionDto> GetTransaction(int transactionId);
    Task<DropdownListItemViewModel> GetTransactionByGroupKey(string groupKey);
    Task<List<TransactionActivityDto>> GetActivities(int transactionId);
    Task<ResponseDto<int>> NextNumber(int documentTypeId);
    Task<DocumentTypeByOperationDto> GetDocumentTransaction(int idOperation, string codeDocumentType);
}
