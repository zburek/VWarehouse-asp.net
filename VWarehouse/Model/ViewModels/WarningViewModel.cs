using Model.Common.Inventory;
using Model.Common.ViewModels;
using System.Collections.Generic;

namespace Model.ViewModels
{
    public class WarningViewModel : IWarningViewModel
    {
        public List<IMeasuringDevice> MeasuringDeviceList { get; set; }
        public List<IVehicle> VehicleLicensePlateList { get; set; }
        public List<IVehicle> VehicleMileageList { get; set; }
    }
}
