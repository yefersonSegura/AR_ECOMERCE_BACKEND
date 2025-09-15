using AR.Common.Dto;
using AR.Core.Purchase.Common.Dto;
using AR.Core.Purchase.Common.ViewModels;

namespace AR.Core.Purchase.Common.Interfaces
{
    public interface IProductRepository
    {
        Task<ResponseDto<List<ProductDto>>> GetIdProducts(int productID);
        Task<BaseResponseDto> SaveProducts(ProductViewModels productViewModels);
        Task<BaseResponseDto> DeleteProducts(int productID);
        Task<ResponseDto<List<ProductDto>>> ListProducts(int idCategory, bool? isFeatured);
        Task<List<PromotionDto>> GetPromotion(DateTime date);
    }
}