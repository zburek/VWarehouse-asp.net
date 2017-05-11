using System;

namespace Model.Common.DbEntities.Inventory
{
    public interface IVehicleEntity : IBaseEntity
    {
        string Type { get; set; }
        string LicensePlate { get; set; }
        DateTime LicenseExpirationDate { get; set; }
        int Mileage { get; set; }
        int NextService { get; set; }
        int? EmployeeID { get; set; }
    }
}
