using Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IGenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(IParameters<TEntity> parameters);
        Task<TEntity> GetByIdAsync(Guid? ID);
        Task<TEntity> GetOneAsync(IParameters<TEntity> parameters);
        Task<int> GetCountAsync(IParameters<TEntity> parameters);
        Task<int> CreateAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(Guid ID);
    }
}
