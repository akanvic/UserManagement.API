using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Core.Entities.Models;

namespace UserMgt.Service.Interface
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
    }
}
