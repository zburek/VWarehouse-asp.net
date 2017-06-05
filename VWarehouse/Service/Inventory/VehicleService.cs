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
    public class VehicleService : IVehicleService
    {
        private IUnitOfWork unitOfWork;
        public VehicleService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Get
        public async Task<List<IVehicle>> GetAllAsync(
            Expression<Func<VehicleEntity, bool>> filter = null,
            Func<IQueryable<VehicleEntity>, IOrderedQueryable<VehicleEntity>> orderBy = null,
            string includeProperties = null)
        {
            return new List<IVehicle>
                (Mapper.Map<List<IVehicle>>
                (await unitOfWork.Vehicles.GetAllAsync(filter, orderBy, includeProperties)));
        }

        public async Task<StaticPagedList<IVehicle>> GetAllPagedListAsync(
            string searchString, string sortOrder, int pageNumber, int pageSize,
            Expression<Func<VehicleEntity, bool>> filter = null)
        {
            Func<IQueryable<VehicleEntity>, IOrderedQueryable<VehicleEntity>> orderBy = null;
            if (!String.IsNullOrEmpty(searchString) && filter == null)
            {
                filter = v => v.Name.Contains(searchString);
            }
            else if (!String.IsNullOrEmpty(searchString) && filter != null)
            {
                filter = v => v.Name.Contains(searchString) && v.EmployeeID == null;
            }
            else if (filter != null)
            {
                filter = v => v.EmployeeID == null;
            }
            #region This is ridiculous, need another way
            switch (sortOrder)
            {
                case "Name":
                    orderBy = source => source.OrderBy(v => v.Name);
                    break;
                case "name_desc":
                    orderBy = source => source.OrderByDescending(v => v.Name);
                    break;
                case "Type":
                    orderBy = source => source.OrderBy(v => v.Type);
                    break;
                case "type_desc":
                    orderBy = source => source.OrderByDescending(v => v.Type);
                    break;
                case "LicenseExpirationDate":
                    orderBy = source => source.OrderBy(v => v.LicenseExpirationDate);
                    break;
                case "licenseExpirationDate_desc":
                    orderBy = source => source.OrderByDescending(v => v.LicenseExpirationDate);
                    break;
                case "Mileage":
                    orderBy = source => source.OrderBy(v => v.Mileage);
                    break;
                case "mileage_desc":
                    orderBy = source => source.OrderByDescending(v => v.Mileage);
                    break;
                case "NextService":
                    orderBy = source => source.OrderBy(v => v.NextService);
                    break;
                case "nextService_desc":
                    orderBy = source => source.OrderByDescending(v => v.NextService);
                    break;
                case "Employee":
                    orderBy = source => source.OrderBy(v => v.Employee.Name);
                    break;
                case "employee_desc":
                    orderBy = source => source.OrderByDescending(v => v.Employee.Name);
                    break;
                default:
                    orderBy = source => source.OrderBy(v => v.ID);
                    break;
            }
            #endregion
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            var count = await unitOfWork.Vehicles.GetCountAsync(filter);
            var vehicleList = (Mapper.Map<List<IVehicle>>(await unitOfWork.Vehicles.GetAllPagedListAsync(filter, orderBy, null, skip, take)));
            var vehiclePagedList = new StaticPagedList<IVehicle>(vehicleList, pageNumber, pageSize, count);

            return vehiclePagedList;
        }
        public async Task<IVehicle> GetByIdAsync(int? ID)
        {
            var vehicle = Mapper.Map<IVehicle>
                (await unitOfWork.Vehicles.GetByIdAsync(ID));
            return vehicle;
        }
        public async Task<IAssignViewModel> CreateAssignViewModelAsync(int? ID)
        {
            var vehicle = Mapper.Map<IAssignViewModel>(await unitOfWork.Vehicles.GetByIdAsync(ID));
            vehicle.EmployeeList = Mapper.Map<List<IEmployee>>(await unitOfWork.Employees.GetAllAsync(null, null, null));
            return vehicle;
        }

        #endregion

        #region CRUD
        public async Task CreateAsync(IVehicle vehicle)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(vehicle);
            int test = await unitOfWork.Vehicles.AddAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(IVehicle vehicle)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(vehicle);
            int test = await unitOfWork.Vehicles.UpdateAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int ID)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(await unitOfWork.Vehicles.GetByIdAsync(ID));
            int test = await unitOfWork.Vehicles.DeleteAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        #endregion

        #region Assign and Return
        public async Task AssignVehicleAsync(IAssignViewModel vehicle)
        {
            var vehicleEntity = await unitOfWork.Vehicles.GetByIdAsync(vehicle.ID);
            vehicleEntity.EmployeeID = vehicle.EmployeeID;
            int test = await unitOfWork.Vehicles.UpdateAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }
        public async Task ReturnOneVehicleAsync(int? ID)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(await unitOfWork.Vehicles.GetByIdAsync(ID));
            vehicleEntity.EmployeeID = null;
            int test = await unitOfWork.Vehicles.UpdateAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnAllVehiclesAsync(int? ID)
        {
            Expression<Func<VehicleEntity, bool>> filter = i => i.EmployeeID == ID;
            IEnumerable<VehicleEntity> vehicleList = await unitOfWork.Vehicles.GetAllAsync(filter, null, null);
            foreach (var vehicleEntity in vehicleList)
            {
                vehicleEntity.EmployeeID = null;
                int test = await unitOfWork.Vehicles.UpdateAsync(vehicleEntity);
            }
            await unitOfWork.SaveAsync();
        }
        #endregion
    }
}
