using Common;
using Common.Parameters;
using DAL;
using DAL.DbEntities;
using DAL.DbEntities.Inventory;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    internal class UnitOfWorkAssignment : IUnitOfWorkAssignment
    {
        protected IVWarehouseContext Context { get; private set; }
        public UnitOfWorkAssignment(IVWarehouseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("Context is null");
            }
            this.Context = context;
        }

        #region Return
        public async Task ReturnAllEmployeesAsync(Guid? assignmentID)
        {
            var assignmentEntity = await Context.Set<AssignmentEntity>().Include("Employees").FirstOrDefaultAsync(assignment => assignment.ID == assignmentID);
            foreach(var employee in assignmentEntity.Employees)
            {
                var employeeEntity = await Context.Set<EmployeeEntity>().FindAsync(employee.ID);
                assignmentEntity.Employees.Remove(employeeEntity);
                await UpdateAsync(assignmentEntity);
            }
        }

        public async Task ReturnAllItemsAsync(Guid? assignmentID)
        {
            var assignmentEntity = await Context.Set<AssignmentEntity>().Include("Items").FirstOrDefaultAsync(assignment => assignment.ID == assignmentID);
            foreach (var item in assignmentEntity.Items)
            {
                var itemEntity = await Context.Set<ItemEntity>().FindAsync(item.ID);
                assignmentEntity.Items.Remove(itemEntity);
                await UpdateAsync(assignmentEntity);
            }
        }

        public async Task ReturnAllMeasuringDevicesAsync(Guid? assignmentID)
        {
            var assignmentEntity = await Context.Set<AssignmentEntity>().Include("MeasuringDevices").FirstOrDefaultAsync(assignment => assignment.ID == assignmentID);
            foreach (var md in assignmentEntity.MeasuringDevices)
            {
                var mdEntity = await Context.Set<MeasuringDeviceEntity>().FindAsync(md.ID);
                assignmentEntity.MeasuringDevices.Remove(mdEntity);
                await UpdateAsync(assignmentEntity);
            }
        }

        public async Task ReturnAllVehiclesAsync(Guid? assignmentID)
        {
            var assignmentEntity = await Context.Set<AssignmentEntity>().Include("Vehicles").FirstOrDefaultAsync(assignment => assignment.ID == assignmentID);
            foreach (var vehicle in assignmentEntity.Vehicles)
            {
                var vehicleEntity = await Context.Set<VehicleEntity>().FindAsync(vehicle.ID);
                assignmentEntity.Vehicles.Remove(vehicleEntity);
                await UpdateAsync(assignmentEntity);
            }
        }
        public async Task ReturnAll(Guid? assignmentID)
        {
            await ReturnAllEmployeesAsync(assignmentID);
            await ReturnAllItemsAsync(assignmentID);
            await ReturnAllMeasuringDevicesAsync(assignmentID);
            await ReturnAllVehiclesAsync(assignmentID);
        }
        #endregion

        #region GetForAssign and Assign
        public async Task<IEnumerable<IBaseEntity>> GetAllEmployeesAsync(IAssignmentParameters assignmentParameters)
        {
            var query = from employee in Context.Set<EmployeeEntity>().Include("Assignments")
                        where (employee.Assignments.All(e => e.StartTime > assignmentParameters.EndTime || e.EndTime < assignmentParameters.StartTime || e.EndTime == null)) || (employee.Assignments.Count == 0)
                        select employee;

            return await query.ToListAsync();
        }

        public async Task AssignAsync(IAssignmentParameters assignmentParameters)
        {
            var assignmentEntity = await Context.Set<AssignmentEntity>().Include("Employees").FirstOrDefaultAsync(assignment => assignment.ID == assignmentParameters.ID);
            foreach (var employee in assignmentParameters.EmployeeList)
            {
                var employeeEntity = await Context.Set<EmployeeEntity>().FindAsync(employee.ID);
                assignmentEntity.Employees.Add(employeeEntity);
                await UpdateAsync(assignmentEntity);
            }
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
            await ReturnAll(ID);
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