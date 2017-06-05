using AutoMapper;
using Model.Common;
using Model.DbEntities;
using Model.DbEntities.Inventory;
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
        public async Task<List<IEmployee>> GetAllAsync(
            Expression<Func<EmployeeEntity, bool>> filter = null,
            Func<IQueryable<EmployeeEntity>, IOrderedQueryable<EmployeeEntity>> orderBy = null,
            string includeProperties = null)
        {
            return new List<IEmployee>
                (Mapper.Map<List<IEmployee>>
                (await unitOfWork.Employees.GetAllAsync(filter, orderBy, includeProperties)));
        }

        public async Task<StaticPagedList<IEmployee>> GetAllPagedListAsync(
            string searchString, string sortOrder, int pageNumber, int pageSize)
        {
            Expression<Func<EmployeeEntity, bool>> filter = null;
            Func<IQueryable<EmployeeEntity>, IOrderedQueryable<EmployeeEntity>> orderBy = null;
            if (!String.IsNullOrEmpty(searchString))
            {
                filter = e => e.Name.Contains(searchString);
            }
            switch (sortOrder)
            {
                case "Name":
                    orderBy = source => source.OrderBy(e => e.Name);
                    break;
                case "name_desc":
                    orderBy = source => source.OrderByDescending(e => e.Name);
                    break;
                default:
                    orderBy = source => source.OrderBy(e => e.ID);
                    break;
            }
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            var count = await unitOfWork.Employees.GetCountAsync(filter);
            var employeeList = (Mapper.Map<List<IEmployee>>(await unitOfWork.Employees.GetAllPagedListAsync(filter, orderBy, null, skip, take)));
            var employeePagedList = new StaticPagedList<IEmployee>(employeeList, pageNumber, pageSize, count);

            return employeePagedList;
        }

        public async Task<IEmployee> GetByIdAsync(int? ID)
        {
            var employee = Mapper.Map<IEmployee>
                (await unitOfWork.Employees.GetByIdAsync(ID));
            return employee;
        }
        public async Task<IEmployee> GetOneAsync(
            Expression<Func<EmployeeEntity, bool>> filter = null,
            string includeProperties = null)
        {
            var employee = Mapper.Map<IEmployee>
                (await unitOfWork.Employees.GetOneAsync(filter, includeProperties));
            return employee;
        }
        #endregion

        #region Basic CRUD
        public async Task CreateAsync(IEmployee employee)
        {
            var employeeEntity = Mapper.Map<EmployeeEntity>(employee);
            int test = await unitOfWork.Employees.AddAsync(employeeEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(IEmployee employee)
        {
            var employeeEntity = Mapper.Map<EmployeeEntity>(employee);
            int test = await unitOfWork.Employees.UpdateAsync(employeeEntity);
            await unitOfWork.SaveAsync();
        }
        
        public async Task DeleteAsync(int ID)
        {
            Expression<Func<ItemEntity, bool>> filterItem = i => i.EmployeeID == ID;
            Expression<Func<MeasuringDeviceEntity, bool>> filterMeasuringDevice = MD => MD.EmployeeID == ID;
            Expression<Func<VehicleEntity, bool>> filterVehicle = v => v.EmployeeID == ID;

            List<ItemEntity> employeeItems = (Mapper.Map<List<ItemEntity>>
                (await unitOfWork.Items.GetAllAsync(filterItem, null, null)));
            foreach(ItemEntity item in employeeItems)
            {
                int testItem = await unitOfWork.Items.DeleteAsync(item);
            }

            List<MeasuringDeviceEntity> employeeMeasuringDevices = (Mapper.Map<List<MeasuringDeviceEntity>>
                (await unitOfWork.MeasuringDevices.GetAllAsync(filterMeasuringDevice, null, null)));
            foreach (MeasuringDeviceEntity measuringDevice in employeeMeasuringDevices)
            {
                int testMeasuringDevice = await unitOfWork.MeasuringDevices.DeleteAsync(measuringDevice);
            }

            List<VehicleEntity> employeeVehicles = (Mapper.Map<List<VehicleEntity>>
                (await unitOfWork.Vehicles.GetAllAsync(filterVehicle, null, null)));
            foreach (VehicleEntity vehicle in employeeVehicles)
            {
                int testVehicle = await unitOfWork.Vehicles.DeleteAsync(vehicle);
            }

            var employeeEnity = Mapper.Map<EmployeeEntity>(await unitOfWork.Employees.GetByIdAsync(ID));
            int test = await unitOfWork.Employees.DeleteAsync(employeeEnity);
            await unitOfWork.SaveAsync();
        }
        #endregion
    }
}
