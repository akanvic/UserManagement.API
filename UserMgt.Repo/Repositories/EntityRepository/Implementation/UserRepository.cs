using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Core;
using UserMgt.Core.Entities.Dtos;
using UserMgt.Core.Entities.Models;
using UserMgt.Repo.Repositories.EntityRepository.Interface;
using UserMgt.Repo.Repositories.GenericRepository.Implementations;

namespace UserMgt.Repo.Repositories.EntityRepository.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserDbContext _dbContext;
        public UserRepository(UserDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> UpdateUser(UpdateUserDTO userDto)
        {
            var response = await _dbContext.Users.FirstOrDefaultAsync(c => c.UserId == userDto.UserId);

            if(response != null)
            {
                response.EmailAddress = userDto.EmailAddress;
            }

            return response!;
        }

        public async Task<User> GetUserProfile(int userId)
        {
            var response = await _dbContext.Users.Where(c => c.UserId == userId).Select(c => new User
            {
                UserId = c.UserId,
                EmailAddress = c.EmailAddress,
            }).FirstOrDefaultAsync();

            return response!;
        }
    }
}
