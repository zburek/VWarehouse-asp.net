using DAL;
using Model.DbEntities;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : GenericRepository<EmployeeEntity>, IEmployeeRepository
    {
        public EmployeeRepository(VWarehouseContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<EmployeeEntity>> GetAllPagedListAsync(
            Expression<Func<EmployeeEntity, bool>> filter = null,
            Func<IQueryable<EmployeeEntity>, IOrderedQueryable<EmployeeEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }
        public Task<int> GetCountAsync(
            Expression<Func<EmployeeEntity, bool>> filter = null)
        {
            return GetQueryable(filter).CountAsync();
        }
    }
}
