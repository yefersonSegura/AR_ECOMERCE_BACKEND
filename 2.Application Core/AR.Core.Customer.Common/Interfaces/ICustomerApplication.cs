using AR.Common.Dto;
using AR.Core.Customer.Common.Dto;
using AR.Core.Customer.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.Customer.Common.Interfaces
{
    public interface ICustomerApplication
    {
        Task<ResponseDto<List<CustomerDto>>> GetCustomers(int idCustomer);
        Task<BaseResponseDto> SaveCustomers(CustomerViewModels customerViewModels);
        Task<BaseResponseDto> DeleteCustomers(int customerID);
    }
}