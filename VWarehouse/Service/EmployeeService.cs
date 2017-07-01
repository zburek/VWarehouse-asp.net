using AutoMapper;
using Common;
using DAL.DbEntities;
using DAL.DbEntities.Inventory;
using Model.Common;
using PagedList;
using Repository;
using Repository.Common;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork unitOfWork;
        public EmployeeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Get
        public async Task<List<IEmployee>> GetAllAsync(IParameters<EmployeeEntity> parameters = null)
        {
            return new List<IEmployee>
                (Mapper.Map<List<IEmployee>>
                (await unitOfWork.Employees.GetAllAsync(parameters)));
        }

        public async Task<StaticPagedList<IEmployee>> GetAllPagedListAsync(IParameters<EmployeeEntity> parameters)
        {
            if (!String.IsNullOrEmpty(parameters.SearchString))
            {
                parameters.Filter = e => e.Name.Contains(parameters.SearchString);
            }
            switch (parameters.SortOrder)
            {
                case "Name":
                    parameters.OrderBy = source => source.OrderBy(e => e.Name);
                    break;
                case "name_desc":
                    parameters.OrderBy = source => source.OrderByDescending(e => e.Name);
                    break;
                default:
                    parameters.OrderBy = source => source.OrderBy(e => e.ID);
                    break;
            }
            parameters.Skip = (parameters.PageNumber - 1) * parameters.PageSize;
            parameters.Take = parameters.PageSize;

            var count = await unitOfWork.Employees.GetCountAsync(parameters);
            var employeeList = (Mapper.Map<List<IEmployee>>(await unitOfWork.Employees.GetAllAsync(parameters)));
            var employeePagedList = new StaticPagedList<IEmployee>(employeeList, parameters.PageNumber.Value, parameters.PageSize.Value, count);

            return employeePagedList;
        }

        public async Task<IEmployee> GetByIdAsync(Guid? ID)
        {
            var employee = Mapper.Map<IEmployee>
                (await unitOfWork.Employees.GetByIdAsync(ID));
            return employee;
        }
        public async Task<IEmployee> GetOneAsync(IParameters<EmployeeEntity> parameters)
        {
            var employee = Mapper.Map<IEmployee>
                (await unitOfWork.Employees.GetOneAsync(parameters));
            return employee;
        }
        #endregion

        #region Basic CRUD
        public async Task CreateAsync(IEmployee employee)
        {
            var employeeEntity = Mapper.Map<EmployeeEntity>(employee);
            await unitOfWork.Employees.AddAsync(employeeEntity);
        }

        public async Task UpdateAsync(IEmployee employee)
        {
            var employeeEntity = Mapper.Map<EmployeeEntity>(employee);
            await unitOfWork.Employees.UpdateAsync(employeeEntity);
        }
        
        public async Task DeleteAsync(Guid ID)
        {
            IParameters<ItemEntity> itemParameters = new Parameters<ItemEntity>();
            itemParameters.Filter = i => i.EmployeeID == ID;
            IParameters<MeasuringDeviceEntity> measuringDeviceParameters = new Parameters<MeasuringDeviceEntity>();
            measuringDeviceParameters.Filter = i => i.EmployeeID == ID;
            IParameters<VehicleEntity> vehicleParameters = new Parameters<VehicleEntity>();
            vehicleParameters.Filter = i => i.EmployeeID == ID;

            List<ItemEntity> employeeItems = (Mapper.Map<List<ItemEntity>>
                (await unitOfWork.Items.GetAllAsync(itemParameters)));
            foreach(ItemEntity item in employeeItems)
            {
                await unitOfWork.Items.DeleteAsync(item.ID);
            }

            List<MeasuringDeviceEntity> employeeMeasuringDevices = (Mapper.Map<List<MeasuringDeviceEntity>>
                (await unitOfWork.MeasuringDevices.GetAllAsync(measuringDeviceParameters)));
            foreach (MeasuringDeviceEntity measuringDevice in employeeMeasuringDevices)
            {
                await unitOfWork.MeasuringDevices.DeleteAsync(measuringDevice.ID);
            }

            List<VehicleEntity> employeeVehicles = (Mapper.Map<List<VehicleEntity>>
                (await unitOfWork.Vehicles.GetAllAsync(vehicleParameters)));
            foreach (VehicleEntity vehicle in employeeVehicles)
            {
                await unitOfWork.Vehicles.DeleteAsync(vehicle.ID);
            }

            var employeeEnity = Mapper.Map<EmployeeEntity>(await unitOfWork.Employees.GetByIdAsync(ID));
            await unitOfWork.Employees.DeleteAsync(employeeEnity.ID);
        }
        #endregion
    }
}
