using DAL;
using DAL.DbEntities.Inventory;
using Repository.Common;
using Repository.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Inventory
{
    public class ItemRepository : IItemRepository
    {
        protected IGenericRepository<ItemEntity> Repository { get; private set; }
        public ItemRepository(VWarehouseContext context)
        {
            this.Repository = new GenericRepository<ItemEntity>(context);
        }

        #region Get
        public async Task<IEnumerable<ItemEntity>> GetAllAsync(IParameters<ItemEntity> parameters)
        {
            return await Repository.GetAllAsync(parameters);
        }
        public async Task<ItemEntity> GetByIdAsync(Guid? ID)
        {
            return await Repository.GetByIdAsync(ID);
        }

        public Task<int> GetCountAsync(IParameters<ItemEntity> parameters)
        {
            return Repository.GetCountAsync(parameters);
        }
        #endregion

        #region Basic CRUD
        public async Task<int> CreateAsync(ItemEntity itemEntity)
        {
            return await Repository.AddAsync(itemEntity);
        }

        public async Task<int> UpdateAsync(ItemEntity itemEntity)
        {
            return await Repository.UpdateAsync(itemEntity);
        }

        public async Task<int> DeleteAsync(Guid ID)
        {
            return await Repository.DeleteAsync(ID);
        }
        #endregion
    }
}