using DAL;
using DAL.DbEntities.Inventory;
using Repository.Common.Inventory;

namespace Repository.Inventory
{
    public class VehicleRepository : GenericRepository<VehicleEntity>, IVehicleRepository
    {
        public VehicleRepository(VWarehouseContext context)
            : base(context)
        {
        }
    }
}
