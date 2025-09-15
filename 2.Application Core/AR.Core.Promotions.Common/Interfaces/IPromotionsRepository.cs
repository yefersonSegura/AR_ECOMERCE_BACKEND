using AR.Common.Dto;
using AR.Core.Promotions.Common.Dto;
using AR.Core.Promotions.Common.ViewModels;

namespace AR.Core.Promotions.Common.Interfaces
{
    public interface IPromotionsRepository
    {
        Task<List<PromotionsDto>> GetPromotions(int idPromotions);
        Task<List<PromotionsDto>> GetPromotionsByProductId(int idProduct);
        Task<BaseResponseDto> SavePromotions(PromotionsViewModels promotionsViewModels);
        Task<BaseResponseDto> DeletePromotions(int promotionsID);
    }
}