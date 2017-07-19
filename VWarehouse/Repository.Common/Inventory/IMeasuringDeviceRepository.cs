using Common.Parameters;
using Model.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Common.Inventory
{
    public interface IMeasuringDeviceRepository
    {
        Task<IEnumerable<IMeasuringDevice>> GetAllAsync(IMeasuringDeviceParameters parameters);
        Task<IMeasuringDevice> GetByIdAsync(Guid? ID);
        Task<int> GetCountAsync(IMeasuringDeviceParameters parameters);
        Task<IEnumerable<IMeasuringDevice>> GetMeasuringDeviceCalibraionDateWarning(int daysDifference);
        Task CreateAsync(IMeasuringDevice measuringDeviceEntity);
        Task UpdateAsync(IMeasuringDevice measuringDeviceEntity);
        Task DeleteAsync(Guid ID);
        Task AssignOneMeasuringDeviceAsync(Guid itemID, Guid? employeeID);
    }
}