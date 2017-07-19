using DAL;
using Repository.Common;

namespace Repository
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork CreateUnitOfWork(IVWarehouseContext context);
    }

    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork CreateUnitOfWork(IVWarehouseContext context)
        {
            return new UnitOfWork(context);
        }
    }
}
