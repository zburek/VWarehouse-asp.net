using Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DbEntities.Inventory
{
    [Table("Vehicle")]
    public class VehicleEntity : BaseEntity
    {
        public string Type { get; set; }
        public string LicensePlate { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public int Mileage { get; set; }
        public int NextService { get; set; }
        [ForeignKey("Employee")]
        public Guid? EmployeeID { get; set; }
        public virtual EmployeeEntity Employee { get; set; }
    }
}
