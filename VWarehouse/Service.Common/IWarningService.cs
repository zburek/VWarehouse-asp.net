using Model.Common.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Common
{
    public interface IWarningService
    {
        Task<List<IMeasuringDevice>> GetMeasuringDeviceCalibraionDateWarning(int daysDifference);
        Task<List<IVehicle>> GetVehicleLicenseDateWarning(int daysDifference);
        Task<List<IVehicle>> GetVehicleMileageWarning(int mileageDifference);
    }
}
