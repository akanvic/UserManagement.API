using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Core.Entities.Dtos;
using UserMgt.Core.Entities.Models;
using UserMgt.Core.Entities.Response;

namespace UserMgt.Service.Interface
{
    public interface IUserService
    {
        Task<ServiceResponse> AddUser(UserDTO userDto);
        Task<ServiceResponse> UpdateUser(UpdateUserDTO userDto);
        Task<ServiceResponse> GetUserProfile(int userId);
    }
}
