﻿using Model.Common.Inventory;
using System.Collections.Generic;

namespace MVC.Models.ViewModels
{
    public interface IWarningViewModel
    {
        List<IMeasuringDevice> MeasuringDeviceList { get; set; }
        List<IVehicle> VehicleLicensePlateList { get; set; }
        List<IVehicle> VehicleMileageList { get; set; }
    }
}