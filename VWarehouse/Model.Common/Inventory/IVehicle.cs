using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Common.Inventory
{
    public interface IVehicle
    {
        int ID { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        string LicensePlate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        DateTime LicenseExpirationDate { get; set; }
        int Mileage { get; set; }
        int NextService { get; set; }
        int? EmployeeID { get; set; }
        IEmployee Employee { get; set; }

    }
}
