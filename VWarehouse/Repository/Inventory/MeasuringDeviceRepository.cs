using DAL;
using Model.DbEntities.Inventory;
using Repository.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Inventory
{
    public class MeasuringDeviceRepository : GenericRepository<MeasuringDeviceEntity>, IMeasuringDeviceRepository
    {
        public MeasuringDeviceRepository(VWarehouseContext context)
            : base(context)
        {
        }
        public async Task<IEnumerable<MeasuringDeviceEntity>> GetAllPagedListAsync(
            Expression<Func<MeasuringDeviceEntity, bool>> filter = null,
            Func<IQueryable<MeasuringDeviceEntity>, IOrderedQueryable<MeasuringDeviceEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }
    }
}
