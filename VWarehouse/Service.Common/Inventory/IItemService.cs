using DAL;
using DAL.DbEntities.Inventory;
using Model.Common.Inventory;
using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Common.Inventory
{
    public interface IItemService
    {
        Task<List<IItem>> GetAllAsync(IParameters<ItemEntity> parameters);
        Task<IItem> GetByIdAsync(Guid? ID);
        Task<StaticPagedList<IItem>> GetAllPagedListAsync(IParameters<ItemEntity> parameters);
        Task CreateAsync(IItem item);
        Task UpdateAsync(IItem item);
        Task DeleteAsync(Guid ID);
        Task AssignItemAsync(Guid itemID, Guid? employeeID);
        Task ReturnOneItemAsync(Guid? ID);
        Task ReturnAllItemsAsync(Guid? ID);

    }
}
