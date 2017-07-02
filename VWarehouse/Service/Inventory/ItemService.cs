using AutoMapper;
using Common;
using DAL.DbEntities.Inventory;
using Model.Common.Inventory;
using PagedList;
using Repository.Common;
using Service.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Inventory
{
    public class ItemService : IItemService
    {
        private IUnitOfWork unitOfWork;
        public ItemService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Get
        public async Task<List<IItem>> GetAllAsync(IParameters<ItemEntity> parameters)
        {
            return new List<IItem>
                (Mapper.Map<List<IItem>>
                (await unitOfWork.Items.GetAllAsync(parameters)));
        }

        public async Task<StaticPagedList<IItem>> GetAllPagedListAsync(IParameters<ItemEntity> parameters)
        {
            if (!String.IsNullOrEmpty(parameters.SearchString) && parameters.Filter == null)
            {
                parameters.Filter = i => i.Name.Contains(parameters.SearchString);
            }
            else if (!String.IsNullOrEmpty(parameters.SearchString) && parameters.Filter != null)
            {
                parameters.Filter = i => i.Name.Contains(parameters.SearchString) && i.EmployeeID == null;
            }
            else if (parameters.Filter != null)
            {
                parameters.Filter = i => i.EmployeeID == null;
            }
            switch (parameters.SortOrder)
            {
                case "Name":
                    parameters.OrderBy = source => source.OrderBy(i => i.Name);
                    break;
                case "name_desc":
                    parameters.OrderBy = source => source.OrderByDescending(i => i.Name);
                    break;
                case "Description":
                    parameters.OrderBy = source => source.OrderBy(i => i.Description);
                    break;
                case "description_desc":
                    parameters.OrderBy = source => source.OrderByDescending(i => i.Name);
                    break;
                case "Employee":
                    parameters.OrderBy = source => source.OrderBy(i => i.Employee.Name);
                    break;
                case "employee_desc":
                    parameters.OrderBy = source => source.OrderByDescending(i => i.Employee.Name);
                    break;
                default:
                    parameters.OrderBy = source => source.OrderBy(i => i.ID);
                    break;
            }
            parameters.Skip = (parameters.PageNumber - 1) * parameters.PageSize;
            parameters.Take = parameters.PageSize;

            var count = await unitOfWork.Items.GetCountAsync(parameters);
            var itemList = (Mapper.Map<List<IItem>>(await unitOfWork.Items.GetAllAsync(parameters)));
            var itemPagedList = new StaticPagedList<IItem>(itemList, parameters.PageNumber.Value, parameters.PageSize.Value, count);

            return itemPagedList;
        }
        public async Task<IItem> GetByIdAsync(Guid? ID)
        {
            var item = Mapper.Map<IItem>(await unitOfWork.Items.GetByIdAsync(ID));
            return item;
        }

        #endregion

        #region Basic CRUD
        public async Task CreateAsync(IItem item)
        {
            var itemEntity = Mapper.Map<ItemEntity>(item);
            await unitOfWork.Items.CreateAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(IItem item)
        {
            var itemEntity = Mapper.Map<ItemEntity>(item);
            await unitOfWork.Items.UpdateAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Guid ID)
        {
            var itemEntity = Mapper.Map<ItemEntity>(await unitOfWork.Items.GetByIdAsync(ID));
            await unitOfWork.Items.DeleteAsync(itemEntity.ID);
            await unitOfWork.SaveAsync();
        }
        #endregion

        #region Assign and Return
        public async Task AssignItemAsync(Guid itemID, Guid? employeeID)
        {
            var itemEntity = await unitOfWork.Items.GetByIdAsync(itemID);
            itemEntity.EmployeeID = employeeID;
            await unitOfWork.Items.UpdateAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }
        public async Task ReturnOneItemAsync(Guid? ID)
        {
            var itemEntity = Mapper.Map<ItemEntity>(await unitOfWork.Items.GetByIdAsync(ID));
            itemEntity.EmployeeID = null;
            await unitOfWork.Items.UpdateAsync(itemEntity);
            await unitOfWork.SaveAsync();
        }
        public async Task ReturnAllItemsAsync(Guid? ID)
        {
            IParameters<ItemEntity> itemParameters = new Parameters<ItemEntity>();
            itemParameters.Filter = i => i.EmployeeID == ID;
            IEnumerable<ItemEntity> itemList = await unitOfWork.Items.GetAllAsync(itemParameters);
            foreach (var itemEntity in itemList)
            {
                itemEntity.EmployeeID = null;
                await unitOfWork.Items.UpdateAsync(itemEntity);
            }
            await unitOfWork.SaveAsync();

            #endregion

        }
    }
}
