using AutoMapper;
using Model.Common.Inventory;
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

        public async Task CreateAsync(IMeasuringDevice measuringDevice)
        {
            MeasuringDeviceEntity measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(measuringDevice);
            await unitOfWork.MeasuringDevices.AddAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(IMeasuringDevice measuringDevice)
        {
            MeasuringDeviceEntity measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(measuringDevice);
            await unitOfWork.MeasuringDevices.UpdateAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }
        
        public async Task DeleteAsync(int ID)
        {
            MeasuringDeviceEntity measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(await unitOfWork.MeasuringDevices.GetByIdAsync(ID));
            await unitOfWork.MeasuringDevices.DeleteAsync(measuringDeviceEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnMeasuringDeviceAsync(int ID)
        {
            MeasuringDeviceEntity measuringDeviceEntity = Mapper.Map<MeasuringDeviceEntity>(await unitOfWork.MeasuringDevices.GetByIdAsync(ID));
            measuringDeviceEntity.EmployeeID = null;
            await unitOfWork.MeasuringDevices.UpdateAsync(measuringDeviceEntity);
        }

    }
}
