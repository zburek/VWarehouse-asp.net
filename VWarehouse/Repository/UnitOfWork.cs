using Repository.Common;
using Repository.Common.Inventory;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmployeeRepository Employees { get; private set; }
        public IItemRepository Items { get; private set; }
        public IMeasuringDeviceRepository MeasuringDevices { get; private set; }
        public IVehicleRepository Vehicles { get; private set; }

        public UnitOfWork(IEmployeeRepository employeeRepository, IItemRepository itemRepository, IMeasuringDeviceRepository measuringDeviceRepository, IVehicleRepository vehicleRepository)
        {         
            Employees = employeeRepository;
            Items = itemRepository;
            MeasuringDevices = measuringDeviceRepository;
            Vehicles = vehicleRepository;
        }
    }
}
