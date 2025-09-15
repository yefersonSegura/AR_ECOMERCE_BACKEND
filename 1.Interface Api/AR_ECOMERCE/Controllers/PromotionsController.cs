using AR.Common.Dto;
using AR.Core.Promotions.Common.Dto;
using AR.Core.Promotions.Common.Interfaces;
using AR.Core.Promotions.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AR_ECOMERCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IPromotionsApplication promotionsApplication;
        public PromotionsController(IPromotionsApplication promotionsApplication)
        {
            this.promotionsApplication = promotionsApplication;
        }

        [HttpGet("GetPromotions")]
        public Task<ResponseDto<List<PromotionsDto>>> GetPromotions([FromQuery] int idPromotions)
        {
            return promotionsApplication.GetPromotions(idPromotions);
        }
        [HttpGet("GetPromotionsByProductId")]
        public Task<ResponseDto<List<PromotionsDto>>> GetPromotionsByProductId([FromQuery] int idProduct)
        {
            return promotionsApplication.GetPromotionsByProductId(idProduct);
        }
        
        [HttpPost("SavePromotions")]
        public Task<BaseResponseDto> SavePromotions([FromBody] PromotionsViewModels promotionsViewModels)
        {
            return promotionsApplication.SavePromotions(promotionsViewModels);
        }

        [HttpDelete("DeletePromotions")]
        public Task<BaseResponseDto> DeletePromotions([FromQuery] int promotionsID)
        {
            return promotionsApplication.DeletePromotions(promotionsID);
        }

    }
}