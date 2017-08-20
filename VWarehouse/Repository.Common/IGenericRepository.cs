using Common;
using Common.Parameters.RepositoryParameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IGenericRepository
    {
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(IGenericRepositoryParameters<TEntity> parameters) where TEntity : class, IBaseEntity;
        Task<TEntity> GetByIdAsync<TEntity>(Guid? ID) where TEntity : class, IBaseEntity;
        Task<TEntity> GetOneAsync<TEntity>(IGenericRepositoryParameters<TEntity> parameters) where TEntity : class, IBaseEntity;
        Task<int> GetCountAsync<TEntity>(IGenericRepositoryParameters<TEntity> parameters) where TEntity : class, IBaseEntity;
        Task<int> CreateAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;
        Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;
        Task<int> DeleteAsync<TEntity>(Guid ID) where TEntity : class, IBaseEntity;
        IUnitOfWork CreateUnitOfWork();
        IUnitOfWorkAssignment CreateUnitOfWorkAssignment();
    }
}
