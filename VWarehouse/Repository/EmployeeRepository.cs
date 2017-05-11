using DAL;
using Model.DbEntities;
using Repository.Common;

namespace Repository
{
    public class EmployeeRepository : GenericRepository<EmployeeEntity>, IEmployeeRepository
    {
        public EmployeeRepository(VWarehouseContext context)
            : base(context)
        {
        }

        /// <summary>
        /// In first version this approach was used https://cpratt.co/truly-generic-repository/
        /// so there was no need for additional IRepositories except IGenericRepository
        /// and Unit Of Work was not used
        /// </summary>

    }
}
