using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using hitachiv1.Dtos.EmployeeDto;
using hitachiv1.Dtos.UserDto;
using Microsoft.EntityFrameworkCore;

namespace hitachiv1.Services.EmployeeServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmployeeService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor){
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<List<GetEmployeeDto>>> GetAllEmployee()
        {
            var response = new Response<List<GetEmployeeDto>>();
            try{
                var employees = await _context.Employees
                    .Include(u => u.User).Include(d => d.Department).ToListAsync();
                response.Data = employees.Select(c => _mapper.Map<GetEmployeeDto>(c)).ToList();
                
            }catch(Exception e){
                response.Success = false;
                response.Message = e.Message;
            }
            
            return response;
        }

        public async Task<Response<GetEmployeeDto>> GetEmployeeById(int id)
        {
            var response = new Response<GetEmployeeDto>();
            
            var employee = await _context.Employees
                .Include(u => u.User)
                .FirstOrDefaultAsync(e => e.Id == id);

            if(employee is not null) response.Data = _mapper.Map<GetEmployeeDto>(employee);
            else response.Success = false;
            return response;
        }

        public async Task<Response<GetEmployeeDto>> AddEmployee(AddEmployeeDto newEmployee)
        {
            var response = new Response<GetEmployeeDto>();
            var employee = _mapper.Map<Employee>(newEmployee);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == newEmployee.UserId);

            var exist = await _context.Employees.Where(e => e.Id == newEmployee.UserId).FirstOrDefaultAsync();
            if(exist is not null){
                response.Success = false;
                response.Message = "Already exist!";
                return response;
            }

            if(user is null){
                response.Success = false;
                response.Message = "User not found!";
            }else{
                try
                {
                    var department = await _context.Departments.FindAsync(newEmployee.DepartmentId);
                    _context.Employees.Add(employee);
                    await _context.SaveChangesAsync();

                    var resEmployee = _mapper.Map<GetEmployeeDto>(employee);
                    resEmployee.Department = _mapper.Map<GetDepartmentDto>(department);
                    //resEmployee.User = _mapper.Map<GetUserDto>(user);
                    response.Data = resEmployee;
                }
                catch (Exception e)
                {
                    response.Success = false;
                    response.Message = e.Message;
                }
            }
            
            return response;
        }

        public async Task<Response<String>> DeleteEmployee(int id)
        {
            var response = new Response<String>();

            try{
                var employee = await _context.Employees.FirstOrDefaultAsync(c => c.Id == id);
                if(employee is null) {
                    response.Success = false;
                    response.Message = $"Employee not found";
                }else{
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();

                    response.Data = $"'{employee.Name}' deleted successfully.";
                    response.Message = "deleted successfully.";
                }
            } catch(Exception e) {
                response.Success = false;
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<GetEmployeeDto>> UpdateEmployee(UpdateEmployeeDto updateEmployee)
        {
            var response = new Response<GetEmployeeDto>();
            
            try
            {   
                var employee = await _context.Employees
                    .Include(c => c.User).Include(d => d.Department)
                    .FirstOrDefaultAsync(e => e.Id == updateEmployee.Id);

                if(employee is null){
                    response.Success = false;
                    response.Message = "NotFound";
                }else{
                    var isDepartmentDifferent = employee.DepartmentId != updateEmployee.DepartmentId;

                    employee.Name = updateEmployee.Name;
                    employee.EmployeeNumber = updateEmployee.EmployeeNumber;
                    employee.Position = updateEmployee.Position;
                    employee.DepartmentId = updateEmployee.DepartmentId;
                    employee.UpdatedDate = DateTime.Now;

                    await _context.SaveChangesAsync();
                    
                    var resEmployee = _mapper.Map<GetEmployeeDto>(employee);
                    if(isDepartmentDifferent){
                        var department = await _context.Departments.FindAsync(updateEmployee.DepartmentId);
                        resEmployee.Department = _mapper.Map<GetDepartmentDto>(department);
                    }
                    
                    response.Data = _mapper.Map<GetEmployeeDto>(employee);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
            
        }
    
        //*** ############################## ~ UTILITIES ~ ############################## ***//
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);

    }
}