using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserMgt.Core.Entities.Dtos;
using UserMgt.Service.Interface;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {

            var response = await _authService.Login(loginDTO);

            return Ok(response);
        }
    }
}
