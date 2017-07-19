using Common.Parameters;
using Model.Common;
using PagedList;
using Repository.Common;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository EmployeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.EmployeeRepository = employeeRepository;
        }

        #region Get
        public async Task<List<IEmployee>> GetAllAsync(IEmployeeParameters employeeParameters = null)
        {
            return new List<IEmployee>(await EmployeeRepository.GetAllAsync(employeeParameters));
        }

        public async Task<StaticPagedList<IEmployee>> GetAllPagedListAsync(IEmployeeParameters employeeParameters)
        {
            var count = await EmployeeRepository.GetCountAsync(employeeParameters);
            var employeeList = await EmployeeRepository.GetAllAsync(employeeParameters);
            var employeePagedList = new StaticPagedList<IEmployee>(employeeList, employeeParameters.PageNumber.Value, employeeParameters.PageSize.Value, count);

            return employeePagedList;
        }

        public async Task<IEmployee> GetByIdAsync(Guid? ID)
        {
            return await EmployeeRepository.GetByIdAsync(ID);
        }
        public async Task<IEmployee> GetOneAsync(IEmployeeParameters employeeParameters)
        {
            return await EmployeeRepository.GetOneAsync(employeeParameters);
        }
        #endregion

        #region Basic CRUD
        public async Task CreateAsync(IEmployee employee)
        {
            await EmployeeRepository.CreateAsync(employee);
        }

        public async Task UpdateAsync(IEmployee employee)
        {
            await EmployeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteAsync(Guid ID)
        {
            await EmployeeRepository.DeleteAsync(ID);
        }

        #endregion

        #region Return Methods      
        public async Task ReturnOneItemAsync(Guid? ID)
        {
            await EmployeeRepository.ReturnOneItemAsync(ID);
        }

        public async Task ReturnAllItemsAsync(Guid? ID)
        {
            await EmployeeRepository.ReturnAllItemsAsync(ID);
        }

        public async Task ReturnOneMeasuringDeviceAsync(Guid? ID)
        {
            await EmployeeRepository.ReturnOneMeasuringDeviceAsync(ID);
        }

        public async Task ReturnAllMeasuringDevicesAsync(Guid? ID)
        {
            await EmployeeRepository.ReturnAllMeasuringDevicesAsync(ID);
        }

        public async Task ReturnOneVehicleAsync(Guid? ID)
        {
            await EmployeeRepository.ReturnOneVehicleAsync(ID);
        }

        public async Task ReturnAllVehiclesAsync(Guid? ID)
        {
            await EmployeeRepository.ReturnAllVehiclesAsync(ID);
        }

        public async Task ReturnAllInventory(Guid? ID)
        {
            await EmployeeRepository.ReturnAllInventory(ID);
        }
        #endregion
    }
}
