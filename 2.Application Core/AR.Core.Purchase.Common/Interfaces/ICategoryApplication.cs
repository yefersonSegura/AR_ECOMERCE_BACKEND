using AR.Common.Dto;
using AR.Core.Common.ViewModels;
using AR.Core.Purchase.Common.Dto;
using AR.Core.Purchase.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.Purchase.Common.Interfaces
{
    public interface ICategoryApplication
    {
        Task<PagedResponseDto<CategoryDto>> GetCategory(QueryCategoriesViewModel query);
        Task<BaseResponseDto> SaveCategories(CategoryViewModels categoryViewModels);
        Task<BaseResponseDto> DeleteCategories(int categoryID);
        
    }
}
