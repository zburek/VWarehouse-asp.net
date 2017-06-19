using DAL;
using DAL.DbEntities;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected IGenericRepository<EmployeeEntity> Repository { get; private set; }
        public EmployeeRepository(IGenericRepository<EmployeeEntity> repository)
        {
            this.Repository = repository;
        }

        #region Get
        public async Task<EmployeeEntity> GetByIdAsync(Guid? ID)
        {
            return await Repository.GetByIdAsync(ID);
        }

        public async Task<IEnumerable<EmployeeEntity>> GetAllAsync(IParameters<EmployeeEntity> parameters)
        {
            return await Repository.GetAllAsync(parameters);
        }

        public async Task<EmployeeEntity> GetOneAsync(IParameters<EmployeeEntity> parameters)
        {
            return await Repository.GetOneAsync(parameters);
        }

        public Task<int> GetCountAsync(IParameters<EmployeeEntity> parameters)
        {
            return Repository.GetCountAsync(parameters);
        }
        #endregion

        #region CRUD
        public async Task AddAsync(EmployeeEntity entity)
        {
            int addTest = await Repository.AddAsync(entity);
            int saveTest = await Repository.SaveAsync();
        }

        public async Task UpdateAsync(EmployeeEntity entity)
        {
            int updateTest = await Repository.UpdateAsync(entity);
            int saveTest = await Repository.SaveAsync();
        }

        public async Task DeleteAsync(Guid ID)
        {
            int updateTest = await Repository.DeleteAsync(ID);
            int saveTest = await Repository.SaveAsync();
        }
        #endregion
    }
}