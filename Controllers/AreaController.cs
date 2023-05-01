using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace hitachiv1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreaController : ControllerBase
    {

        private readonly IAreaService _areaService;

        public ILogger<DepartmentController> _logger { get; }

        public AreaController(IAreaService areaService, ILogger<DepartmentController> logger)
        {
            _areaService = areaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<Area>>>> GetAll(){
            return Ok(await _areaService.All<GetAreaDto>());
        }

        [HttpPost]
        public async Task<ActionResult<Response<GetAreaDto>>> AddAssetClass(AddAreaDto newArea){
            var response = await _areaService.Add<GetAreaDto>(newArea);
            if(!response.Success){
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}