using Model.Common.Inventory;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Inventory
{
    public class Vehicle : IVehicle
    {
        [Required]
        public int ID { get; set; }
        public string Type { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public string LicensePlate { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public int Mileage { get; set; }
        public int NextService { get; set; }
        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
