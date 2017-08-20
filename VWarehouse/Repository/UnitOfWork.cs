using Common;
using DAL;
using DAL.DbEntities.Inventory;
using Repository.Common;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected IVWarehouseContext Context { get; private set; }
        public UnitOfWork(IVWarehouseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Context is null");
            }
            this.Context = context;
        }

        #region Return
        public async Task ReturnAllItemsAsync(Guid? ID)
        {
            IQueryable<ItemEntity> query = Context.Set<ItemEntity>().Where(i => i.EmployeeID == ID);
            foreach (var itemEntity in query)
            {
                itemEntity.EmployeeID = null;
                await UpdateAsync<ItemEntity>(itemEntity);
            }
        }

        public async Task ReturnAllMeasuringDevicesAsync(Guid? ID)
        {
            IQueryable<MeasuringDeviceEntity> query = Context.Set<MeasuringDeviceEntity>().Where(i => i.EmployeeID == ID);
            foreach (var measuringDeviceEntity in query)
            {
                measuringDeviceEntity.EmployeeID = null;
                await UpdateAsync<MeasuringDeviceEntity>(measuringDeviceEntity);
            }
        }

        public async Task ReturnAllVehiclesAsync(Guid? ID)
        {
            IQueryable<VehicleEntity> query = Context.Set<VehicleEntity>().Where(i => i.EmployeeID == ID);
            foreach (var vehicleEntity in query)
            {
                vehicleEntity.EmployeeID = null;
                await UpdateAsync<VehicleEntity>(vehicleEntity);
            }
        }
        public async Task ReturnAllInventory(Guid? ID)
        {
            await ReturnAllItemsAsync(ID);
            await ReturnAllMeasuringDevicesAsync(ID);
            await ReturnAllVehiclesAsync(ID);
        }
        #endregion

        #region CRUD
        public virtual async Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            try
            {
                var dbEntityEntry = Context.Entry(entity);
                if (dbEntityEntry.State == EntityState.Detached)
                {
                    Context.Set<TEntity>().Attach(entity);
                }
                dbEntityEntry.State = EntityState.Modified;
                return await Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual async Task<int> DeleteAsync<TEntity>(Guid ID) where TEntity : class, IBaseEntity
        {
            await ReturnAllInventory(ID);
            var entity = Context.Set<TEntity>().Find(ID);
            if (entity == null)
            {
                return await Task.FromResult(0);
            }

            try
            {
                var dbEntityEntry = Context.Entry(entity);
                if (dbEntityEntry.State != EntityState.Deleted)
                {
                    dbEntityEntry.State = EntityState.Deleted;
                }
                else
                {
                    Context.Set<TEntity>().Attach(entity);
                    Context.Set<TEntity>().Remove(entity);
                }
                return await Task.FromResult(1);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Save and Dispose
        public async Task SaveAsync()
        {
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Dispose()
        {
            Context.Dispose();
        }
        #endregion
    }
}
