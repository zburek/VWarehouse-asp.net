
using Model.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IEmployeeRepository : IGenericRepository<EmployeeEntity>
    {
        Task<IEnumerable<EmployeeEntity>> GetAllPagedListAsync(
            Expression<Func<EmployeeEntity, bool>> filter = null,
            Func<IQueryable<EmployeeEntity>, IOrderedQueryable<EmployeeEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);
    }
}
