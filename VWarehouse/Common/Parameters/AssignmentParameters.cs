using System;
using System.Collections.Generic;

namespace Common.Parameters
{
    public interface IAssignmentParameters : IParameters
    {
        DateTime? StartTime { get; set; }
        DateTime? EndTime { get; set; }
        List<IBaseEntity> EmployeeList { get; set; }
        List<IBaseEntity> ItemList { get; set; }
        List<IBaseEntity> MeasuringDeviceList { get; set; }
        List<IBaseEntity> VehicleList { get; set; }
    }

    public class AssignmentParameters : Parameters, IAssignmentParameters
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public List<IBaseEntity> EmployeeList { get; set; }
        public List<IBaseEntity> ItemList { get; set; }
        public List<IBaseEntity> MeasuringDeviceList { get; set; }
        public List<IBaseEntity> VehicleList { get; set; }
    }
}