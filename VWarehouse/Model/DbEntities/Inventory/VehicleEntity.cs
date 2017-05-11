using Model.Common.DbEntities;
using Model.Common.DbEntities.Inventory;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DbEntities.Inventory
{
    [Table("Vehicle")]
    public class VehicleEntity : BaseEntity, IVehicleEntity, IBaseEntity
    {
        public string Type { get; set; }
        public string LicensePlate { get; set; }
        public DateTime LicenseExpirationDate { get; set; }
        public int Mileage { get; set; }
        public int NextService { get; set; }
        [ForeignKey("EmployeeEntity")]
        public int? EmployeeID { get; set; }
        public virtual EmployeeEntity EmployeeEntity { get; set; }
    }
}
