using Common;
using Common.Parameters;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Repository.Common
{
    public interface IAssignmentRepository
    {
        Task<IAssignment> GetByIdAsync(Guid? assignmentID);
        Task<IEnumerable<IAssignment>> GetAllAsync(IAssignmentParameters parameters);      
        Task<IAssignment> GetOneAsync(IAssignmentParameters parameters);     
        Task<int> GetCountAsync(IAssignmentParameters parameters);       
        Task CreateAsync(IAssignment assignment);
        Task UpdateAsync(IAssignment assignment);
        Task DeleteAsync(Guid assignmentID);
        Task ReturnOneEmployeeAsync(Guid? empID, Guid? assignmentID);
        Task ReturnAllEmployeesAsync(Guid? assignmentID);
        Task ReturnOneItemAsync(Guid? itemID, Guid? assignmentID);
        Task ReturnAllItemsAsync(Guid? assignmentID);
        Task ReturnOneMeasuringDeviceAsync(Guid? mdID, Guid? assignmentID);
        Task ReturnAllMeasuringDevicesAsync(Guid? assignmentID);
        Task ReturnOneVehicleAsync(Guid? vehicleID, Guid? assignmentID);
        Task ReturnAllVehiclesAsync(Guid? assignmentID);
        Task ReturnAll(Guid? assignmentID);
        Task<IEnumerable<IBaseEntity>> GetEmployeesAsync(IAssignmentParameters assignmentParameters);
    }
}