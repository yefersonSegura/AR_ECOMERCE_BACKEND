using AR.Common.Dto;
using AR.Core.Customer.Common.Dto;
using AR.Core.Customer.Common.Interfaces;
using AR.Core.Customer.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AR_ECOMERCE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerApplication customerApplication;
        public CustomerController(ICustomerApplication customerApplication)
        {
            this.customerApplication = customerApplication;
        }

        [HttpGet("GetCustomers")]
        public Task<ResponseDto<List<CustomerDto>>> GetCustomers([FromQuery] int idCustomer)
        {
            return customerApplication.GetCustomers(idCustomer);
        }

        [HttpPost("SaveCustomers")]
        public Task<BaseResponseDto> SaveCustomers([FromBody] CustomerViewModels customerViewModels)
        {
            return customerApplication.SaveCustomers(customerViewModels);
        }

        [HttpDelete("DeleteCustomers")]
        public Task<BaseResponseDto> DeleteCustomers([FromQuery] int customerID)
        {
            return customerApplication.DeleteCustomers(customerID);
        }

    }
}