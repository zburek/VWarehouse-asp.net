using Common.Parameters;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Repository.Common
{
    public interface IEmployeeRepository
    {
        Task<IEmployee> GetByIdAsync(Guid? ID);
        Task<IEnumerable<IEmployee>> GetAllAsync(IEmployeeParameters parameters);
        Task<IEmployee> GetOneAsync(IEmployeeParameters parameters);
        Task<int> GetCountAsync(IEmployeeParameters parameters);
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
