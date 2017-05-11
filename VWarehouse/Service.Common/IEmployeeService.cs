
using Model.Common;
using Model.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service.Common
{
    public interface IEmployeeService
    {
        Task<List<IEmployee>> GetAllAsync(
            Expression<Func<EmployeeEntity, bool>> filter = null, 
            Func<IQueryable<EmployeeEntity>, IOrderedQueryable<EmployeeEntity>> orderBy = null, 
            string includeProperties = null);
        Task<IEmployee> GetByIdAsync(int? ID);
        Task CreateAsync(IEmployee employee);
        Task UpdateAsync(IEmployee employee);  
        Task DeleteAsync(int ID);  
    }
}
