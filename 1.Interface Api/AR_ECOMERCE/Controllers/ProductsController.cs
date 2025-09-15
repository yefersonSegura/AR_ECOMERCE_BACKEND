using AR.Common.Dto;
using AR.Core.Purchase.Common.Dto;
using AR.Core.Purchase.Common.Interfaces;
using AR.Core.Purchase.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AR_ECOMERCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductApplication productApplication;

        public ProductsController(IProductApplication productApplication)
        {
            this.productApplication = productApplication;
        }

        [HttpGet("GetIdProducts")]
        public Task<ResponseDto<List<ProductDto>>> GetIdProducts([FromQuery] int productID)
        {
            return productApplication.GetIdProducts(productID);
        }

        [HttpGet("ListProducts")]
        public Task<ResponseDto<List<ProductDto>>> ListProducts([FromQuery] int idCategory,[FromQuery] bool? isFeatured)
        {
            return productApplication.ListProducts(idCategory, isFeatured);
        }

        [HttpPost("SaveProducts")]
        public Task<BaseResponseDto> SaveProducts([FromBody] ProductViewModels productViewModels)
        {
            return productApplication.SaveProducts(productViewModels);
        }

        [HttpDelete("DeleteProducts")]
        public Task<BaseResponseDto> DeleteProducts([FromQuery] int productID)
        {
            return productApplication.DeleteProducts(productID);
        }
        [HttpGet("GetPromotions")]
        public Task<ResponseDto<List<PromotionDto>>> GetPromotions([FromQuery]DateTime date)
        {
            return productApplication.GetPromotion(date);
        }

    }
}