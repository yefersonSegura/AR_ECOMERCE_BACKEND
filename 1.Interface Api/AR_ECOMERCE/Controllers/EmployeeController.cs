using AR.Common;
using AR.Common.Dto;
using AR.Core.Employee.Common.Dto;
using AR.Core.Employee.Common.Interfaces;
using AR.Core.Employee.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AR_ECOMERCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeApplication employeeApplication;

        public EmployeeController(IEmployeeApplication employeeApplication)
        {
            this.employeeApplication = employeeApplication;
        }

        [HttpGet("GetEmployees")]
        public Task<ResponseDto<List<EmployeeDto>>> GetEmployees([FromQuery] int idEmployee)
        {
            return employeeApplication.GetEmployees(idEmployee);
        }

        [HttpPost("SaveEmployee")]
        public Task<BaseResponseDto> SaveEmployee([FromBody] EmployeeViewModels employeeViewModels)
        {
            return employeeApplication.SaveEmployee(employeeViewModels);
        }

        [HttpDelete("DeleteEmployees")]
        public Task<BaseResponseDto> DeleteEmployees([FromQuery] int employeeID)
        {
            return employeeApplication.DeleteEmployee(employeeID);
        }


    }
}