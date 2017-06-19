using DAL;
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
        public VehicleRepository(VWarehouseContext context)
        {
            this.Repository = new GenericRepository<VehicleEntity>(context);
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
        public async Task<int> CreateAsync(VehicleEntity itemEntity)
        {
            return await Repository.AddAsync(itemEntity);
        }

        public async Task<int> UpdateAsync(VehicleEntity itemEntity)
        {
            return await Repository.UpdateAsync(itemEntity);
        }

        public async Task<int> DeleteAsync(Guid ID)
        {
            return await Repository.DeleteAsync(ID);
        }
        #endregion
    }
}
