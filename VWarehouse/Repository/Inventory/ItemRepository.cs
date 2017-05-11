using DAL;
using Model.DbEntities.Inventory;
using Repository.Common.Inventory;

namespace Repository.Inventory
{
    public class ItemRepository : GenericRepository<ItemEntity>, IItemRepository
    {
        public ItemRepository(VWarehouseContext context)
            : base(context)
        {
        }

        /// <summary>
        /// In first version this approach was used https://cpratt.co/truly-generic-repository/
        /// so there was no need for additional IRepositories except IGenericRepository
        /// and Unit Of Work was not used
        /// </summary
    }
}