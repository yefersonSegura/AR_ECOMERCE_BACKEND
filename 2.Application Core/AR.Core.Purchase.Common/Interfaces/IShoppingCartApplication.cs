using AR.Common.Dto;
using AR.Core.Purchase.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.Purchase.Common.Interfaces
{
    public interface IShoppingCartApplication
    {
        Task<ResponseDto<List<ProductDto>>> GetShoppingCartByCustomer(int CustomerId);
        Task<BaseResponseDto> AddProductToShoppingCart(int customerId, int productId, int quantity);
        Task<BaseResponseDto> RemoveProductOfShoppingCart(int customerId, int productId);
        ResponseDto<InvoiceDto> calulateCart(InvoiceDto invoice);
    }
}
