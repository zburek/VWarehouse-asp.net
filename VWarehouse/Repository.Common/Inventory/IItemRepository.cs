
using Model.DbEntities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Common.Inventory
{
    public interface IItemRepository : IGenericRepository<ItemEntity>
    {
        Task<IEnumerable<ItemEntity>> GetAllPagedListAsync(
            Expression<Func<ItemEntity, bool>> filter = null,
            Func<IQueryable<ItemEntity>, IOrderedQueryable<ItemEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);
    }
}
