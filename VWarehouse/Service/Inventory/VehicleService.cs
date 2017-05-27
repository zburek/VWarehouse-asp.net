using AutoMapper;
using Model.Common;
using Model.Common.Inventory;
using Model.Common.ViewModels;
using Model.DbEntities.Inventory;
using Repository;
using Repository.Common;
using Service.Common.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Inventory
{
    public class VehicleService : IVehicleService
    {
        private IUnitOfWork unitOfWork;
        public VehicleService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Get
        public async Task<List<IVehicle>> GetAllAsync(
            Expression<Func<VehicleEntity, bool>> filter = null,
            Func<IQueryable<VehicleEntity>, IOrderedQueryable<VehicleEntity>> orderBy = null,
            string includeProperties = null)
        {
            return new List<IVehicle>
                (Mapper.Map<List<IVehicle>>
                (await unitOfWork.Vehicles.GetAllAsync(filter, orderBy, includeProperties)));
        }

        public async Task<IVehicle> GetByIdAsync(int? ID)
        {
            var vehicle = Mapper.Map<IVehicle>
                (await unitOfWork.Vehicles.GetByIdAsync(ID));
            return vehicle;
        }
        public async Task<IAssignViewModel> CreateAssignViewModelAsync(int? ID)
        {
            var vehicle = Mapper.Map<IAssignViewModel>(await unitOfWork.Vehicles.GetByIdAsync(ID));
            vehicle.EmployeeList = Mapper.Map<List<IEmployee>>(await unitOfWork.Employees.GetAllAsync(null, null, null));
            return vehicle;
        }

        #endregion

        #region CRUD
        public async Task CreateAsync(IVehicle vehicle)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(vehicle);
            await unitOfWork.Vehicles.AddAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(IVehicle vehicle)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(vehicle);
            await unitOfWork.Vehicles.UpdateAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int ID)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(await unitOfWork.Vehicles.GetByIdAsync(ID));
            await unitOfWork.Vehicles.DeleteAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        #endregion

        #region Assign and Return
        public async Task AssignVehicleAsync(IAssignViewModel vehicle)
        {
            var vehicleEntity = await unitOfWork.Vehicles.GetByIdAsync(vehicle.ID);
            vehicleEntity.EmployeeID = vehicle.EmployeeID;
            await unitOfWork.Vehicles.UpdateAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }
        public async Task ReturnOneVehicleAsync(int? ID)
        {
            var vehicleEntity = Mapper.Map<VehicleEntity>(await unitOfWork.Vehicles.GetByIdAsync(ID));
            vehicleEntity.EmployeeID = null;
            await unitOfWork.Vehicles.UpdateAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnAllVehiclesAsync(int? ID)
        {
            Expression<Func<VehicleEntity, bool>> filter = i => i.EmployeeID == ID;
            IEnumerable<VehicleEntity> vehicleList = await unitOfWork.Vehicles.GetAllAsync(filter, null, null);
            foreach (var vehicleEntity in vehicleList)
            {
                vehicleEntity.EmployeeID = null;
                await unitOfWork.Vehicles.UpdateAsync(vehicleEntity);
            }
            await unitOfWork.SaveAsync();
        }

        #endregion
    }
}
