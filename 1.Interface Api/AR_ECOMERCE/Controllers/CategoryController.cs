using AR.Common.Dto;
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
        public CategoryController( ICategoryApplication categoryApplication)
        {
            this.categoryApplication = categoryApplication;
        }

        [HttpGet("GetCategories")]
        public Task<ResponseDto<List<CategoryDto>>> GetCategories()
        {
            return categoryApplication.GetCategory();
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