using AR.Common.Dto;
using AR.Core.Customer.Common.Dto;
using AR.Core.Customer.Common.ViewModels;

namespace AR.Core.Customer.Common.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDto>> GetCustomers(int idCustomer);
        Task<BaseResponseDto> SaveCustomers(CustomerViewModels customerViewModels);
        Task<BaseResponseDto> DeleteCustomers(int customerID);
    }
}