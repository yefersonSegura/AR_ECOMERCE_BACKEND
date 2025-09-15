using AR.Common.Dto;
using AR.Common.Functions;
using AR.Common.InterfacesFirabase;
using AR.Core.Customer.Common.Dto;
using AR.Core.Customer.Common.Interfaces;
using AR.Core.Customer.Common.ViewModels;
using System.Runtime.ExceptionServices;

namespace AR.Core.Customer.Application
{
    public class CustomerApplication : ICustomerApplication
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IFirebaseFN firebaseFN1;
        public CustomerApplication(ICustomerRepository customerRepository, IFirebaseFN firebaseFN1)
        {
            this.customerRepository = customerRepository;
            this.firebaseFN1 = firebaseFN1;
        }

        public async Task<ResponseDto<List<CustomerDto>>> GetCustomers(int idCustomer)
        {
            ResponseDto<List<CustomerDto>> result = new ResponseDto<List<CustomerDto>>();
            try
            {
                result.Data = await customerRepository.GetCustomers(idCustomer);
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }

        public async Task<BaseResponseDto> SaveCustomers(CustomerViewModels productViewModels)
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
                        var resultImage = await firebaseFN1.UploadStorage(memoryStream, "imagesCustomers", $"{fn.GetRandomNumber(13)}.jpg");
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
                result = await customerRepository.SaveCustomers(productViewModels);
            }
            catch (Exception ex)
            {
                result.Status = ex.HResult;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<BaseResponseDto> DeleteCustomers(int customerID)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                result = await customerRepository.DeleteCustomers(customerID);
            }
            catch (Exception ex)
            {
                result.Status = ex.HResult;
                result.Message = ex.Message;
            }
            return result;
        }

    }
}