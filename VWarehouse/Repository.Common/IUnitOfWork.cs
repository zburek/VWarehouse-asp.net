using Repository.Common.Inventory;
using System;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }
        IItemRepository Items { get; }
        IMeasuringDeviceRepository MeasuringDevices { get; }
        IVehicleRepository Vehicles { get; }
    }
}
