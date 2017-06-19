
using DAL;
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
        public IEmployeeRepository Employees { get; private set; }
        public IItemRepository Items { get; private set; }
        public IMeasuringDeviceRepository MeasuringDevices { get; private set; }
        public IVehicleRepository Vehicles { get; private set; }

        public UnitOfWork(VWarehouseContext context)
        {         
            this.Context = context;
            Employees = new EmployeeRepository(Context);
            Items = new ItemRepository(Context);
            MeasuringDevices = new MeasuringDeviceRepository(Context);
            Vehicles = new VehicleRepository(Context);
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
