using DAL;
using DAL.DbEntities.Inventory;
using Repository.Common.Inventory;

namespace Repository.Inventory
{
    public class MeasuringDeviceRepository : GenericRepository<MeasuringDeviceEntity>, IMeasuringDeviceRepository
    {
        public MeasuringDeviceRepository(VWarehouseContext context)
            : base(context)
        {
        }
    }
}

