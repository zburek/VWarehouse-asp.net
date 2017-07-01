using Common;
using DAL.DbEntities.Inventory;
using Repository.Common;
using Repository.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Inventory
{
    public class VehicleRepository : IVehicleRepository
    {
        protected IGenericRepository<VehicleEntity> Repository { get; private set; }
        public VehicleRepository(IGenericRepository<VehicleEntity> repository)
        {
            this.Repository = repository;
        }

        #region Get
        public async Task<IEnumerable<VehicleEntity>> GetAllAsync(IParameters<VehicleEntity> parameters)
        {
            return await Repository.GetAllAsync(parameters);
        }
        public async Task<VehicleEntity> GetByIdAsync(Guid? ID)
        {
            return await Repository.GetByIdAsync(ID);
        }

        public Task<int> GetCountAsync(IParameters<VehicleEntity> parameters)
        {
            return Repository.GetCountAsync(parameters);
        }
        #endregion

        #region Basic CRUD
        public async Task CreateAsync(VehicleEntity vehicleEntity)
        {
            await Repository.AddAsync(vehicleEntity);
            await Repository.SaveAsync();
        }

        public async Task UpdateAsync(VehicleEntity vehicleEntity)
        {
            await Repository.UpdateAsync(vehicleEntity);
             await Repository.SaveAsync(); 
        }

        public async Task DeleteAsync(Guid ID)
        {
            await Repository.DeleteAsync(ID);
            await Repository.SaveAsync();
        }
        #endregion
    }
}
