using AutoMapper;
using Model.Common;
using Model.Common.Inventory;
using Model.Common.ViewModels;
using Model.DbEntities.Inventory;
using PagedList;
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

        #region Get
        public async Task<List<IItem>> GetAllAsync(
            Expression<Func<ItemEntity, bool>> filter = null,
            Func<IQueryable<ItemEntity>, IOrderedQueryable<ItemEntity>> orderBy = null,
            string includeProperties = null)
        {
            return new List<IItem>
                (Mapper.Map<List<IItem>>
                (await unitOfWork.Items.GetAllAsync(filter, orderBy, includeProperties)));
        }

        public async Task<StaticPagedList<IItem>> GetAllPagedListAsync(
            string searchString, string sortOrder, int pageNumber, int pageSize,
            Expression<Func<ItemEntity, bool>> filter = null)
        {
            Func<IQueryable<ItemEntity>, IOrderedQueryable<ItemEntity>> orderBy = null;
            if (!String.IsNullOrEmpty(searchString) && filter == null)
            {
                filter = i => i.Name.Contains(searchString);
            }
            else if(!String.IsNullOrEmpty(searchString) && filter != null)
            {
                filter = i => i.Name.Contains(searchString) && i.EmployeeID == null;
            }
            else if (filter != null)
            {
                filter = i => i.EmployeeID == null;
            }
            switch (sortOrder)
            {
                case "Name":
                    orderBy = source => source.OrderBy(i => i.Name);
                    break;
                case "name_desc":
                    orderBy = source => source.OrderByDescending(i => i.Name);
                    break;
                case "Description":
                    orderBy = source => source.OrderBy(i => i.Description);
                    break;
                case "description_desc":
                    orderBy = source => source.OrderByDescending(i => i.Name);
                    break;
                case "Employee":
                    orderBy = source => source.OrderBy(i => i.Employee.Name);
                    break;
                case "employee_desc":
                    orderBy = source => source.OrderByDescending(i => i.Employee.Name);
                    break;
                default:
                    orderBy = source => source.OrderBy(i => i.ID);
                    break;
            }
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            var count = await unitOfWork.Items.GetCountAsync(filter);
            var itemList = (Mapper.Map<List<IItem>>(await unitOfWork.Items.GetAllPagedListAsync(filter, orderBy, null, skip, take)));
            var itemPagedList = new StaticPagedList<IItem>(itemList, pageNumber, pageSize, count);

            return itemPagedList;
        }
        public async Task<IItem> GetByIdAsync(int? ID)
        {
            var item = Mapper.Map<IItem>(await unitOfWork.Items.GetByIdAsync(ID));
            return item;
        }
        public async Task<IAssignViewModel> CreateAssignViewModelAsync(int? ID)
        {
            var item = Mapper.Map<IAssignViewModel>(await unitOfWork.Items.GetByIdAsync(ID));
            item.EmployeeList = Mapper.Map<List<IEmployee>>(await unitOfWork.Employees.GetAllAsync(null, null, null));
            return item;
        }

        #endregion

        #region Basic CRUD
        public async Task CreateAsync(IItem item)
        {
            var itemEntity = Mapper.Map<ItemEntity>(item);
            int test = await unitOfWork.Items.AddAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(IItem item)
        {
            var itemEntity = Mapper.Map<ItemEntity>(item);
            int test = await unitOfWork.Items.UpdateAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int ID)
        {
            var itemEntity = Mapper.Map<ItemEntity>(await unitOfWork.Items.GetByIdAsync(ID));
            int test = await unitOfWork.Items.DeleteAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }
        #endregion

        #region Assign and Return
        public async Task AssignItemAsync(IAssignViewModel item)
        {
            var itemEntity = await unitOfWork.Items.GetByIdAsync(item.ID);
            itemEntity.EmployeeID = item.EmployeeID;
            int test = await unitOfWork.Items.UpdateAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }
        public async Task ReturnOneItemAsync(int? ID)
        {
            var itemEntity = Mapper.Map<ItemEntity>(await unitOfWork.Items.GetByIdAsync(ID));
            itemEntity.EmployeeID = null;
            int test = await unitOfWork.Items.UpdateAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }
        public async Task ReturnAllItemsAsync(int? ID)
        {
            Expression<Func<ItemEntity, bool>> filter = i => i.EmployeeID == ID;
            IEnumerable<ItemEntity> itemList = await unitOfWork.Items.GetAllAsync(filter, null, null);
            foreach(var itemEntity in itemList)
            {
                itemEntity.EmployeeID = null;
                int test = await unitOfWork.Items.UpdateAsync(itemEntity);
            }
            await unitOfWork.SaveAsync();
        }
        #endregion

    }
}
