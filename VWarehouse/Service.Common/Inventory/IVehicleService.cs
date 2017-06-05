using Model.Common.Inventory;
using Model.Common.ViewModels;
using Model.DbEntities.Inventory;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Common.Inventory
{
    public interface IVehicleService
    {
        Task<List<IVehicle>> GetAllAsync(
            Expression<Func<VehicleEntity, bool>> filter = null,
            Func<IQueryable<VehicleEntity>, IOrderedQueryable<VehicleEntity>> orderBy = null,
            string includeProperties = null);
        Task<IVehicle> GetByIdAsync(int? ID);
        Task<StaticPagedList<IVehicle>> GetAllPagedListAsync(
             string searchString, string sortOrder, int pageNumber, int pageSize,
             Expression<Func<VehicleEntity, bool>> filter = null);
        Task CreateAsync(IVehicle item);
        Task UpdateAsync(IVehicle item);
        Task DeleteAsync(int ID);
        Task<IAssignViewModel> CreateAssignViewModelAsync(int? ID);
        Task AssignVehicleAsync(IAssignViewModel measuringDevice);
        Task ReturnOneVehicleAsync(int? ID);
        Task ReturnAllVehiclesAsync(int? ID);
    }
}
