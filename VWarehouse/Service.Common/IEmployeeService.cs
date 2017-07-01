using Common;
using DAL.DbEntities;
using Model.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Common
{
    public interface IEmployeeService
    {  
        Task<List<IEmployee>> GetAllAsync(IParameters<EmployeeEntity> parameters = null);
        Task<StaticPagedList<IEmployee>> GetAllPagedListAsync(IParameters<EmployeeEntity> parameters);
        Task<IEmployee> GetOneAsync(IParameters<EmployeeEntity> parameters);
        Task<IEmployee> GetByIdAsync(Guid? ID);
        Task CreateAsync(IEmployee employee);
        Task UpdateAsync(IEmployee employee);  
        Task DeleteAsync(Guid ID);
    }
}
