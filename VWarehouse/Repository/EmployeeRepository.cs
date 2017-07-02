using DAL;
using DAL.DbEntities;
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