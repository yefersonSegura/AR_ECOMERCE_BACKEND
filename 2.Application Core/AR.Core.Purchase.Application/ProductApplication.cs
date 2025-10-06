using AR.Common.Dto;
using AR.Common.Functions;
using AR.Common.InterfacesFirabase;
using AR.Core.Common.ViewModels;
using AR.Core.Purchase.Common.Dto;
using AR.Core.Purchase.Common.Interfaces;
using AR.Core.Purchase.Common.ViewModels;
using System.Runtime.ExceptionServices;

namespace AR.Core.Purchase.Application
{
    public class ProductApplication : IProductApplication, ICategoryApplication
    {

        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IFirebaseFN firebaseFN1;

        public ProductApplication(IProductRepository productRepository, ICategoryRepository categoryRepository, IFirebaseFN firebaseFN1)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.firebaseFN1 = firebaseFN1;
        }

        public async Task<ResponseDto<List<ProductDto>>> GetIdProducts(int productID)
        {
            ResponseDto<List<ProductDto>> result = new ResponseDto<List<ProductDto>>();
            try
            {
                result = await productRepository.GetIdProducts(productID);
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }

        public async Task<BaseResponseDto> SaveProducts(ProductViewModels productViewModels)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                FunctionApp fn = new FunctionApp();
                if (!string.IsNullOrEmpty(productViewModels.base64Url))
                {
                    byte[] imageBytes = Convert.FromBase64String(productViewModels.base64Url);
                    using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                    {
                        var resultImage = await firebaseFN1.UploadStorage(memoryStream, "imagesProducts", $"{fn.GetRandomNumber(13)}.jpg");
                        if (!resultImage.IsSuccessful)
                        {
                            result.Status = resultImage.Status;
                            result.IsSuccessful = resultImage.IsSuccessful;
                            result.Message = resultImage.Message;
                            return result;
                        }
                        productViewModels.urlImage = resultImage.Data;
                    }
                }
                result = await productRepository.SaveProducts(productViewModels);
            }
            catch (Exception ex)
            {
                result.Status = ex.HResult;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<BaseResponseDto> DeleteProducts(int productID)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                result = await productRepository.DeleteProducts(productID);
            }
            catch (Exception ex)
            {
                result.Status = ex.HResult;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<BaseResponseDto> DeleteCategories(int categoryID)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                result = await categoryRepository.DeleteCategories(categoryID);
            }
            catch (Exception ex)
            {
                result.Status = ex.HResult;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<PagedResponseDto<CategoryDto>> GetCategory(QueryCategoriesViewModel query)
        {
            PagedResponseDto<CategoryDto> result = new PagedResponseDto<CategoryDto>();
            try
            {
                result = await categoryRepository.GetCategory(query);
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }

        public async Task<BaseResponseDto> SaveCategories(CategoryViewModels categoryViewModels)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                result = await categoryRepository.SaveCategories(categoryViewModels);
            }
            catch (Exception ex)
            {
                result.Status = ex.HResult;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResponseDto<List<ProductDto>>> ListProducts(int idCategory, bool? isFeatured)
        {
            ResponseDto<List<ProductDto>> result = new ResponseDto<List<ProductDto>>();
            try
            {
                 result = await productRepository.ListProducts(idCategory,isFeatured);
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }

        public async Task<ResponseDto<List<PromotionDto>>> GetPromotion(DateTime date)
        {
            ResponseDto<List<PromotionDto>> result = new ResponseDto<List<PromotionDto>>();
            try
            {
                result.Data = await productRepository.GetPromotion(date);
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }
    }
}