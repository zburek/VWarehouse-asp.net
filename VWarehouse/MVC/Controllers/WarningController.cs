using AutoMapper;
using Model.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Web.Mvc;
using Service.Common.Inventory;
using DAL.DbEntities.Inventory;
using Common;
using MVC.Models.WarningViewModels;

namespace MVC.Controllers
{
    public class WarningController : Controller
    {
        protected IMeasuringDeviceService MeasuringDeviceService;
        protected IVehicleService VehicleService;
        public IWarningViewModel Warning;
        protected IParameters<MeasuringDeviceEntity> measuringDeviceParameters;
        protected IParameters<VehicleEntity> vehicleParameters;

        public WarningController(
            IMeasuringDeviceService measuringDeviceService, IVehicleService vehicleService, WarningViewModel warning, 
            IParameters<MeasuringDeviceEntity> measuringDeviceParameters, IParameters<VehicleEntity> vehicleParameters)
        {
            this.MeasuringDeviceService = measuringDeviceService;
            this.VehicleService = vehicleService;
            this.Warning = warning;
            this.measuringDeviceParameters = measuringDeviceParameters;
            this.vehicleParameters = vehicleParameters;
        }
        public async Task<ActionResult> Index()
        {
            int daysDifference = 21;
            int mileageDifference = 500;

            measuringDeviceParameters.Filter = MD => DbFunctions.DiffDays(DateTime.Today, MD.CalibrationExpirationDate) < daysDifference;
            Warning.MeasuringDeviceList = Mapper.Map<List<IMeasuringDevice>>(await MeasuringDeviceService.GetAllAsync(measuringDeviceParameters));

            vehicleParameters.Filter = VLP => DbFunctions.DiffDays(DateTime.Today, VLP.LicenseExpirationDate) < daysDifference;
            Warning.VehicleLicensePlateList = Mapper.Map<List<IVehicle>>(await VehicleService.GetAllAsync(vehicleParameters));

            vehicleParameters.Filter = VM => (VM.NextService - VM.Mileage) < mileageDifference;
            Warning.VehicleMileageList = Mapper.Map<List<IVehicle>>(await VehicleService.GetAllAsync(vehicleParameters));

            return View(Warning);
        }

    }
}
