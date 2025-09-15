using AR.Common.Dto;
using AR.Common.Functions;
using AR.Common.InterfacesFirabase;
using AR.Core.Employee.Common.Dto;
using AR.Core.Employee.Common.Interfaces;
using AR.Core.Employee.Common.ViewModels;
using System.Runtime.ExceptionServices;

namespace AR.Core.Employee.Application
{
    public class EmployeeApplication : IEmployeeApplication
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IFirebaseFN firebaseFN1;
        public EmployeeApplication(IEmployeeRepository employeeRepository, IFirebaseFN firebaseFN1)
        {
            this.employeeRepository = employeeRepository;
            this.firebaseFN1 = firebaseFN1;
        }

        public async Task<ResponseDto< List<EmployeeDto>>> GetEmployees(int idEmployee)
        {
            ResponseDto < List<EmployeeDto>> result = new ResponseDto<List<EmployeeDto>>();
            try
            {
                result.Data = await employeeRepository.GetEmployees(idEmployee);
                result.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                ExceptionDispatchInfo.Capture(new Exception(ex.Message)).Throw();
            }
            return result;
        }

        public async Task<BaseResponseDto> SaveEmployee(EmployeeViewModels employeeViewModels)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                FunctionApp fn = new FunctionApp();
                if (!string.IsNullOrEmpty(employeeViewModels.base64Url))
                {
                    byte[] imageBytes = Convert.FromBase64String(employeeViewModels.base64Url);
                    using (MemoryStream memoryStream = new MemoryStream(imageBytes))
                    {
                        var resultImage = await firebaseFN1.UploadStorage(memoryStream, "imagesEmployee", $"{fn.GetRandomNumber(13)}.jpg");
                        if (!resultImage.IsSuccessful)
                        {
                            result.Status = resultImage.Status;
                            result.IsSuccessful = resultImage.IsSuccessful;
                            result.Message = resultImage.Message;
                            return result;
                        }
                        employeeViewModels.urlImage = resultImage.Data;
                    }
                }
                result = await employeeRepository.SaveEmployee(employeeViewModels);
            }
            catch (Exception ex)
            {
                result.Status = ex.HResult;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<BaseResponseDto> DeleteEmployee(int employeeID)
        {
            BaseResponseDto result = new BaseResponseDto();
            try
            {
                result = await employeeRepository.DeleteEmployee(employeeID);
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