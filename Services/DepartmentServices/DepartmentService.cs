using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace hitachiv1.Services.DepartmentServices
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DepartmentService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor){
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Response<GetDepartmentDto>> AddDepartment(AddDepartmentDto newDepartment)
        {
            var response = new Response<GetDepartmentDto>();
            var department = _mapper.Map<Department>(newDepartment);

            var exist = await _context.Departments
                .Where(e => e.DepartmentCode == newDepartment.DepartmentCode 
                    || e.DepartmentName == newDepartment.DepartmentName)
                .FirstOrDefaultAsync();

            if(exist is not null){
                response.Success = false;
                response.Message = "Department code or name already exists!";
                return response;
            }

            try
            {
                _context.Departments.Add(department);
                await _context.SaveChangesAsync();

                var res = _mapper.Map<GetDepartmentDto>(department);
                //resEmployee.User = _mapper.Map<GetUserDto>(user);
                response.Data = res;
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
            }
            
            return response;
        }

        public async Task<Response<List<GetDepartmentDto>>> AllDepartment()
        {
            var response = new Response<List<GetDepartmentDto>>();
            try{
                var departments = await _context.Departments.ToListAsync();
                response.Data = departments.Select(c => _mapper.Map<GetDepartmentDto>(c)).ToList();
                
            }catch(Exception e){
                response.Success = false;
                response.Message = e.Message;
            }
            
            return response;
        }

        public Task<Response<string>> DeleteDepartment(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<GetDepartmentDto>> DepartmentById(int id)
        {
            var response = new Response<GetDepartmentDto>();
            var department = await _context.Departments.FindAsync(id);
            if(department is null) {
                response.Success = false;
                response.Message = "Not found";
            }

            response.Data = _mapper.Map<GetDepartmentDto>(department);
            return response;
        }

        public Task<Response<GetDepartmentDto>> UpdateDepartment(GetDepartmentDto updateDepartment)
        {
            throw new NotImplementedException();
        }
    }
}