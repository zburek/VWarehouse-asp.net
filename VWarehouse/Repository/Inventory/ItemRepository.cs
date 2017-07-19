using AutoMapper;
using Common.Parameters;
using Common.Parameters.RepositoryParameters;
using DAL.DbEntities.Inventory;
using Model.Common.Inventory;
using Repository.Common;
using Repository.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Inventory
{
    public class ItemRepository : IItemRepository
    {
        protected IGenericRepository Repository { get; private set; }
        protected IGenericRepositoryParameters<ItemEntity> GenericParameters { get; private set; }
        public ItemRepository(IGenericRepository repository, IGenericRepositoryParameters<ItemEntity> parameters)
        {
            this.Repository = repository;
            this.GenericParameters = parameters;
        }

        #region Get
        public async Task<IEnumerable<IItem>> GetAllAsync(IItemParameters parameters)
        {
            GenericParameters = Mapper.Map<IGenericRepositoryParameters<ItemEntity>>(parameters);
            if (GenericParameters.Paged)
            {
                if (!String.IsNullOrEmpty(GenericParameters.SearchString) && parameters.OnStock != true)
                {
                    GenericParameters.Filter = i => i.Name.Contains(GenericParameters.SearchString);
                }
                else if (!String.IsNullOrEmpty(GenericParameters.SearchString) && parameters.OnStock == true)
                {
                    GenericParameters.Filter = i => i.Name.Contains(GenericParameters.SearchString) && i.EmployeeID == null;
                }
                else if (parameters.OnStock == true)
                {
                    GenericParameters.Filter = i => i.EmployeeID == null;
                }

                switch (GenericParameters.SortOrder)
                {
                    case "Name":
                        GenericParameters.OrderBy = source => source.OrderBy(i => i.Name);
                        break;
                    case "name_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(i => i.Name);
                        break;
                    case "Description":
                        GenericParameters.OrderBy = source => source.OrderBy(i => i.Description);
                        break;
                    case "description_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(i => i.Name);
                        break;
                    case "Employee":
                        GenericParameters.OrderBy = source => source.OrderBy(i => i.Employee.Name);
                        break;
                    case "employee_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(i => i.Employee.Name);
                        break;
                    default:
                        GenericParameters.OrderBy = source => source.OrderBy(i => i.ID);
                        break;
                }
                GenericParameters.Skip = (GenericParameters.PageNumber - 1) * GenericParameters.PageSize;
                GenericParameters.Take = GenericParameters.PageSize;
            }

            var itemList = (Mapper.Map<List<IItem>>(await Repository.GetAllAsync(GenericParameters)));
            return itemList;
        }
        public async Task<IItem> GetByIdAsync(Guid? ID)
        {
            var item = Mapper.Map<IItem>
                (await Repository.GetByIdAsync<ItemEntity>(ID));
            return item;
        }

        public Task<int> GetCountAsync(IItemParameters parameters)
        {
            if (!String.IsNullOrEmpty(GenericParameters.SearchString) && parameters.OnStock != true)
            {
                GenericParameters.Filter = i => i.Name.Contains(GenericParameters.SearchString);
            }
            else if (!String.IsNullOrEmpty(GenericParameters.SearchString) && parameters.OnStock == true)
            {
                GenericParameters.Filter = i => i.Name.Contains(GenericParameters.SearchString) && i.EmployeeID == null;
            }
            else if (parameters.OnStock == true)
            {
                GenericParameters.Filter = i => i.EmployeeID == null;
            }
            return Repository.GetCountAsync(GenericParameters);
        }


        #endregion

        #region Basic CRUD
        public async Task CreateAsync(IItem item)
        {
            var itemEntity = Mapper.Map<ItemEntity>(item);
            await Repository.CreateAsync(itemEntity);
        }

        public async Task UpdateAsync(IItem item)
        {
            var itemEntity = Mapper.Map<ItemEntity>(item);
            await Repository.UpdateAsync(itemEntity);
        }

        public async Task DeleteAsync(Guid ID)
        {
            await Repository.DeleteAsync<ItemEntity>(ID);
        }
        #endregion

        #region Assign
        public async Task AssignOneItemAsync(Guid itemID, Guid? employeeID)
        {
            var itemEntity = await Repository.GetByIdAsync<ItemEntity>(itemID);
            itemEntity.EmployeeID = employeeID;
            await Repository.UpdateAsync(itemEntity);
        }
        #endregion
    }
}