
using DAL;
using Repository.Common;
using Repository.Common.Inventory;
using Repository.Inventory;
using System.Data.Entity.Validation;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VWarehouseContext _context;
        public IEmployeeRepository Employees { get; private set; }
        public IItemRepository Items { get; private set; }
        public IMeasuringDeviceRepository MeasuringDevices { get; private set; }
        public IVehicleRepository Vehicles { get; private set; }

        public UnitOfWork(VWarehouseContext context)
        {
            this._context = context;
            Employees = new EmployeeRepository(_context);
            Items = new ItemRepository(_context);
            MeasuringDevices = new MeasuringDeviceRepository(_context);
            Vehicles = new VehicleRepository(_context);
        }

        public virtual async Task SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                ThrowEnhancedValidationException(e);
            }
        }
        protected virtual void ThrowEnhancedValidationException(DbEntityValidationException e)
        {
            var errorMessages = e.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

            var fullErrorMessage = string.Join("; ", errorMessages);
            var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
            throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
