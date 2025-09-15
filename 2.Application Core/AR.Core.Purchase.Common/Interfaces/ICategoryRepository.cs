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
    public interface ICategoryRepository
    {
        Task<List<CategoryDto>> GetCategory();
        Task<BaseResponseDto> SaveCategories(CategoryViewModels categoryViewModels);
        Task<BaseResponseDto> DeleteCategories(int categoryID);
    }
}
