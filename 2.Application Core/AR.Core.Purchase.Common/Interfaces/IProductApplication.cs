using AR.Common.Dto;
using AR.Core.Purchase.Common.Dto;
using AR.Core.Purchase.Common.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AR.Core.Purchase.Common.Interfaces
{
    public interface IProductApplication
    {
        Task<ResponseDto<List<ProductDto>>> GetIdProducts(int productID);
        Task<BaseResponseDto> SaveProducts(ProductViewModels productViewModels);
        Task<BaseResponseDto> DeleteProducts(int productID);
        Task<ResponseDto<List<ProductDto>>> ListProducts(int idCategory, bool? isFeatured);
        Task<ResponseDto<List<PromotionDto>>> GetPromotion(DateTime date);
    }
}