using Common.Parameters;
using Model.Common.Inventory;
using PagedList;
using Repository.Common.Inventory;
using Service.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Inventory
{
    public class ItemService : IItemService
    {
        private IItemRepository ItemRepository;
        public ItemService(IItemRepository itemRepository)
        {
            this.ItemRepository = itemRepository;
        }

        #region Get
        public async Task<List<IItem>> GetAllAsync(IItemParameters itemParameters)
        {
            return new List<IItem>(await ItemRepository.GetAllAsync(itemParameters));
        }

        public async Task<StaticPagedList<IItem>> GetAllPagedListAsync(IItemParameters itemParameters)
        {
            var count = await ItemRepository.GetCountAsync(itemParameters);
            var itemList = await ItemRepository.GetAllAsync(itemParameters);
            var itemPagedList = new StaticPagedList<IItem>(itemList, itemParameters.PageNumber.Value, itemParameters.PageSize.Value, count);

            return itemPagedList;
        }
        public async Task<IItem> GetByIdAsync(Guid? ID)
        {
            return await ItemRepository.GetByIdAsync(ID);
        }

        #endregion

        #region Basic CRUD
        public async Task CreateAsync(IItem item)
        {
            await ItemRepository.CreateAsync(item);
        }

        public async Task UpdateAsync(IItem item)
        {
            await ItemRepository.UpdateAsync(item);
        }

        public async Task DeleteAsync(Guid ID)
        {
            await ItemRepository.DeleteAsync(ID);
        }
        #endregion

        #region Assign
        public async Task AssignItemAsync(Guid itemID, Guid? employeeID)
        {
            await ItemRepository.AssignOneItemAsync(itemID, employeeID);
        }
        #endregion
    }
}
