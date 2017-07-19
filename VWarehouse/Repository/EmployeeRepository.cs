using AutoMapper;
using Common.Parameters;
using Common.Parameters.RepositoryParameters;
using DAL.DbEntities;
using DAL.DbEntities.Inventory;
using Model.Common;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected IGenericRepository Repository { get; private set; }
        protected IGenericRepositoryParameters<EmployeeEntity> GenericParameters { get; private set; }
        public EmployeeRepository(IGenericRepository repository, IGenericRepositoryParameters<EmployeeEntity> parameters)
        {
            this.Repository = repository;
            this.GenericParameters = parameters;
        }

        #region Get
        public async Task<IEmployee> GetByIdAsync(Guid? ID)
        {
            var employee = Mapper.Map<IEmployee>
                (await Repository.GetByIdAsync<EmployeeEntity>(ID));
            return employee;
        }

        public async Task<IEnumerable<IEmployee>> GetAllAsync(IEmployeeParameters parameters)
        {
            GenericParameters = Mapper.Map<IGenericRepositoryParameters<EmployeeEntity>>(parameters);
            if(GenericParameters.Paged)
            {
                switch (GenericParameters.SortOrder)
                {
                    case "Name":
                        GenericParameters.OrderBy = source => source.OrderBy(e => e.Name);
                        break;
                    case "name_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(e => e.Name);
                        break;
                    default:
                        GenericParameters.OrderBy = source => source.OrderBy(e => e.ID);
                        break;
                }
                GenericParameters.Skip = (GenericParameters.PageNumber - 1) * GenericParameters.PageSize;
                GenericParameters.Take = GenericParameters.PageSize;
            }

            if (!String.IsNullOrEmpty(parameters.SearchString))
            {
                GenericParameters.Filter = e => e.Name.Contains(GenericParameters.SearchString);
            }
            var employeeList = (Mapper.Map<List<IEmployee>>(await Repository.GetAllAsync(GenericParameters)));
            return employeeList;
        }

        public async Task<IEmployee> GetOneAsync(IEmployeeParameters parameters)
        {
            GenericParameters = Mapper.Map<IGenericRepositoryParameters<EmployeeEntity>>(parameters);
            GenericParameters.Filter = e => e.ID == GenericParameters.ID;
            var employee = (Mapper.Map<IEmployee>(await Repository.GetOneAsync(GenericParameters)));
            return employee;
        }

        public Task<int> GetCountAsync(IEmployeeParameters parameters)
        {
            if (!String.IsNullOrEmpty(parameters.SearchString))
            {
                GenericParameters.Filter = e => e.Name.Contains(GenericParameters.SearchString);
            }
            return Repository.GetCountAsync(GenericParameters);
        }
        #endregion

        #region CRUD
        public async Task CreateAsync(IEmployee employee)
        {
            var employeeEntity = Mapper.Map<EmployeeEntity>(employee);
            await Repository.CreateAsync(employeeEntity);
        }

        public async Task UpdateAsync(IEmployee employee)
        {
            var employeeEntity = Mapper.Map<EmployeeEntity>(employee);
            await Repository.UpdateAsync(employeeEntity);
        }

        public async Task DeleteAsync(Guid ID)
        {
            IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();
            await unitOfWork.DeleteAsync<EmployeeEntity>(ID);
            await unitOfWork.SaveAsync();
        }
        #endregion

        #region Return
        public async Task ReturnOneItemAsync(Guid? ID)
        {
            var itemEntity = await Repository.GetByIdAsync<ItemEntity>(ID);
            itemEntity.EmployeeID = null;
            await Repository.UpdateAsync(itemEntity);
        }

        public async Task ReturnAllItemsAsync(Guid? ID)
        {
            IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();
            await unitOfWork.ReturnAllItemsAsync(ID);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnOneMeasuringDeviceAsync(Guid? ID)
        {
            var measuringDeviceEntity = await Repository.GetByIdAsync<MeasuringDeviceEntity>(ID);
            measuringDeviceEntity.EmployeeID = null;
            await Repository.UpdateAsync(measuringDeviceEntity);
        }

        public async Task ReturnAllMeasuringDevicesAsync(Guid? ID)
        {
            IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();
            await unitOfWork.ReturnAllMeasuringDevicesAsync(ID);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnOneVehicleAsync(Guid? ID)
        {
            var VehicleEntity = await Repository.GetByIdAsync<VehicleEntity>(ID);
            VehicleEntity.EmployeeID = null;
            await Repository.UpdateAsync(VehicleEntity);
        }

        public async Task ReturnAllVehiclesAsync(Guid? ID)
        {
            IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();
            await unitOfWork.ReturnAllVehiclesAsync(ID);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnAllInventory(Guid? ID)
        {
            try
            {
                IUnitOfWork unitOfWork = Repository.CreateUnitOfWork();
                await unitOfWork.ReturnAllInventory(ID);
                await unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
