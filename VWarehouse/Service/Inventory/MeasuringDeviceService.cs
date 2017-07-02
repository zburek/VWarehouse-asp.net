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
    public class MeasuringDeviceService : IMeasuringDeviceService
    {
        private IUnitOfWork unitOfWork;
        public MeasuringDeviceService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Get
        public async Task<List<IMeasuringDevice>> GetAllAsync(IParameters<MeasuringDeviceEntity> parameters)
        {
            return new List<IMeasuringDevice>
                (Mapper.Map<List<IMeasuringDevice>>
                (await unitOfWork.MeasuringDevices.GetAllAsync(parameters)));
        }

        public async Task<StaticPagedList<IMeasuringDevice>> GetAllPagedListAsync(IParameters<MeasuringDeviceEntity> parameters)
        {
            if (!String.IsNullOrEmpty(parameters.SearchString) && parameters.Filter == null)
            {
                parameters.Filter = MD => MD.Name.Contains(parameters.SearchString);
            }
            else if (!String.IsNullOrEmpty(parameters.SearchString) && parameters.Filter != null)
            {
                parameters.Filter = MD => MD.Name.Contains(parameters.SearchString) && MD.EmployeeID == null;
            }
            else if (parameters.Filter != null)
            {
                parameters.Filter = MD => MD.EmployeeID == null;
            }
            switch (parameters.SortOrder)
            {
                case "Name":
                    parameters.OrderBy = source => source.OrderBy(MD => MD.Name);
                    break;
                case "name_desc":
                    parameters.OrderBy = source => source.OrderByDescending(MD => MD.Name);
                    break;
                case "CalibrationExpirationDate":
                    parameters.OrderBy = source => source.OrderBy(MD => MD.CalibrationExpirationDate);
                    break;
                case "calibrationExpirationDate_desc":
                    parameters.OrderBy = source => source.OrderByDescending(MD => MD.CalibrationExpirationDate);
                    break;
                case "Employee":
                    parameters.OrderBy = source => source.OrderBy(MD => MD.Employee.Name);
                    break;
                case "employee_desc":
                    parameters.OrderBy = source => source.OrderByDescending(MD => MD.Employee.Name);
                    break;
                default:
                    parameters.OrderBy = source => source.OrderBy(MD => MD.ID);
                    break;
            }
            parameters.Skip = (parameters.PageNumber - 1) * parameters.PageSize;
            parameters.Take = parameters.PageSize;

            var count = await unitOfWork.MeasuringDevices.GetCountAsync(parameters);
            var measuringDeviceList = (Mapper.Map<List<IMeasuringDevice>>(await unitOfWork.MeasuringDevices.GetAllAsync(parameters)));
            var measuringDevicePagedList = new StaticPagedList<IMeasuringDevice>(measuringDeviceList, parameters.PageNumber.Value, parameters.PageSize.Value, count);

            return measuringDevicePagedList;
        }
        public async Task<IMeasuringDevice> GetByIdAsync(Guid? ID)
        {
            var measuringDevice = Mapper.Map<IMeasuringDevice>
                (await unitOfWork.MeasuringDevices.GetByIdAsync(ID));
            return measuringDevice;
        }
        #endregion

        #region CRUD
        public async Task CreateAsync(IMeasuringDevice measuringDevice)
        {
            var measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(measuringDevice);
            await unitOfWork.MeasuringDevices.CreateAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(IMeasuringDevice measuringDevice)
        {
            var measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(measuringDevice);
            await unitOfWork.MeasuringDevices.UpdateAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }
        
        public async Task DeleteAsync(Guid ID)
        {
            var measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(await unitOfWork.MeasuringDevices.GetByIdAsync(ID));
            await unitOfWork.MeasuringDevices.DeleteAsync(measuringDeviceEntity.ID);
            await unitOfWork.SaveAsync();
        }

        #endregion

        #region Assign and Return
        public async Task AssignMeasuringDeviceAsync(Guid itemID, Guid? employeeID)
        {
            var measuringDeviceEntity = await unitOfWork.MeasuringDevices.GetByIdAsync(itemID);
            measuringDeviceEntity.EmployeeID = employeeID;
            await unitOfWork.MeasuringDevices.UpdateAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnOneMeasuringDeviceAsync(Guid? ID)
        {
            var measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(await unitOfWork.MeasuringDevices.GetByIdAsync(ID));
            measuringDeviceEntity.EmployeeID = null;
            await unitOfWork.MeasuringDevices.UpdateAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnAllMeasuringDevicesAsync(Guid? ID)
        {
            IParameters<MeasuringDeviceEntity> measuringDeviceParameters = new Parameters<MeasuringDeviceEntity>();
            measuringDeviceParameters.Filter = i => i.EmployeeID == ID;
            IEnumerable<MeasuringDeviceEntity> measuringDeviceList = await unitOfWork.MeasuringDevices.GetAllAsync(measuringDeviceParameters);
            foreach (var measuringDeviceEntity in measuringDeviceList)
            {
                measuringDeviceEntity.EmployeeID = null;
                await unitOfWork.MeasuringDevices.UpdateAsync(measuringDeviceEntity);
            }
            await unitOfWork.SaveAsync();
        }
        #endregion
    }
}
