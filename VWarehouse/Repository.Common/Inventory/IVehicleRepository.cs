
using Model.DbEntities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Common.Inventory
{
    public interface IVehicleRepository : IGenericRepository<VehicleEntity>
    {
        Task<IEnumerable<VehicleEntity>> GetAllPagedListAsync(
            Expression<Func<VehicleEntity, bool>> filter = null,
            Func<IQueryable<VehicleEntity>, IOrderedQueryable<VehicleEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);
    }
}
