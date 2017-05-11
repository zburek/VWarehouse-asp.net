
using Model.Common.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IGenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
            string includeProperties = null);
        Task<TEntity> GetByIdAsync(int? ID);
        Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
