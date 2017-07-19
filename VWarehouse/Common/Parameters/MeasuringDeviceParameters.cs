using System;

namespace Common.Parameters
{
    public interface IMeasuringDeviceParameters : IParameters
    {
        Guid? EmployeeID { get; set; }
        bool OnStock { get; set; }
    }

    public class MeasuringDeviceParameters : Parameters, IMeasuringDeviceParameters
    {
        public Guid? EmployeeID { get; set; }
        public bool OnStock { get; set; }
    }
}
