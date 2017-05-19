using AutoMapper;
using Model;
using Model.Common;
using Model.DbEntities;
using Model.DbEntities.Inventory;
using Repository;
using Repository.Common;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        private IUnitOfWork unitOfWork;
        public EmployeeService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Get
        public async Task<List<IEmployee>> GetAllAsync(
            Expression<Func<EmployeeEntity, bool>> filter = null, 
            Func <IQueryable<EmployeeEntity>, IOrderedQueryable<EmployeeEntity>> orderBy = null, 
            string includeProperties = null)
        {
            return new List<IEmployee>
                (Mapper.Map<List<IEmployee>>
                (await unitOfWork.Employees.GetAllAsync(filter, orderBy, includeProperties)));
        }
        
        public async Task<IEmployee> GetByIdAsync(int? ID)
        {
            var employee = Mapper.Map<IEmployee>
                (await unitOfWork.Employees.GetByIdAsync(ID));
            return employee;
        }
        public async Task<IEmployee> GetOneAsync(
            Expression<Func<EmployeeEntity, bool>> filter = null,
            string includeProperties = null)
        {
            var employee = Mapper.Map<IEmployee>
                (await unitOfWork.Employees.GetOneAsync(filter, includeProperties));
            return employee;
        }
        #endregion

        #region CRUD
        public async Task CreateAsync(IEmployee employee)
        {
            EmployeeEntity employeeEntity = Mapper.Map<EmployeeEntity>(employee);
            await unitOfWork.Employees.AddAsync(employeeEntity);
            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(IEmployee employee)
        {
            EmployeeEntity employeeEntity = Mapper.Map<EmployeeEntity>(employee);
            await unitOfWork.Employees.UpdateAsync(employeeEntity);
            await unitOfWork.SaveAsync();
        }
        
        public async Task DeleteAsync(int ID)
        {
            Expression<Func<ItemEntity, bool>> filter = i => i.EmployeeID == ID;
            /// Try shorten code by adding Expresion to GenericRepository Delete method
            /// Add code for additional tables in VWarehouse
            List<ItemEntity> employeeItems = (Mapper.Map<List<ItemEntity>>
                (await unitOfWork.Items.GetAllAsync(filter, null, null)));
            foreach(ItemEntity item in employeeItems)
            {
                await unitOfWork.Items.DeleteAsync(item);
            }

            EmployeeEntity employeeEnity = Mapper.Map<EmployeeEntity>(await unitOfWork.Employees.GetByIdAsync(ID));
            await unitOfWork.Employees.DeleteAsync(employeeEnity);
            await unitOfWork.SaveAsync();
        }
        #endregion
    }
}
