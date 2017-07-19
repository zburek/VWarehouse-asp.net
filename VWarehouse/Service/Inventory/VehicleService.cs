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
    public class VehicleService : IVehicleService
    {
        private IVehicleRepository VehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            this.VehicleRepository = vehicleRepository;
        }

        #region Get
        public async Task<List<IVehicle>> GetAllAsync(IVehicleParameters vehicleParameters)
        {
            return new List<IVehicle>(await VehicleRepository.GetAllAsync(vehicleParameters));
        }

        public async Task<StaticPagedList<IVehicle>> GetAllPagedListAsync(IVehicleParameters vehicleParameters)
        {
            var count = await VehicleRepository.GetCountAsync(vehicleParameters);
            var vehicleList = await VehicleRepository.GetAllAsync(vehicleParameters);
            var vehiclePagedList = new StaticPagedList<IVehicle>(vehicleList, vehicleParameters.PageNumber.Value, vehicleParameters.PageSize.Value, count);

            return vehiclePagedList;
        }
        public async Task<IVehicle> GetByIdAsync(Guid? ID)
        {
            return await VehicleRepository.GetByIdAsync(ID);
        }
        #endregion

        #region CRUD
        public async Task CreateAsync(IVehicle vehicle)
        {
            await VehicleRepository.CreateAsync(vehicle);
        }

        public async Task UpdateAsync(IVehicle vehicle)
        {
            await VehicleRepository.UpdateAsync(vehicle);
        }

        public async Task DeleteAsync(Guid ID)
        {
            await VehicleRepository.DeleteAsync(ID);
        }
        #endregion

        #region Assign
        public async Task AssignVehicleAsync(Guid vehicleID, Guid? employeeID)
        {
            await VehicleRepository.AssignOneVehicleAsync(vehicleID, employeeID);
        }
        #endregion
    }
}
