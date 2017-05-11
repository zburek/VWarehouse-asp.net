
using System;

namespace Model.Common.DbEntities.Inventory
{
    public interface IMeasuringDeviceEntity : IBaseEntity
    {
        string SerialNumber { get; set; }
        DateTime CalibrationExpirationDate { get; set; }
        int? EmployeeID { get; set; }
    }
}
