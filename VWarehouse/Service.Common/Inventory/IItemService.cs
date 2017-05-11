using Model.Common.Inventory;
using Model.DbEntities.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Common.Inventory
{
    public interface IItemService
    {
        Task<List<IItem>> GetAllAsync(
            Expression<Func<ItemEntity, bool>> filter = null,
            Func<IQueryable<ItemEntity>, IOrderedQueryable<ItemEntity>> orderBy = null,
            string includeProperties = null);
        Task<IItem> GetByIdAsync(int? ID);
        Task CreateAsync(IItem item);
        Task UpdateAsync(IItem item);
        Task DeleteAsync(int ID);
        Task ReturnItemAsync(int ID);
    }
}
