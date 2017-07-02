using DAL;
using DAL.DbEntities;
using DAL.DbEntities.Inventory;
using Repository.Common;
using Repository.Common.Inventory;
using Repository.Inventory;
using System;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VWarehouseContext Context;
        public IGenericRepository<EmployeeEntity> genericRepositoryEmployee { get; private set; }
        public IGenericRepository<ItemEntity> genericRepositoryItem { get; private set; }
        public IGenericRepository<MeasuringDeviceEntity> genericRepositoryMeasuringDevice { get; private set; }
        public IGenericRepository<VehicleEntity> genericRepositoryVehicle { get; private set; }
        public IEmployeeRepository Employees { get; private set; }
        public IItemRepository Items { get; private set; }
        public IMeasuringDeviceRepository MeasuringDevices { get; private set; }
        public IVehicleRepository Vehicles { get; private set; }

        public UnitOfWork(VWarehouseContext context)
        {
            this.Context = context;
            genericRepositoryEmployee = new GenericRepository<EmployeeEntity>(Context);
            genericRepositoryItem = new GenericRepository<ItemEntity>(Context);
            genericRepositoryMeasuringDevice = new GenericRepository<MeasuringDeviceEntity>(Context);
            genericRepositoryVehicle = new GenericRepository<VehicleEntity>(Context);
            Employees = new EmployeeRepository(genericRepositoryEmployee);
            Items = new ItemRepository(genericRepositoryItem);
            MeasuringDevices = new MeasuringDeviceRepository(genericRepositoryMeasuringDevice);
            Vehicles = new VehicleRepository(genericRepositoryVehicle);
        }

        public virtual async Task SaveAsync()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
