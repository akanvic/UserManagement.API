using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using UserMgt.Core.Entities.Dtos;
using UserMgt.Core.Entities.Models;
using UserMgt.Core.Entities.Response;
using UserMgt.Service.Interface;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Register a new User
        /// </summary>
        /// <param name="userDTO">The Id of a new user</param>
        /// <returns>201 Created</returns>
        /// 
        [ProducesResponseType(typeof(ServiceResponse), 201)]
        [ProducesResponseType(typeof(void), 400)]
        [Description("Register new User")]
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> AddUser(UserDTO userDTO)
        {

              var response = await _userService.AddUser(userDTO);

              return Ok(response);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO userDTO)
        {

            var response = await _userService.UpdateUser(userDTO);

            return Ok(response);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("GetUserProfile")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {

            var response = await _userService.GetUserProfile(userId);

            return Ok(response);
        }
    }
}
