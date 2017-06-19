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
        public ItemRepository(IGenericRepository<ItemEntity> repository)
        {
            this.Repository = repository;
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
        public async Task CreateAsync(ItemEntity itemEntity)
        {
            int addTest = await Repository.AddAsync(itemEntity);
            int saveTest = await Repository.SaveAsync();
        }

        public async Task UpdateAsync(ItemEntity itemEntity)
        {
            int updateTest = await Repository.UpdateAsync(itemEntity);
            int saveTest = await Repository.SaveAsync();
        }

        public async Task DeleteAsync(Guid ID)
        {
            int updateTest = await Repository.DeleteAsync(ID);
            int saveTest = await Repository.SaveAsync();
        }
        #endregion
    }
}