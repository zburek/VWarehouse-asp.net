using DAL;
using DAL.DbEntities.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Common.Inventory
{
    public interface IMeasuringDeviceRepository
    {
        Task<IEnumerable<MeasuringDeviceEntity>> GetAllAsync(IParameters<MeasuringDeviceEntity> parameters);
        Task<MeasuringDeviceEntity> GetByIdAsync(Guid? ID);
        Task<int> GetCountAsync(IParameters<MeasuringDeviceEntity> parameters);
        Task<int> CreateAsync(MeasuringDeviceEntity itemEntity);
        Task<int> UpdateAsync(MeasuringDeviceEntity itemEntity);
        Task<int> DeleteAsync(Guid ID);
    }
}
