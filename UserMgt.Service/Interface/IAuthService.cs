using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Core.Entities.Dtos;
using UserMgt.Core.Entities.Response;

namespace UserMgt.Service.Interface
{
    public interface IAuthService
    {
        Task<LoginResponse> Login(LoginDTO loginDTO);
    }
}
