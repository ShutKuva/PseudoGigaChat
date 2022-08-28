using BLL.Abstractions;
using Core.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]UserDTOFromUI user)
        {
            string token = await _authenticationService.Authenticate(user);

            if(token == null)
            {
                return BadRequest("Unregistered user");
            }
            else
            {
                return Ok(new { token = token });
            }
        }

        [Authorize]
        [HttpGet("validate")]
        public IActionResult IsValidToken()
        {
            return Ok();
        }
    }
}
