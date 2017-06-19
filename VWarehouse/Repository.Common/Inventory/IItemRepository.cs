using DAL;
using DAL.DbEntities.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Common.Inventory
{
    public interface IItemRepository
    {
        Task<IEnumerable<ItemEntity>> GetAllAsync(IParameters<ItemEntity> parameters);
        Task<ItemEntity> GetByIdAsync(Guid? ID);
        Task<int> GetCountAsync(IParameters<ItemEntity> parameters);
        Task<int> CreateAsync(ItemEntity itemEntity);
        Task<int> UpdateAsync(ItemEntity itemEntity);
        Task<int> DeleteAsync(Guid ID);
    }
}
