
using DAL;
using DAL.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IEmployeeRepository
    {
        Task<EmployeeEntity> GetByIdAsync(Guid? ID);
        Task<IEnumerable<EmployeeEntity>> GetAllAsync(IParameters<EmployeeEntity> parameters);
        Task<EmployeeEntity> GetOneAsync(IParameters<EmployeeEntity> parameters);
        Task<int> GetCountAsync(IParameters<EmployeeEntity> parameters);
        Task<int> AddAsync(EmployeeEntity entity);
        Task<int> UpdateAsync(EmployeeEntity entity);
        Task<int> DeleteAsync(Guid ID);
    }
}
