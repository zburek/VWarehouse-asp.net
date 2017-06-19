using Model.Common.Inventory;
using System.Collections.Generic;

namespace MVC.Models.ViewModels
{
    public class WarningViewModel : IWarningViewModel
    {
        public List<IMeasuringDevice> MeasuringDeviceList { get; set; }
        public List<IVehicle> VehicleLicensePlateList { get; set; }
        public List<IVehicle> VehicleMileageList { get; set; }
    }
}
