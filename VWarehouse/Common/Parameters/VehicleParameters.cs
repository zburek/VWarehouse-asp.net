using System;

namespace Common.Parameters
{
    public interface IVehicleParameters : IParameters
    {
        Guid? EmployeeID { get; set; }
        bool OnStock { get; set; }
    }

    public class VehicleParameters : Parameters, IVehicleParameters
    {
        public Guid? EmployeeID { get; set; }
        public bool OnStock { get; set; }
    }
}
