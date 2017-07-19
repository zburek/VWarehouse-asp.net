using AutoMapper;
using Model.Common.Inventory;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVC.Models.WarningViewModels;
using Service.Common;

namespace MVC.Controllers
{
    public class WarningController : Controller
    {
        protected IWarningService WarningService;
        public IWarningViewModel Warning;

        public WarningController(IWarningService warningService, WarningViewModel warning)
        {
            this.WarningService = warningService;
            this.Warning = warning;
        }
        public async Task<ActionResult> Index()
        {
            int daysDifference = 21;
            int mileageDifference = 500;

            Warning.MeasuringDeviceList = Mapper.Map<List<IMeasuringDevice>>(await WarningService.GetMeasuringDeviceCalibraionDateWarning(daysDifference));
            Warning.VehicleLicensePlateList = Mapper.Map<List<IVehicle>>(await WarningService.GetVehicleLicenseDateWarning(daysDifference));
            Warning.VehicleMileageList = Mapper.Map<List<IVehicle>>(await WarningService.GetVehicleMileageWarning(mileageDifference));

            return View(Warning);
        }
    }
}
