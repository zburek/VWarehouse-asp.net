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
    public interface IMeasuringDeviceService
    {
        Task<List<IMeasuringDevice>> GetAllAsync(
            Expression<Func<MeasuringDeviceEntity, bool>> filter = null,
            Func<IQueryable<MeasuringDeviceEntity>, IOrderedQueryable<MeasuringDeviceEntity>> orderBy = null,
            string includeProperties = null);
        Task<IMeasuringDevice> GetByIdAsync(int? ID);
        Task<StaticPagedList<IMeasuringDevice>> GetAllPagedListAsync(
            string searchString, string sortOrder, int pageNumber, int pageSize,
            Expression<Func<MeasuringDeviceEntity, bool>> filter = null);
        Task CreateAsync(IMeasuringDevice measuringDevice);
        Task UpdateAsync(IMeasuringDevice measuringDevice);
        Task DeleteAsync(int ID);
        Task<IAssignViewModel> CreateAssignViewModelAsync(int? ID);
        Task AssignMeasuringDeviceAsync(IAssignViewModel measuringDevice);
        Task ReturnOneMeasuringDeviceAsync(int? ID);
        Task ReturnAllMeasuringDevicesAsync(int? ID);
    }
}
