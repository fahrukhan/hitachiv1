using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hitachiv1.Services.DepartmentServices
{
    public interface IDepartmentService
    {
        Task<Response<List<GetDepartmentDto>>> AllDepartment();
        Task<Response<GetDepartmentDto>> DepartmentById(int id);
        Task<Response<GetDepartmentDto>> AddDepartment(AddDepartmentDto newDepartment);
        Task<Response<GetDepartmentDto>> UpdateDepartment(GetDepartmentDto updateDepartment);
        Task<Response<String>> DeleteDepartment(int id);
    }
}