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
    public class ItemRepository : GenericRepository<ItemEntity>, IItemRepository
    {
        public ItemRepository(VWarehouseContext context)
            : base(context)
        {
        }
        public async Task<IEnumerable<ItemEntity>> GetAllPagedListAsync(
            Expression<Func<ItemEntity, bool>> filter = null,
            Func<IQueryable<ItemEntity>, IOrderedQueryable<ItemEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }
    }
}