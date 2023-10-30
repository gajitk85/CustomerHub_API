using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerHub.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetByAgeAsync(int age);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
}
