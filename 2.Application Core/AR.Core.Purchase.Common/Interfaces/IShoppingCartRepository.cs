using AR.Common.Dto;
using AR.Core.Purchase.Common.Dto;
using AR.Core.Purchase.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.Purchase.Common.Interfaces
{
    public interface IShoppingCartRepository
    {
        Task<List<ProductDto>> GetShoppingCartByCustomer(int CustomerId);
        Task<BaseResponseDto> AddProductToShoppingCart(int customerId, int productId, int quantity);
        Task<BaseResponseDto> RemoveProductOfShoppingCart(int customerId, int productId);
    }
}
