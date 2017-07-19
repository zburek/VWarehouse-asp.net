using Common.Parameters;
using Model.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Common.Inventory
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<IVehicle>> GetAllAsync(IVehicleParameters parameters);
        Task<IVehicle> GetByIdAsync(Guid? ID);
        Task<int> GetCountAsync(IVehicleParameters parameters);
        Task<List<IVehicle>> GetVehicleLicenseDateWarning(int daysDifference);
        Task<List<IVehicle>> GetVehicleMileageWarning(int mileageDifference);
        Task CreateAsync(IVehicle vehicleEntity);
        Task UpdateAsync(IVehicle vehicleEntity);
        Task DeleteAsync(Guid ID);
        Task AssignOneVehicleAsync(Guid itemID, Guid? employeeID);
    }
}
