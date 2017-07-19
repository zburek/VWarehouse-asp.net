using AutoMapper;
using Common.Parameters;
using Common.Parameters.RepositoryParameters;
using DAL.DbEntities.Inventory;
using Model.Common.Inventory;
using Repository.Common;
using Repository.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Inventory
{
    public class VehicleRepository : IVehicleRepository
    {
        protected IGenericRepository Repository { get; private set; }
        protected IGenericRepositoryParameters<VehicleEntity> GenericParameters { get; private set; }
        public VehicleRepository(IGenericRepository repository, IGenericRepositoryParameters<VehicleEntity> parameters)
        {
            this.Repository = repository;
            this.GenericParameters = parameters;
        }

        #region Get
        public async Task<IEnumerable<IVehicle>> GetAllAsync(IVehicleParameters parameters)
        {
            GenericParameters = Mapper.Map<IGenericRepositoryParameters<VehicleEntity>>(parameters);
            if (parameters.Paged)
            {
                if (!String.IsNullOrEmpty(GenericParameters.SearchString) && parameters.OnStock != true)
                {
                    GenericParameters.Filter = v => v.Name.Contains(GenericParameters.SearchString);
                }
                else if (!String.IsNullOrEmpty(GenericParameters.SearchString) && parameters.OnStock == true)
                {
                    GenericParameters.Filter = v => v.Name.Contains(GenericParameters.SearchString) && v.EmployeeID == null;
                }
                else if (parameters.OnStock == true)
                {
                    GenericParameters.Filter = v => v.EmployeeID == null;
                }
                #region Possible to add orderby and dec/asc field to parameters to reduce code here
                switch (GenericParameters.SortOrder)
                {
                    case "Name":
                        GenericParameters.OrderBy = source => source.OrderBy(v => v.Name);
                        break;
                    case "name_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(v => v.Name);
                        break;
                    case "Type":
                        GenericParameters.OrderBy = source => source.OrderBy(v => v.Type);
                        break;
                    case "type_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(v => v.Type);
                        break;
                    case "LicenseExpirationDate":
                        GenericParameters.OrderBy = source => source.OrderBy(v => v.LicenseExpirationDate);
                        break;
                    case "licenseExpirationDate_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(v => v.LicenseExpirationDate);
                        break;
                    case "Mileage":
                        GenericParameters.OrderBy = source => source.OrderBy(v => v.Mileage);
                        break;
                    case "mileage_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(v => v.Mileage);
                        break;
                    case "NextService":
                        GenericParameters.OrderBy = source => source.OrderBy(v => v.NextService);
                        break;
                    case "nextService_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(v => v.NextService);
                        break;
                    case "Employee":
                        GenericParameters.OrderBy = source => source.OrderBy(v => v.Employee.Name);
                        break;
                    case "employee_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(v => v.Employee.Name);
                        break;
                    default:
                        GenericParameters.OrderBy = source => source.OrderBy(v => v.ID);
                        break;
                }
                #endregion
                GenericParameters.Skip = (GenericParameters.PageNumber - 1) * GenericParameters.PageSize;
                GenericParameters.Take = GenericParameters.PageSize;
            }
            var vehicleList = (Mapper.Map<List<IVehicle>>(await Repository.GetAllAsync(GenericParameters)));
            return vehicleList;
        }
        public async Task<IVehicle> GetByIdAsync(Guid? ID)
        {
            var vehicle = Mapper.Map<IVehicle>
                (await Repository.GetByIdAsync<VehicleEntity>(ID));
            return vehicle;
        }

        public Task<int> GetCountAsync(IVehicleParameters parameters)
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

        public async Task<List<IVehicle>> GetVehicleLicenseDateWarning(int daysDifference)
        {
            GenericParameters.Filter = VLP => DbFunctions.DiffDays(DateTime.Today, VLP.LicenseExpirationDate) < daysDifference;
            return Mapper.Map<List<IVehicle>>(await Repository.GetAllAsync(GenericParameters));
        }

        public async Task<List<IVehicle>> GetVehicleMileageWarning(int mileageDifference)
        {
            GenericParameters.Filter = VM => (VM.NextService - VM.Mileage) < mileageDifference;
            return Mapper.Map<List<IVehicle>>(await Repository.GetAllAsync(GenericParameters));
        }
        #endregion

        #region Basic CRUD
        public async Task CreateAsync(IVehicle vehicle)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(vehicle);
            await Repository.CreateAsync(vehicleEntity);
        }

        public async Task UpdateAsync(IVehicle vehicle)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(vehicle);
            await Repository.UpdateAsync(vehicleEntity);
        }

        public async Task DeleteAsync(Guid ID)
        {
            await Repository.DeleteAsync<VehicleEntity>(ID);
        }
        #endregion

        #region Assign
        public async Task AssignOneVehicleAsync(Guid vehicleID, Guid? employeeID)
        {
            var vehicleEntity = await Repository.GetByIdAsync<VehicleEntity>(vehicleID);
            vehicleEntity.EmployeeID = employeeID;
            await Repository.UpdateAsync(vehicleEntity);
        }
        #endregion
    }
}
