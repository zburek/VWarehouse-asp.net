using Common.Parameters;
using Model.Common.Inventory;
using PagedList;
using Repository.Common.Inventory;
using Service.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Inventory
{
    public class MeasuringDeviceService : IMeasuringDeviceService
    {
        private IMeasuringDeviceRepository MeasuringDeviceRepository;
        public MeasuringDeviceService(IMeasuringDeviceRepository measuringDeviceRepository)
        {
            this.MeasuringDeviceRepository = measuringDeviceRepository;
        }

        #region Get
        public async Task<List<IMeasuringDevice>> GetAllAsync(IMeasuringDeviceParameters measuringDeviceParameters)
        {
            return new List<IMeasuringDevice>(await MeasuringDeviceRepository.GetAllAsync(measuringDeviceParameters));
        }

        public async Task<StaticPagedList<IMeasuringDevice>> GetAllPagedListAsync(IMeasuringDeviceParameters measuringDeviceParameters)
        {
            var count = await MeasuringDeviceRepository.GetCountAsync(measuringDeviceParameters);
            var measuringDeviceList = await MeasuringDeviceRepository.GetAllAsync(measuringDeviceParameters);
            var measuringDevicePagedList = new StaticPagedList<IMeasuringDevice>(measuringDeviceList, measuringDeviceParameters.PageNumber.Value, measuringDeviceParameters.PageSize.Value, count);

            return measuringDevicePagedList;
        }
        public async Task<IMeasuringDevice> GetByIdAsync(Guid? ID)
        {
            return await MeasuringDeviceRepository.GetByIdAsync(ID);
        }
        #endregion

        #region CRUD
        public async Task CreateAsync(IMeasuringDevice measuringDevice)
        {
            await MeasuringDeviceRepository.CreateAsync(measuringDevice);
        }

        public async Task UpdateAsync(IMeasuringDevice measuringDevice)
        {
            await MeasuringDeviceRepository.UpdateAsync(measuringDevice);
        }
        
        public async Task DeleteAsync(Guid ID)
        {
            await MeasuringDeviceRepository.DeleteAsync(ID);
        }

        #endregion

        #region Assign
        public async Task AssignMeasuringDeviceAsync(Guid measuringDeviceID, Guid? employeeID)
        {
            await MeasuringDeviceRepository.AssignOneMeasuringDeviceAsync(measuringDeviceID, employeeID);
        }
        #endregion
    }
}
