using Repository.Common.Inventory;
using System;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IItemRepository Items { get; }
        IMeasuringDeviceRepository MeasuringDevices { get; }
        IVehicleRepository Vehicles { get; }
        Task SaveAsync();
    }
}
