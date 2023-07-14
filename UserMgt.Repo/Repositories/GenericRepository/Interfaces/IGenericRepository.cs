using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UserMgt.Repo.Repositories.GenericRepository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> FindAllAsync(bool trackChanges);
        Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges);
        Task<T> CreateAsync(T entity);
        Task<T> FirstByDefaultAsync(Expression<Func<T, bool>> expression);
        Task<int> Save();
    }
}
