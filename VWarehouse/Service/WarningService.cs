using Model.Common.Inventory;
using Repository.Common.Inventory;
using Service.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class WarningService : IWarningService
    {
        // CREATES TOO MANY REPOSITORIES
        private IMeasuringDeviceRepository MeasuringDeviceRepository;
        private IVehicleRepository VehicleRepository;
        public WarningService(IMeasuringDeviceRepository measuringDeviceRepository, IVehicleRepository vehicleRepository)
        {
            this.MeasuringDeviceRepository = measuringDeviceRepository;
            this.VehicleRepository = vehicleRepository;
        }
        public async Task<List<IMeasuringDevice>> GetMeasuringDeviceCalibraionDateWarning(int daysDifference)
        {
            return new List<IMeasuringDevice>(await MeasuringDeviceRepository.GetMeasuringDeviceCalibraionDateWarning(daysDifference));
        }

        public async Task<List<IVehicle>> GetVehicleLicenseDateWarning(int daysDifference)
        {
            return new List<IVehicle>(await VehicleRepository.GetVehicleLicenseDateWarning(daysDifference));
        }

        public async Task<List<IVehicle>> GetVehicleMileageWarning(int mileageDifference)
        {
            return new List<IVehicle>(await VehicleRepository.GetVehicleMileageWarning(mileageDifference));
        }
    }
}
