
using Model.DbEntities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Common.Inventory
{
    public interface IMeasuringDeviceRepository : IGenericRepository<MeasuringDeviceEntity>
    {
        Task<IEnumerable<MeasuringDeviceEntity>> GetAllPagedListAsync(
            Expression<Func<MeasuringDeviceEntity, bool>> filter = null,
            Func<IQueryable<MeasuringDeviceEntity>, IOrderedQueryable<MeasuringDeviceEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);
    }
}
