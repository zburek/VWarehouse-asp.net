using System;

namespace Model.Common.Inventory
{
    public interface IVehicle
    {
        int ID { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        string LicensePlate { get; set; }
        DateTime LicenseExpirationDate { get; set; }
        int Mileage { get; set; }
        int NextService { get; set; }
        int? EmployeeID { get; set; }
    }
}
