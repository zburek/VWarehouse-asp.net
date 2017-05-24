using AutoMapper;
using Model.Common;
using Model.Common.Inventory;
using Model.Common.ViewModels;
using Model.DbEntities.Inventory;
using Repository;
using Repository.Common;
using Service.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Inventory
{
    public class ItemService : IItemService
    {
        private IUnitOfWork unitOfWork;
        public ItemService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<IItem>> GetAllAsync(
            Expression<Func<ItemEntity, bool>> filter = null,
            Func<IQueryable<ItemEntity>, IOrderedQueryable<ItemEntity>> orderBy = null,
            string includeProperties = null)
        {
            return new List<IItem>
                (Mapper.Map<List<IItem>>
                (await unitOfWork.Items.GetAllAsync(filter, orderBy, includeProperties)));
        }

        public async Task<IItem> GetByIdAsync(int? ID)
        {
            var item = Mapper.Map<IItem>(await unitOfWork.Items.GetByIdAsync(ID));
            return item;
        }

        public async Task CreateAsync(IItem item)
        {
            var itemEntity = Mapper.Map<ItemEntity>(item);
            await unitOfWork.Items.AddAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(IItem item)
        {
            var itemEntity = Mapper.Map<ItemEntity>(item);
            await unitOfWork.Items.UpdateAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int ID)
        {
            var itemEntity = Mapper.Map<ItemEntity>(await unitOfWork.Items.GetByIdAsync(ID));
            await unitOfWork.Items.DeleteAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task<IAssignViewModel> CreateAssignViewModelAsync(int? ID)
        {
            var item = Mapper.Map<IAssignViewModel>(await unitOfWork.Items.GetByIdAsync(ID));
            item.EmployeeList = Mapper.Map<List<IEmployee>>(await unitOfWork.Employees.GetAllAsync(null, null, null));
            return item;
        }

        public async Task AssignItemAsync(IAssignViewModel item)
        {
            var itemEntity = await unitOfWork.Items.GetByIdAsync(item.ID);
            itemEntity.EmployeeID = item.EmployeeID;
            await unitOfWork.Items.UpdateAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }
        public async Task ReturnOneItemAsync(int? ID)
        {
            var itemEntity = Mapper.Map<ItemEntity>(await unitOfWork.Items.GetByIdAsync(ID));
            itemEntity.EmployeeID = null;
            await unitOfWork.Items.UpdateAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }
    }
}
