using AR.Common.Dto;
using AR.Common.Functions;
using AR.Common.InterfacesFirabase;
using AR.Core.Promotions.Common.Dto;
using AR.Core.Promotions.Common.Interfaces;
using AR.Core.Promotions.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.Promotions.Application
{
    public class PromotionsApplication : IPromotionsApplication
    {
        private readonly IPromotionsRepository promotionsRepository;
        private readonly IFirebaseFN firebaseFN1;
        public PromotionsApplication(IPromotionsRepository promotionsRepository, IFirebaseFN firebaseFN1)
        {
            this.firebaseFN1 = firebaseFN1;
            this.promotionsRepository = promotionsRepository;
        }

        public async Task<BaseResponseDto> DeletePromotions(int promotionsID)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                result = await promotionsRepository.DeletePromotions(promotionsID);
            }
            catch (Exception ex)
            {
                result.Status = ex.HResult;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<ResponseDto<List<PromotionsDto>>> GetPromotions(int idPromotions)
        {
            ResponseDto<List<PromotionsDto>> result = new ResponseDto<List<PromotionsDto>>();
            try
            {
                result.Data = await promotionsRepository.GetPromotions(idPromotions);
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }

        public Task<ResponseDto<List<PromotionsDto>>> GetPromotionsByProductId(int idProduct)
        {
            ResponseDto<List<PromotionsDto>> result = new ResponseDto<List<PromotionsDto>>();
            try
            {
                result.Data = promotionsRepository.GetPromotionsByProductId(idProduct).Result;
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return Task.FromResult(result);
        }

        public async Task<BaseResponseDto> SavePromotions(PromotionsViewModels promotionsViewModels)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                FunctionApp fn = new FunctionApp();
                if (!string.IsNullOrEmpty(promotionsViewModels.base64Url))
                {
                    byte[] imageBytes = Convert.FromBase64String(promotionsViewModels.base64Url);
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
                        promotionsViewModels.base64Url = resultImage.Data;
                    }
                }
                result = await promotionsRepository.SavePromotions(promotionsViewModels);
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }
    }
}