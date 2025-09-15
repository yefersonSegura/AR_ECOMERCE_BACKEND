
using AR.Common.Dto;
using AR.Core.Employee.Common.Dto;
using AR.Core.Employee.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AR.Core.Employee.Common.Interfaces
{
    public  interface IEmployeeRepository
    {
        Task<List<EmployeeDto>> GetEmployees(int idEmployee);
        Task<BaseResponseDto> SaveEmployee(EmployeeViewModels employeeViewModels);
        Task<BaseResponseDto> DeleteEmployee(int employeeID);
    }
}
