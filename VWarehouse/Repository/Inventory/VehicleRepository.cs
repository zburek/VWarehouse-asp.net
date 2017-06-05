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
    public class VehicleRepository : GenericRepository<VehicleEntity>, IVehicleRepository
    {
        public VehicleRepository(VWarehouseContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<VehicleEntity>> GetAllPagedListAsync(
            Expression<Func<VehicleEntity, bool>> filter = null,
            Func<IQueryable<VehicleEntity>, IOrderedQueryable<VehicleEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }
    }
}
