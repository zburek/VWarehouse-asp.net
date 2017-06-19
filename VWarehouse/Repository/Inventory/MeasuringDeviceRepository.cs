using DAL;
using DAL.DbEntities.Inventory;
using Repository.Common;
using Repository.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Inventory
{
    public class MeasuringDeviceRepository : IMeasuringDeviceRepository
    {
        protected IGenericRepository<MeasuringDeviceEntity> Repository { get; private set; }
        public MeasuringDeviceRepository(VWarehouseContext context)
        {
            this.Repository = new GenericRepository<MeasuringDeviceEntity>(context);
        }

        #region Get
        public async Task<IEnumerable<MeasuringDeviceEntity>> GetAllAsync(IParameters<MeasuringDeviceEntity> parameters)
        {
            return await Repository.GetAllAsync(parameters);
        }
        public async Task<MeasuringDeviceEntity> GetByIdAsync(Guid? ID)
        {
            return await Repository.GetByIdAsync(ID);
        }

        public Task<int> GetCountAsync(IParameters<MeasuringDeviceEntity> parameters)
        {
            return Repository.GetCountAsync(parameters);
        }
        #endregion

        #region Basic CRUD
        public async Task<int> CreateAsync(MeasuringDeviceEntity measuringDeviceEntity)
        {
            return await Repository.AddAsync(measuringDeviceEntity);
        }

        public async Task<int> UpdateAsync(MeasuringDeviceEntity measuringDeviceEntity)
        {
            return await Repository.UpdateAsync(measuringDeviceEntity);
        }

        public async Task<int> DeleteAsync(Guid ID)
        {
            return await Repository.DeleteAsync(ID);
        }
        #endregion
    }
}

