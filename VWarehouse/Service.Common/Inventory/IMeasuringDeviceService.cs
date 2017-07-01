using Common;
using DAL.DbEntities.Inventory;
using Model.Common.Inventory;
using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Common.Inventory
{
    public interface IMeasuringDeviceService
    {
        Task<List<IMeasuringDevice>> GetAllAsync(IParameters<MeasuringDeviceEntity> parameters);
        Task<IMeasuringDevice> GetByIdAsync(Guid? ID);
        Task<StaticPagedList<IMeasuringDevice>> GetAllPagedListAsync(IParameters<MeasuringDeviceEntity> parameters);
        Task CreateAsync(IMeasuringDevice measuringDevice);
        Task UpdateAsync(IMeasuringDevice measuringDevice);
        Task DeleteAsync(Guid ID);
        Task AssignMeasuringDeviceAsync(Guid itemID, Guid? employeeID);
        Task ReturnOneMeasuringDeviceAsync(Guid? ID);
        Task ReturnAllMeasuringDevicesAsync(Guid? ID);
    }
}
