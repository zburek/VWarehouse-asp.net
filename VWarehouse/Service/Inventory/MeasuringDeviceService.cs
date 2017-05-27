using AutoMapper;
using Model.Common;
using Model.Common.Inventory;
using Model.Common.ViewModels;
using Model.DbEntities.Inventory;
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
            await unitOfWork.MeasuringDevices.AddAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(IMeasuringDevice measuringDevice)
        {
            var measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(measuringDevice);
            await unitOfWork.MeasuringDevices.UpdateAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }
        
        public async Task DeleteAsync(int ID)
        {
            var measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(await unitOfWork.MeasuringDevices.GetByIdAsync(ID));
            await unitOfWork.MeasuringDevices.DeleteAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }

        #endregion

        #region Assign and Return
        public async Task AssignMeasuringDeviceAsync(IAssignViewModel measuringDevice)
        {
            var measuringDeviceEntity = await unitOfWork.MeasuringDevices.GetByIdAsync(measuringDevice.ID);
            measuringDeviceEntity.EmployeeID = measuringDevice.EmployeeID;
            await unitOfWork.MeasuringDevices.UpdateAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnOneMeasuringDeviceAsync(int? ID)
        {
            var measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(await unitOfWork.MeasuringDevices.GetByIdAsync(ID));
            measuringDeviceEntity.EmployeeID = null;
            await unitOfWork.MeasuringDevices.UpdateAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnAllMeasuringDevicesAsync(int? ID)
        {
            Expression<Func<MeasuringDeviceEntity, bool>> filter = i => i.EmployeeID == ID;
            IEnumerable<MeasuringDeviceEntity> measuringDeviceList = await unitOfWork.MeasuringDevices.GetAllAsync(filter, null, null);
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
