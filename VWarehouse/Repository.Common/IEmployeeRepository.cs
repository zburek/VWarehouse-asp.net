using Common;
using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Repository.Common
{
    public interface IEmployeeRepository
    {
        Task<EmployeeEntity> GetByIdAsync(Guid? ID);
        Task<IEnumerable<EmployeeEntity>> GetAllAsync(IParameters<EmployeeEntity> parameters);
        Task<EmployeeEntity> GetOneAsync(IParameters<EmployeeEntity> parameters);
        Task<int> GetCountAsync(IParameters<EmployeeEntity> parameters);
        Task CreateAsync(EmployeeEntity entity);
        Task UpdateAsync(EmployeeEntity entity);
        Task DeleteAsync(Guid ID);
    }
}
