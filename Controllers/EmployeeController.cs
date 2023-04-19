using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hitachiv1.Dtos.EmployeeDto;
using hitachiv1.Services.EmployeeServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hitachiv1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<GetEmployeeDto>>>> GetAll() {
            return Ok(await _employeeService.GetAllEmployee());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetEmployeeDto>>> GetById(int id){
            var response = await _employeeService.GetEmployeeById(id);
            if(response.Data is null) return NotFound(response);
            return Ok(response);
            //return Ok(await _employeeService.GetEmployeeById(id));
        }

        [HttpPost]
        public async Task<ActionResult<Response<GetEmployeeDto>>> AddEmployee(AddEmployeeDto newEmployee){
            var response = await _employeeService.AddEmployee(newEmployee);
            if(!response.Success){
                return BadRequest(response);
            }
            
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<Response<GetEmployeeDto>>> UpdateEmployee(UpdateEmployeeDto updateEmployee){
            var response = await _employeeService.UpdateEmployee(updateEmployee);
            if(!response.Success){
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<string>>> DeleteEmployee(int id){
            var response = await _employeeService.DeleteEmployee(id);
            if(response.Success == false) return NotFound(response);
            return Ok(response);
        }
    }
}