using AutoMapper;
using Model.Common.Inventory;
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

        public async Task CreateAsync(IVehicle vehicle)
        {
            VehicleEntity vehicleEntity = Mapper.Map<VehicleEntity>(vehicle);
            await unitOfWork.Vehicles.AddAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(IVehicle vehicle)
        {
            VehicleEntity vehicleEntity = Mapper.Map<VehicleEntity>(vehicle);
            await unitOfWork.Vehicles.UpdateAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int ID)
        {
            VehicleEntity vehicleEntity = Mapper.Map<VehicleEntity>(await unitOfWork.Vehicles.GetByIdAsync(ID));
            await unitOfWork.Vehicles.DeleteAsync(vehicleEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task ReturnItemAsync(int ID)
        {
            VehicleEntity vehicleEntity = Mapper.Map<VehicleEntity>(await unitOfWork.Vehicles.GetByIdAsync(ID));
            vehicleEntity.EmployeeID = null;
            await unitOfWork.Vehicles.UpdateAsync(vehicleEntity);
        }

    }
}
