using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserMgt.Core;
using UserMgt.Repo.Repositories.GenericRepository.Interfaces;

namespace UserMgt.Repo.Repositories.GenericRepository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected UserDbContext _UserContext;
 


        public GenericRepository(UserDbContext BankContext)
        {
            _UserContext = BankContext;
        }

        public async Task<IQueryable<T>> FindAllAsync(bool trackChanges) =>
            !trackChanges ? await Task.Run(() => _UserContext.Set<T>().AsNoTracking()) : await Task.Run(() => _UserContext.Set<T>());

        public async Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges) =>
            !trackChanges ? await Task.Run(() => _UserContext.Set<T>().Where(expression).AsNoTracking()) : await Task.Run(() => _UserContext.Set<T>().Where(expression));

        public async Task<T> FirstByDefaultAsync(Expression<Func<T, bool>> expression) =>
            await Task.Run(() => _UserContext.Set<T>().FirstOrDefaultAsync(expression));
        public Task<T> CreateAsync(T entity) => Task.Run(() => _UserContext.Set<T>().Add(entity).Entity);

        public async Task<int> Save()
        {
            return await _UserContext.SaveChangesAsync();
        }
    }
}
