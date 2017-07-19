using Common.Parameters;
using Model.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Common
{
    public interface IEmployeeService
    {  
        Task<List<IEmployee>> GetAllAsync(IEmployeeParameters parameters = null);
        Task<StaticPagedList<IEmployee>> GetAllPagedListAsync(IEmployeeParameters parameters);
        Task<IEmployee> GetOneAsync(IEmployeeParameters parameters);
        Task<IEmployee> GetByIdAsync(Guid? ID);
        Task CreateAsync(IEmployee employee);
        Task UpdateAsync(IEmployee employee);  
        Task DeleteAsync(Guid ID);
        Task ReturnOneItemAsync(Guid? ID);
        Task ReturnAllItemsAsync(Guid? ID);
        Task ReturnOneMeasuringDeviceAsync(Guid? ID);
        Task ReturnAllMeasuringDevicesAsync(Guid? ID);
        Task ReturnOneVehicleAsync(Guid? ID);
        Task ReturnAllVehiclesAsync(Guid? ID);
        Task ReturnAllInventory(Guid? ID);
    }
}
