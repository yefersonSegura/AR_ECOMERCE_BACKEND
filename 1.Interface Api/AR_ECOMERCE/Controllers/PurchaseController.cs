using AR.Common.Dto;
using AR.Core.Purchase.Common.Dto;
using AR.Core.Purchase.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AR_ECOMERCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IShoppingCartApplication shoppingCartApplication;
        private readonly IPurchaseApplication purchaseApplication;
        public PurchaseController(IShoppingCartApplication shoppingCartApplication, IPurchaseApplication purchaseApplication)
        {
            this.shoppingCartApplication = shoppingCartApplication;
            this.purchaseApplication = purchaseApplication;
        }

        [HttpGet("GetShoppingCartByCustomer")]
        public Task<ResponseDto<List<ProductDto>>> GetShoppingCartByCustomer([FromQuery] int customerId)
        {
            return shoppingCartApplication.GetShoppingCartByCustomer(customerId);
        }
        [HttpGet("AddProductToShoppingCart")]
        public Task<BaseResponseDto> AddProductToShoppingCart([FromQuery] int customerId, [FromQuery] int productId, [FromQuery] int quantity)
        {
            return shoppingCartApplication.AddProductToShoppingCart(customerId, productId, quantity);
        }
        [HttpDelete("RemoveProductOfShoppingCart")]
        public Task<BaseResponseDto> RemoveProductOfShoppingCart([FromQuery] int customerId, [FromQuery] int productId)
        {
            return shoppingCartApplication.RemoveProductOfShoppingCart(customerId, productId);
        }
        [HttpPost("CalulateCart")]
        public BaseResponseDto CalulateCart([FromBody] InvoiceDto invoice)
        {
            return shoppingCartApplication.calulateCart(invoice);
        }
        [HttpPost("SaveOrder")]
        public Task<ResponseDto<BaseResponseDto>> SaveOrder([FromBody]InvoiceDto invoice)
        {
            return purchaseApplication.SaveOrder(invoice);
        }
    }
}
