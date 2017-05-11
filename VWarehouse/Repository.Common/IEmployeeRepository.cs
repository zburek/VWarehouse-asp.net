
using Model.DbEntities;

namespace Repository.Common
{
    public interface IEmployeeRepository : IGenericRepository<EmployeeEntity>
    {
        /// <summary>
        /// In first version this approach was used https://cpratt.co/truly-generic-repository/
        /// so there was no need for additional IRepositories except IGenericRepository
        /// and Unit Of Work was not used
        /// </summary>
    }
}
