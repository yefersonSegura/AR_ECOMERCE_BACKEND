using AR.Common.Dto;
using AR.Core.Common.ViewModels;
using AR.Core.Purchase.Common.Dto;
using AR.Core.Purchase.Common.Interfaces;
using AR.Core.Purchase.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AR_ECOMERCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryApplication categoryApplication;
        public CategoryController(ICategoryApplication categoryApplication)
        {
            this.categoryApplication = categoryApplication;
        }

        [HttpPost("GetCategories")]
        public async Task<PagedResponseDto<CategoryDto>> GetCategory([FromBody]QueryCategoriesViewModel query)
        {
            return await categoryApplication.GetCategory(query);
        }

        [HttpPost("SaveCategories")]
        public Task<BaseResponseDto> SaveCategories([FromBody] CategoryViewModels categoryViewModels)
        {
            return categoryApplication.SaveCategories(categoryViewModels);
        }

        [HttpDelete("DeleteCategories")]
        public Task<BaseResponseDto> DeleteCategories([FromQuery] int categoryID)
        {
            return categoryApplication.DeleteCategories(categoryID);
        }

    }
}