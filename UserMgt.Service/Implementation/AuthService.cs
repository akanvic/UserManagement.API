using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Core.Entities.Dtos;
using UserMgt.Core.Entities.Models;
using UserMgt.Core.Entities.Response;
using UserMgt.Repo.Repositories.EntityRepository.Interface;
using UserMgt.Service.Helpers;
using UserMgt.Service.Interface;

namespace UserMgt.Service.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse> Login(LoginDTO loginDTO)
        {
            var loginResponse = new LoginResponse();
            try
            {
                
                var userResponse = await _userRepository
                    .FirstByDefaultAsync(c => c.EmailAddress == loginDTO.EmailAddress);

                if(userResponse == null)
                {
                    loginResponse.StatusCode = HttpStatusCode.BadRequest;
                    loginResponse.Message = "Invalid user credentials";

                    return loginResponse;
                }
                var isPasswordVerified = Helper.VerifyPassword(loginDTO.Password!, userResponse.Password!);

                if (!isPasswordVerified)
                {
                    loginResponse.StatusCode = HttpStatusCode.BadRequest;
                    loginResponse.Message = "Invalid user credentials";

                    return loginResponse;
                }
                var user = new User
                {
                    EmailAddress = loginDTO.EmailAddress,
                    UserId = userResponse.UserId
                };

                var token = _tokenService.GenerateAccessToken(user);

                loginResponse.StatusCode = HttpStatusCode.OK;
                loginResponse.Message = "User Login successfully";
                loginResponse.Token = token;

                return loginResponse;
            }
            catch (Exception ex)
            {
                loginResponse.StatusCode = HttpStatusCode.InternalServerError;
                loginResponse.Message = ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message;

                return loginResponse;
            }
        }
    }
}
