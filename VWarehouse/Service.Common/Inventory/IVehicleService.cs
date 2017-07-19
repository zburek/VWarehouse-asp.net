using Common.Parameters;
using Model.Common.Inventory;
using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Common.Inventory
{
    public interface IVehicleService
    {
        Task<List<IVehicle>> GetAllAsync(IVehicleParameters parameters);
        Task<IVehicle> GetByIdAsync(Guid? ID);
        Task<StaticPagedList<IVehicle>> GetAllPagedListAsync(IVehicleParameters parameters);
        Task CreateAsync(IVehicle item);
        Task UpdateAsync(IVehicle item);
        Task DeleteAsync(Guid ID);
        Task AssignVehicleAsync(Guid itemID, Guid? employeeID);
    }
}
