using System;
using AR.Common.Dto;

namespace AR.Core.Purchase.Common.Interfaces;

public interface IPurchaseApplication
{
    Task<ResponseDto<BaseResponseDto>> SaveOrder(InvoiceDto invoice);
}
