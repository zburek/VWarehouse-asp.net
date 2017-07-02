using DAL;
using DAL.DbEntities.Inventory;
using Repository.Common.Inventory;

namespace Repository.Inventory
{
    public class ItemRepository : GenericRepository<ItemEntity>, IItemRepository
    {
        public ItemRepository(VWarehouseContext context)
            : base(context)
        {
        }
    }
}