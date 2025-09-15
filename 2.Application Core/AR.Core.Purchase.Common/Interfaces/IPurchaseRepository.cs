using System;
using AR.Common.Dto;
using AR.Core.Purchase.Common.ViewModels;

namespace AR.Core.Purchase.Common.Interfaces;

public interface IPurchaseRepository
{
    Task<ResponseDto<int>> SaveOrder(CreateOrderViewModel model);
    Task<BaseResponseDto> SaveDetailOrder(CreateOrderDetailViewModel item);
}
