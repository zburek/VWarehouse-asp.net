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
        public EmployeeRepository(VWarehouseContext context)
        {
            this.Repository = new GenericRepository<EmployeeEntity>(context);
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
        public async Task<int> AddAsync(EmployeeEntity entity)
        {
            return await Repository.AddAsync(entity);
        }

        public async Task<int> UpdateAsync(EmployeeEntity entity)
        {
            return await Repository.UpdateAsync(entity);
        }

        public async Task<int> DeleteAsync(Guid ID)
        {
            return await Repository.DeleteAsync(ID);
        }
        #endregion

    }
}