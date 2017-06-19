using AutoMapper;
using DAL;
using DAL.DbEntities.Inventory;
using Model.Common.Inventory;
using PagedList;
using Repository;
using Repository.Common;
using Service.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<List<IVehicle>> GetAllAsync(IParameters<VehicleEntity> parameters)
        {
            return new List<IVehicle>
                (Mapper.Map<List<IVehicle>>
                (await unitOfWork.Vehicles.GetAllAsync(parameters)));
        }

        public async Task<StaticPagedList<IVehicle>> GetAllPagedListAsync(IParameters<VehicleEntity> parameters)
        {
            if (!String.IsNullOrEmpty(parameters.SearchString) && parameters.Filter == null)
            {
                parameters.Filter = v => v.Name.Contains(parameters.SearchString);
            }
            else if (!String.IsNullOrEmpty(parameters.SearchString) && parameters.Filter != null)
            {
                parameters.Filter = v => v.Name.Contains(parameters.SearchString) && v.EmployeeID == null;
            }
            else if (parameters.Filter != null)
            {
                parameters.Filter = v => v.EmployeeID == null;
            }
            #region This is ridiculous, need another way
            switch (parameters.SortOrder)
            {
                case "Name":
                    parameters.OrderBy = source => source.OrderBy(v => v.Name);
                    break;
                case "name_desc":
                    parameters.OrderBy = source => source.OrderByDescending(v => v.Name);
                    break;
                case "Type":
                    parameters.OrderBy = source => source.OrderBy(v => v.Type);
                    break;
                case "type_desc":
                    parameters.OrderBy = source => source.OrderByDescending(v => v.Type);
                    break;
                case "LicenseExpirationDate":
                    parameters.OrderBy = source => source.OrderBy(v => v.LicenseExpirationDate);
                    break;
                case "licenseExpirationDate_desc":
                    parameters.OrderBy = source => source.OrderByDescending(v => v.LicenseExpirationDate);
                    break;
                case "Mileage":
                    parameters.OrderBy = source => source.OrderBy(v => v.Mileage);
                    break;
                case "mileage_desc":
                    parameters.OrderBy = source => source.OrderByDescending(v => v.Mileage);
                    break;
                case "NextService":
                    parameters.OrderBy = source => source.OrderBy(v => v.NextService);
                    break;
                case "nextService_desc":
                    parameters.OrderBy = source => source.OrderByDescending(v => v.NextService);
                    break;
                case "Employee":
                    parameters.OrderBy = source => source.OrderBy(v => v.Employee.Name);
                    break;
                case "employee_desc":
                    parameters.OrderBy = source => source.OrderByDescending(v => v.Employee.Name);
                    break;
                default:
                    parameters.OrderBy = source => source.OrderBy(v => v.ID);
                    break;
            }
            #endregion
            parameters.Skip = (parameters.PageNumber - 1) * parameters.PageSize;
            parameters.Take = parameters.PageSize;

            var count = await unitOfWork.Vehicles.GetCountAsync(parameters);
            var vehicleList = (Mapper.Map<List<IVehicle>>(await unitOfWork.Vehicles.GetAllAsync(parameters)));
            var vehiclePagedList = new StaticPagedList<IVehicle>(vehicleList, parameters.PageNumber.Value, parameters.PageSize.Value, count);

            return vehiclePagedList;
        }
        public async Task<IVehicle> GetByIdAsync(Guid? ID)
        {
            var vehicle = Mapper.Map<IVehicle>
                (await unitOfWork.Vehicles.GetByIdAsync(ID));
            return vehicle;
        }
        #endregion

        #region CRUD
        public async Task CreateAsync(IVehicle vehicle)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(vehicle);
            int test = await unitOfWork.Vehicles.CreateAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(IVehicle vehicle)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(vehicle);
            int test = await unitOfWork.Vehicles.UpdateAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Guid ID)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(await unitOfWork.Vehicles.GetByIdAsync(ID));
            int test = await unitOfWork.Vehicles.DeleteAsync(vehicleEntity.ID);
            await unitOfWork.SaveAsync();
        }

        #endregion

        #region Assign and Return
        public async Task AssignVehicleAsync(Guid itemID, Guid? employeeID)
        {
            var vehicleEntity = await unitOfWork.Vehicles.GetByIdAsync(itemID);
            vehicleEntity.EmployeeID = employeeID;
            int test = await unitOfWork.Vehicles.UpdateAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }
        public async Task ReturnOneVehicleAsync(Guid? ID)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(await unitOfWork.Vehicles.GetByIdAsync(ID));
            vehicleEntity.EmployeeID = null;
            int test = await unitOfWork.Vehicles.UpdateAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnAllVehiclesAsync(Guid? ID)
        {
            IParameters<VehicleEntity> vehicleParameters = new Parameters<VehicleEntity>();
            vehicleParameters.Filter = i => i.EmployeeID == ID;
            IEnumerable<VehicleEntity> vehicleList = await unitOfWork.Vehicles.GetAllAsync(vehicleParameters);
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
