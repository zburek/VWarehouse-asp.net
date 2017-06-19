using DAL;
using DAL.DbEntities.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Common.Inventory
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<VehicleEntity>> GetAllAsync(IParameters<VehicleEntity> parameters);
        Task<VehicleEntity> GetByIdAsync(Guid? ID);
        Task<int> GetCountAsync(IParameters<VehicleEntity> parameters);
        Task<int> CreateAsync(VehicleEntity itemEntity);
        Task<int> UpdateAsync(VehicleEntity itemEntity);
        Task<int> DeleteAsync(Guid ID);
    }
}
