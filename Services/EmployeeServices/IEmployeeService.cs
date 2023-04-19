using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hitachiv1.Dtos.EmployeeDto;

namespace hitachiv1.Services.EmployeeServices
{
    public interface IEmployeeService
    {
        Task<Response<List<GetEmployeeDto>>> GetAllEmployee();
        Task<Response<GetEmployeeDto>> GetEmployeeById(int id);
        Task<Response<GetEmployeeDto>> AddEmployee(AddEmployeeDto newEmployee);
        Task<Response<GetEmployeeDto>> UpdateEmployee(UpdateEmployeeDto updateEmployee);
        Task<Response<String>> DeleteEmployee(int id);
    }
}