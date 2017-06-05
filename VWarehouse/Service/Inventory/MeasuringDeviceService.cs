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
    public class MeasuringDeviceService : IMeasuringDeviceService
    {
        private IUnitOfWork unitOfWork;
        public MeasuringDeviceService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Get
        public async Task<List<IMeasuringDevice>> GetAllAsync(
            Expression<Func<MeasuringDeviceEntity, bool>> filter = null,
            Func<IQueryable<MeasuringDeviceEntity>, IOrderedQueryable<MeasuringDeviceEntity>> orderBy = null,
            string includeProperties = null)
        {
            return new List<IMeasuringDevice>
                (Mapper.Map<List<IMeasuringDevice>>
                (await unitOfWork.MeasuringDevices.GetAllAsync(filter, orderBy, includeProperties)));
        }

        public async Task<StaticPagedList<IMeasuringDevice>> GetAllPagedListAsync(
            string searchString, string sortOrder, int pageNumber, int pageSize,
            Expression<Func<MeasuringDeviceEntity, bool>> filter = null)
        {
            Func<IQueryable<MeasuringDeviceEntity>, IOrderedQueryable<MeasuringDeviceEntity>> orderBy = null;
            if (!String.IsNullOrEmpty(searchString) && filter == null)
            {
                filter = MD => MD.Name.Contains(searchString);
            }
            else if (!String.IsNullOrEmpty(searchString) && filter != null)
            {
                filter = MD => MD.Name.Contains(searchString) && MD.EmployeeID == null;
            }
            else if (filter != null)
            {
                filter = MD => MD.EmployeeID == null;
            }
            switch (sortOrder)
            {
                case "Name":
                    orderBy = source => source.OrderBy(MD => MD.Name);
                    break;
                case "name_desc":
                    orderBy = source => source.OrderByDescending(MD => MD.Name);
                    break;
                case "CalibrationExpirationDate":
                    orderBy = source => source.OrderBy(MD => MD.CalibrationExpirationDate);
                    break;
                case "calibrationExpirationDate_desc":
                    orderBy = source => source.OrderByDescending(MD => MD.CalibrationExpirationDate);
                    break;
                case "Employee":
                    orderBy = source => source.OrderBy(MD => MD.Employee.Name);
                    break;
                case "employee_desc":
                    orderBy = source => source.OrderByDescending(MD => MD.Employee.Name);
                    break;
                default:
                    orderBy = source => source.OrderBy(MD => MD.ID);
                    break;
            }
            var skip = (pageNumber - 1) * pageSize;
            var take = pageSize;

            var count = await unitOfWork.MeasuringDevices.GetCountAsync(filter);
            var measuringDeviceList = (Mapper.Map<List<IMeasuringDevice>>(await unitOfWork.MeasuringDevices.GetAllPagedListAsync(filter, orderBy, null, skip, take)));
            var measuringDevicePagedList = new StaticPagedList<IMeasuringDevice>(measuringDeviceList, pageNumber, pageSize, count);

            return measuringDevicePagedList;
        }
        public async Task<IMeasuringDevice> GetByIdAsync(int? ID)
        {
            var measuringDevice = Mapper.Map<IMeasuringDevice>
                (await unitOfWork.MeasuringDevices.GetByIdAsync(ID));
            return measuringDevice;
        }

        public async Task<IAssignViewModel> CreateAssignViewModelAsync(int? ID)
        {
            var measuringDevice = Mapper.Map<IAssignViewModel>(await unitOfWork.MeasuringDevices.GetByIdAsync(ID));
            measuringDevice.EmployeeList = Mapper.Map<List<IEmployee>>(await unitOfWork.Employees.GetAllAsync(null, null, null));
            return measuringDevice;
        }

        #endregion

        #region CRUD
        public async Task CreateAsync(IMeasuringDevice measuringDevice)
        {
            var measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(measuringDevice);
            int test = await unitOfWork.MeasuringDevices.AddAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(IMeasuringDevice measuringDevice)
        {
            var measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(measuringDevice);
            int test = await unitOfWork.MeasuringDevices.UpdateAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }
        
        public async Task DeleteAsync(int ID)
        {
            var measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(await unitOfWork.MeasuringDevices.GetByIdAsync(ID));
            int test = await unitOfWork.MeasuringDevices.DeleteAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }

        #endregion

        #region Assign and Return
        public async Task AssignMeasuringDeviceAsync(IAssignViewModel measuringDevice)
        {
            var measuringDeviceEntity = await unitOfWork.MeasuringDevices.GetByIdAsync(measuringDevice.ID);
            measuringDeviceEntity.EmployeeID = measuringDevice.EmployeeID;
            int test = await unitOfWork.MeasuringDevices.UpdateAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnOneMeasuringDeviceAsync(int? ID)
        {
            var measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(await unitOfWork.MeasuringDevices.GetByIdAsync(ID));
            measuringDeviceEntity.EmployeeID = null;
            int test = await unitOfWork.MeasuringDevices.UpdateAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnAllMeasuringDevicesAsync(int? ID)
        {
            Expression<Func<MeasuringDeviceEntity, bool>> filter = i => i.EmployeeID == ID;
            IEnumerable<MeasuringDeviceEntity> measuringDeviceList = await unitOfWork.MeasuringDevices.GetAllAsync(filter, null, null);
            foreach (var measuringDeviceEntity in measuringDeviceList)
            {
                measuringDeviceEntity.EmployeeID = null;
                int test = await unitOfWork.MeasuringDevices.UpdateAsync(measuringDeviceEntity);
            }
            await unitOfWork.SaveAsync();
        }
        #endregion
    }
}
