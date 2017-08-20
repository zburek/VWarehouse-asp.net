using DAL;
using Repository.Common;

namespace Repository
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork CreateUnitOfWork(IVWarehouseContext context);
        IUnitOfWorkAssignment CreateUnitOfWorkAssignment(IVWarehouseContext context);
    }

    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork CreateUnitOfWork(IVWarehouseContext context)
        {
            return new UnitOfWork(context);
        }
        public IUnitOfWorkAssignment CreateUnitOfWorkAssignment(IVWarehouseContext context)
        {
            return new UnitOfWorkAssignment(context);
        }
    }
}
