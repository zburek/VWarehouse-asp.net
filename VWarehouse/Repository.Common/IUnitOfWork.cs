using Common;
using System;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;
        Task<int> DeleteAsync<TEntity>(Guid ID) where TEntity : class, IBaseEntity;
        Task ReturnAllItemsAsync(Guid? ID);
        Task ReturnAllMeasuringDevicesAsync(Guid? ID);
        Task ReturnAllVehiclesAsync(Guid? ID);
        Task ReturnAllInventory(Guid? ID);       
        Task SaveAsync();
    }
}
