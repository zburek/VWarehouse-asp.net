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
    }
}
