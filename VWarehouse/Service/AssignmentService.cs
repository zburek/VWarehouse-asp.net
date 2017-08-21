using Common;
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
    public class AssignmentService : IAssignmentService
    {
        private IAssignmentRepository AssignmentRepository;
        public AssignmentService(IAssignmentRepository assignmentRepository)
        {
            this.AssignmentRepository = assignmentRepository;
        }

        #region Get
        public async Task<List<IAssignment>> GetAllAsync(IAssignmentParameters assignmentParameters = null)
        {
            return new List<IAssignment>(await AssignmentRepository.GetAllAsync(assignmentParameters));
        }

        public async Task<StaticPagedList<IAssignment>> GetAllPagedListAsync(IAssignmentParameters assignmentParameters)
        {
            var count = await AssignmentRepository.GetCountAsync(assignmentParameters);
            var assignmentList = await AssignmentRepository.GetAllAsync(assignmentParameters);
            var assignmentPagedList = new StaticPagedList<IAssignment>(assignmentList, assignmentParameters.PageNumber.Value, assignmentParameters.PageSize.Value, count);

            return assignmentPagedList;
        }

        public async Task<IAssignment> GetByIdAsync(Guid? ID)
        {
            return await AssignmentRepository.GetByIdAsync(ID);
        }
        public async Task<IAssignment> GetOneAsync(IAssignmentParameters assignmentParameters)
        {
            return await AssignmentRepository.GetOneAsync(assignmentParameters);
        }


        #endregion

        #region Basic CRUD
        public async Task CreateAsync(IAssignment assignment)
        {
            await AssignmentRepository.CreateAsync(assignment);
        }

        public async Task UpdateAsync(IAssignment assignment)
        {
            await AssignmentRepository.UpdateAsync(assignment);
        }

        public async Task DeleteAsync(Guid ID)
        {
            await AssignmentRepository.DeleteAsync(ID);
        }

        #endregion

        #region Return Methods    

        public async Task ReturnOneEmployeeAsync(Guid? empID, Guid? assignmentID)
        {
            await AssignmentRepository.ReturnOneEmployeeAsync(empID, assignmentID);
        }

        public async Task ReturnAllEmployeesAsync(Guid? ID)
        {
            await AssignmentRepository.ReturnAllEmployeesAsync(ID);
        }

        public async Task ReturnOneItemAsync(Guid? empID, Guid? assignmentID)
        {
            await AssignmentRepository.ReturnOneItemAsync(empID, assignmentID);
        }

        public async Task ReturnAllItemsAsync(Guid? ID)
        {
            await AssignmentRepository.ReturnAllItemsAsync(ID);
        }

        public async Task ReturnOneMeasuringDeviceAsync(Guid? empID, Guid? assignmentID)
        {
            await AssignmentRepository.ReturnOneMeasuringDeviceAsync(empID, assignmentID);
        }

        public async Task ReturnAllMeasuringDevicesAsync(Guid? ID)
        {
            await AssignmentRepository.ReturnAllMeasuringDevicesAsync(ID);
        }

        public async Task ReturnOneVehicleAsync(Guid? empID, Guid? assignmentID)
        {
            await AssignmentRepository.ReturnOneVehicleAsync(empID, assignmentID);
        }

        public async Task ReturnAllVehiclesAsync(Guid? ID)
        {
            await AssignmentRepository.ReturnAllVehiclesAsync(ID);
        }

        public async Task ReturnAll(Guid? ID)
        {
            await AssignmentRepository.ReturnAll(ID);
        }
        #endregion    

        #region Assign
        public async Task<IEnumerable<IBaseEntity>> GetAllEmployeesAsync(IAssignmentParameters assignmentParameters = null)
        {
            return await AssignmentRepository.GetEmployeesAsync(assignmentParameters);
        }

        public async Task AssignAsync(IAssignmentParameters assignmentParameters = null)
        {
            await AssignmentRepository.AssignAsync(assignmentParameters);
        }

        #endregion
    }
}
