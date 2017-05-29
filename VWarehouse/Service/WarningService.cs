using AutoMapper;
using Model.Common.Inventory;
using Repository;
using Repository.Common;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model.Common.ViewModels;
using System.Linq.Expressions;
using Model.DbEntities.Inventory;
using System.Data.Entity;
using Model.ViewModels;

namespace Service
{
    public class WarningService : IWarningService
    {
        private IUnitOfWork unitOfWork;
        public IWarningViewModel warning;
        public WarningService(UnitOfWork unitOfWork, WarningViewModel warning)
        {
            this.unitOfWork = unitOfWork;
            this.warning = warning;
        }

        public async Task<IWarningViewModel> CreateWarningViewModel()
        {

            int daysDifference = 21;
            int mileageDifference = 500;

            Expression<Func<MeasuringDeviceEntity, bool>> filterMD = MD => DbFunctions.DiffDays(DateTime.Today, MD.CalibrationExpirationDate) < daysDifference;
            Expression<Func<VehicleEntity, bool>> filterVLP = VLP => DbFunctions.DiffDays(DateTime.Today, VLP.LicenseExpirationDate) < daysDifference;
            Expression<Func<VehicleEntity, bool>> filterVM = VM => (VM.NextService - VM.Mileage) < mileageDifference;

            warning.MeasuringDeviceList = Mapper.Map<List<IMeasuringDevice>>(await unitOfWork.MeasuringDevices.GetAllAsync(filterMD, null, null));
            warning.VehicleLicensePlateList = Mapper.Map<List<IVehicle>>(await unitOfWork.Vehicles.GetAllAsync(filterVLP, null, null));
            warning.VehicleMileageList = Mapper.Map<List<IVehicle>>(await unitOfWork.Vehicles.GetAllAsync(filterVM, null, null));

            return warning;
        }

    }
}
