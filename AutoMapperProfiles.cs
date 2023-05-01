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
            CreateMap<AddDepartmentDto, Department>();
            CreateMap<AddAssetClassDto, AssetClass>();
            CreateMap<AddAreaDto, Area>();

            CreateMap<Employee, GetEmployeeDto>();
            CreateMap<User, GetUserDto>();
            CreateMap<Department, GetDepartmentDto>();
            CreateMap<AssetClass, GetAssetClassDto>();
            CreateMap<Area, GetAreaDto>();
            
        }
        
    }
}