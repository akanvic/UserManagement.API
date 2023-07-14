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
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ServiceResponse> AddUser(UserDTO userDto)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var hashedPassword = Helper.HashPassword(userDto.Password!);
                var user = new User
                {
                    EmailAddress = userDto.EmailAddress,
                    Password = hashedPassword,
                };

                await _userRepository.CreateAsync(user);
                await _userRepository.Save();

                serviceResponse.StatusCode = HttpStatusCode.Created;
                serviceResponse.Message = "User created successfully";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.StatusCode = HttpStatusCode.InternalServerError;
                serviceResponse.Message = ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message;

                return serviceResponse;
            }
        }

        public async Task<ServiceResponse> GetUserProfile(int userId)
        {
            var serviceResponse = new ServiceResponse();

            try
            {
                var response = await _userRepository.FirstByDefaultAsync(c=>c.UserId == userId);  

                if(response == null)
                {
                    serviceResponse.StatusCode = HttpStatusCode.BadRequest;
                    serviceResponse.Message = "User could not be found";

                    return serviceResponse;
                }

                var userResponse = new UpdateUserDTO
                {
                    UserId = response.UserId,
                    EmailAddress = response.EmailAddress,
                };

                serviceResponse.StatusCode = HttpStatusCode.OK;
                serviceResponse.Message = "User profile retrieved successfully";
                serviceResponse.Data = userResponse;

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.StatusCode = HttpStatusCode.InternalServerError;
                serviceResponse.Message = ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message;

                return serviceResponse;
            }
        }

        public async Task<ServiceResponse> UpdateUser(UpdateUserDTO userDto)
        {
            var serviceResponse = new ServiceResponse();
            try
            {
                var response = await _userRepository.UpdateUser(userDto);

                if(response == null)
                {
                    serviceResponse.StatusCode = HttpStatusCode.BadRequest;
                    serviceResponse.Message = "User could not be found";

                    return serviceResponse;
                }

                var isUpdated = await _userRepository.Save();

                serviceResponse.StatusCode = HttpStatusCode.NoContent;
                serviceResponse.Message = "User Updated successfully";

                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.StatusCode = HttpStatusCode.InternalServerError;
                serviceResponse.Message = ex?.InnerException?.InnerException?.Message ?? ex?.InnerException?.Message ?? ex?.Message;

                return serviceResponse;
            }
        }
    }
}
