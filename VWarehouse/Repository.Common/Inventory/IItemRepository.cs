
using Model.DbEntities.Inventory;

namespace Repository.Common.Inventory
{
    public interface IItemRepository : IGenericRepository<ItemEntity>
    {
        /// <summary>
        /// In first version this approach was used https://cpratt.co/truly-generic-repository/
        /// so there was no need for additional IRepositories except IGenericRepository
        /// and Unit Of Work was not used
        /// </summary>
    }
}
