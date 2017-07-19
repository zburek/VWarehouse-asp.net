using Common.Parameters;
using Model.Common.Inventory;
using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Common.Inventory
{
    public interface IMeasuringDeviceService
    {
        Task<List<IMeasuringDevice>> GetAllAsync(IMeasuringDeviceParameters parameters);
        Task<IMeasuringDevice> GetByIdAsync(Guid? ID);
        Task<StaticPagedList<IMeasuringDevice>> GetAllPagedListAsync(IMeasuringDeviceParameters parameters);
        Task CreateAsync(IMeasuringDevice measuringDevice);
        Task UpdateAsync(IMeasuringDevice measuringDevice);
        Task DeleteAsync(Guid ID);
        Task AssignMeasuringDeviceAsync(Guid itemID, Guid? employeeID);
    }
}
