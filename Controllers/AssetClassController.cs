using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hitachiv1.Dtos.AssetDetailDto;
using hitachiv1.Services.AssetServices;
using Microsoft.AspNetCore.Mvc;

namespace hitachiv1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AssetClassController : ControllerBase
    {
        private readonly IAssetClassService _assetClassService;

        public ILogger<DepartmentController> _logger { get; }

        public AssetClassController(IAssetClassService assetClassService, ILogger<DepartmentController> logger)
        {
            _assetClassService = assetClassService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<GetAssetClassDto>>>> GetAll() {
            return Ok(await _assetClassService.All<GetAssetClassDto>());
        }

        [HttpPost]
        public async Task<ActionResult<Response<GetAssetClassDto>>> AddAssetClass(AddAssetClassDto newAssetClass){
            var response = await _assetClassService.Add<GetAssetClassDto>(newAssetClass);
            if(!response.Success){
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<Response<GetAssetClassDto>>> UpdateAssetClass(GetAssetClassDto updateEmployee){
            var response = await _assetClassService.Update<GetAssetClassDto>(updateEmployee);
            if(!response.Success){
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetAssetClassDto>>> GetById(int id){
            var response = await _assetClassService.GetById<GetAssetClassDto>(id);
            if(!response.Success) return NotFound(response);
            return Ok(response);
        }
    }
}