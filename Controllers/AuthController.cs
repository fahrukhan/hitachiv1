using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hitachiv1.Data;
using hitachiv1.Dtos.UserDto;
using Microsoft.AspNetCore.Mvc;

namespace hitachiv1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo){
            _authRepo = authRepo;
        }

        [HttpGet("Test")]
        public ActionResult<Response<String>> Test() {
            var response = _authRepo.Test();
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Response<int>>> Register(UserRegisterDto request){
            var response = await _authRepo.Register(
                new User{ Email = request.Email }, request.Password
            );

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<Response<int>>> Login(UserLoginDto request){
            var response = await _authRepo.Login(request.Email, request.Password);
            if(!response.Success){
                return BadRequest(response);
            }

            return Ok(response);
        }        
    }
}