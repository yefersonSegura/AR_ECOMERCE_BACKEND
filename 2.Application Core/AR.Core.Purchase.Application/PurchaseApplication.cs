using System;
using Ar.Common.Transaction;
using AR.Common.Dto;
using AR.Core.Common.Interfaces;
using AR.Core.Purchase.Common.Interfaces;

namespace AR.Core.Purchase.Application;

public class PurchaseApplication : IPurchaseApplication
{
    private readonly ITransactionManager transactionManager;
    private readonly ITransactionRepository transactionRepository;
    private readonly IShoppingCartApplication shoppingCartApplication;
    public PurchaseApplication(ITransactionManager transactionManager, ITransactionRepository transactionRepository, IShoppingCartApplication shoppingCartApplication)
    {
        this.transactionManager = transactionManager;
        this.transactionRepository = transactionRepository;
        this.shoppingCartApplication = shoppingCartApplication;
    }
    public async Task<ResponseDto<BaseResponseDto>> SaveOrder(InvoiceDto invoice)
    {
        ResponseDto<BaseResponseDto> result = new ResponseDto<BaseResponseDto>();
        try
        {
            var resultTransaction = await transactionRepository.GetTransactionByGroupKey("boletaventa");
            if (resultTransaction.Id == 0)
            {
                result.Message = "Error al indetificar el tipo de proceso";
                return result;
            }

            invoice.transaction = new AR.Common.domain.TransactionInfo
            {
                Operation = new AR.Common.domain.OperationDto()
                {
                    id = (int)resultTransaction.Value!,
                    name = resultTransaction.Code,
                }
            };

            invoice.transaction.DocumentType = new DocumentTypeByOperationDto() { DocumentTypeCode = "BOL" };

            var response = await transactionManager.Process(invoice);
            if (!response.IsSuccessful)
            {
                result.Message = response.Message;
                return result;
            }

            foreach (var item in invoice.items)
            {
                await shoppingCartApplication.RemoveProductOfShoppingCart(invoice.userId, item.productID);
            }

            result.IsSuccessful = response.IsSuccessful;
            result.Message = response.Message;
        }
        catch (Exception ex)
        {
            result.Status = ex.HResult;
            result.Message = ex.Message;
        }
        return result;
    }
}
