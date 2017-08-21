using Common;
using Common.Parameters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IUnitOfWorkAssignment : IDisposable
    {
        Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;
        Task<int> DeleteAsync<TEntity>(Guid ID) where TEntity : class, IBaseEntity;
        Task ReturnAllItemsAsync(Guid? ID);
        Task ReturnAllEmployeesAsync(Guid? ID);
        Task ReturnAllMeasuringDevicesAsync(Guid? ID);
        Task ReturnAllVehiclesAsync(Guid? ID);
        Task ReturnAll(Guid? ID);
        Task<IEnumerable<IBaseEntity>> GetAllEmployeesAsync(IAssignmentParameters assignmentParameters);
        Task SaveAsync();
        Task AssignAsync(IAssignmentParameters assignmentParameters);
    }
}
