using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Core.Entities.Dtos;
using UserMgt.Core.Entities.Models;
using UserMgt.Repo.Repositories.GenericRepository.Interfaces;

namespace UserMgt.Repo.Repositories.EntityRepository.Interface
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> UpdateUser(UpdateUserDTO userDto);
        Task<User> GetUserProfile(int userId);
    }
}
