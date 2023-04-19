using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;


namespace hitachiv1
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles(){
            CreateMap<AddEmployeeDto, Employee>();
            CreateMap<Employee, GetEmployeeDto>();
            CreateMap<User, GetUserDto>();
            CreateMap<Department, GetDepartmentDto>();
            CreateMap<AddDepartmentDto, Department>();
        }
        
    }
}