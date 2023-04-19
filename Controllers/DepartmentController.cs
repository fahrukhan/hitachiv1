using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hitachiv1.Services.DepartmentServices;
using Microsoft.AspNetCore.Mvc;

namespace hitachiv1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public ILogger<DepartmentController> _logger { get; }

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger)
        {
            _departmentService = departmentService;
            _logger = logger;
        }


        [HttpGet]
        public async Task<ActionResult<Response<List<GetDepartmentDto>>>> GetAll() {
            _logger.LogInformation("Department list");
            return Ok(await _departmentService.AllDepartment());
        }

        [HttpPost]
        public async Task<ActionResult<Response<GetDepartmentDto>>> AddEmployee(AddDepartmentDto newDept){
            var response = await _departmentService.AddDepartment(newDept);
            if(!response.Success){
                return BadRequest(response);
            }
            return Ok(response);
        }



    }
}