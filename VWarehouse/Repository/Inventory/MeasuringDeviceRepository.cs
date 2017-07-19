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
    public class MeasuringDeviceRepository : IMeasuringDeviceRepository
    {
        protected IGenericRepository Repository { get; private set; }
        protected IGenericRepositoryParameters<MeasuringDeviceEntity> GenericParameters { get; private set; }
        public MeasuringDeviceRepository(IGenericRepository repository, IGenericRepositoryParameters<MeasuringDeviceEntity> parameters)
        {
            this.Repository = repository;
            this.GenericParameters = parameters;
        }

        #region Get
        public async Task<IEnumerable<IMeasuringDevice>> GetAllAsync(IMeasuringDeviceParameters parameters)
        {
            GenericParameters = Mapper.Map<IGenericRepositoryParameters<MeasuringDeviceEntity>>(parameters);
            if (GenericParameters.Paged)
            {
                if (!String.IsNullOrEmpty(GenericParameters.SearchString) && parameters.OnStock != true)
                {
                    GenericParameters.Filter = MD => MD.Name.Contains(parameters.SearchString);
                }
                else if (!String.IsNullOrEmpty(GenericParameters.SearchString) && parameters.OnStock == true)
                {
                    GenericParameters.Filter = MD => MD.Name.Contains(parameters.SearchString) && MD.EmployeeID == null;
                }
                else if (parameters.OnStock == true)
                {
                    GenericParameters.Filter = MD => MD.EmployeeID == null;
                }
                switch (GenericParameters.SortOrder)
                {
                    case "Name":
                        GenericParameters.OrderBy = source => source.OrderBy(MD => MD.Name);
                        break;
                    case "name_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(MD => MD.Name);
                        break;
                    case "CalibrationExpirationDate":
                        GenericParameters.OrderBy = source => source.OrderBy(MD => MD.CalibrationExpirationDate);
                        break;
                    case "calibrationExpirationDate_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(MD => MD.CalibrationExpirationDate);
                        break;
                    case "Employee":
                        GenericParameters.OrderBy = source => source.OrderBy(MD => MD.Employee.Name);
                        break;
                    case "employee_desc":
                        GenericParameters.OrderBy = source => source.OrderByDescending(MD => MD.Employee.Name);
                        break;
                    default:
                        GenericParameters.OrderBy = source => source.OrderBy(MD => MD.ID);
                        break;
                }
                GenericParameters.Skip = (GenericParameters.PageNumber - 1) * GenericParameters.PageSize;
                GenericParameters.Take = GenericParameters.PageSize;
            }

            var measuringDeviceList = (Mapper.Map<List<IMeasuringDevice>>(await Repository.GetAllAsync(GenericParameters)));
            return measuringDeviceList;
        }
        public async Task<IMeasuringDevice> GetByIdAsync(Guid? ID)
        {
            var measuringDevice = Mapper.Map<IMeasuringDevice>
                (await Repository.GetByIdAsync<MeasuringDeviceEntity>(ID));
            return measuringDevice;
        }

        public Task<int> GetCountAsync(IMeasuringDeviceParameters parameters)
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

        public async Task<IEnumerable<IMeasuringDevice>> GetMeasuringDeviceCalibraionDateWarning(int daysDifference)
        {
            GenericParameters.Filter = MD => DbFunctions.DiffDays(DateTime.Today, MD.CalibrationExpirationDate) < daysDifference;
            return Mapper.Map<List<IMeasuringDevice>>(await Repository.GetAllAsync(GenericParameters));
        }
        #endregion

        #region Basic CRUD
        public async Task CreateAsync(IMeasuringDevice measuringDevice)
        {
            var measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(measuringDevice);
            await Repository.CreateAsync(measuringDeviceEntity);
        }

        public async Task UpdateAsync(IMeasuringDevice measuringDevice)
        {
            var measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(measuringDevice);
            await Repository.UpdateAsync(measuringDeviceEntity);
        }

        public async Task DeleteAsync(Guid ID)
        {
            await Repository.DeleteAsync<MeasuringDeviceEntity>(ID);
        }
        #endregion

        #region Assign
        public async Task AssignOneMeasuringDeviceAsync(Guid measuringDeviceID, Guid? employeeID)
        {
            var measuringDeviceEntity = await Repository.GetByIdAsync<MeasuringDeviceEntity>(measuringDeviceID);
            measuringDeviceEntity.EmployeeID = employeeID;
            await Repository.UpdateAsync(measuringDeviceEntity);
        }
        #endregion
    }
}
