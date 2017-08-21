using Common;
using Common.Parameters;
using Model.Common;
using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Common
{
    public interface IAssignmentService
    {
        Task<List<IAssignment>> GetAllAsync(IAssignmentParameters parameters = null);
        Task<StaticPagedList<IAssignment>> GetAllPagedListAsync(IAssignmentParameters parameters);
        Task<IAssignment> GetOneAsync(IAssignmentParameters parameters);
        Task<IAssignment> GetByIdAsync(Guid? ID);  
        Task CreateAsync(IAssignment employee);
        Task UpdateAsync(IAssignment employee);
        Task DeleteAsync(Guid ID);
        Task ReturnOneEmployeeAsync(Guid? empID, Guid? assignmentID);
        Task ReturnAllEmployeesAsync(Guid? ID);
        Task ReturnOneItemAsync(Guid? empID, Guid? assignmentID);
        Task ReturnAllItemsAsync(Guid? ID);
        Task ReturnOneMeasuringDeviceAsync(Guid? empID, Guid? assignmentID);
        Task ReturnAllMeasuringDevicesAsync(Guid? ID);
        Task ReturnOneVehicleAsync(Guid? empID, Guid? assignmentID);
        Task ReturnAllVehiclesAsync(Guid? ID);
        Task ReturnAll(Guid? ID);
        Task<IEnumerable<IBaseEntity>> GetAllEmployeesAsync(IAssignmentParameters assignmentParameters = null);
        Task AssignAsync(IAssignmentParameters assignmentParameters = null);
    }
}
