using Common.Parameters;
using Model.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Common.Inventory
{
    public interface IItemRepository
    {
        Task<IEnumerable<IItem>> GetAllAsync(IItemParameters parameters);
        Task<IItem> GetByIdAsync(Guid? ID);
        Task<int> GetCountAsync(IItemParameters parameters);
        Task CreateAsync(IItem item);
        Task UpdateAsync(IItem item);
        Task DeleteAsync(Guid ID);
        Task AssignOneItemAsync(Guid itemID, Guid? employeeID);
    }
}