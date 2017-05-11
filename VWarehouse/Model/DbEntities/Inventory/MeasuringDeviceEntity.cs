using Model.Common.DbEntities;
using Model.Common.DbEntities.Inventory;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DbEntities.Inventory
{
    [Table("MeasuringDevice")]
    public class MeasuringDeviceEntity : BaseEntity, IMeasuringDeviceEntity, IBaseEntity
    {
        public string SerialNumber { get; set; }
        public DateTime CalibrationExpirationDate { get; set; }
        [ForeignKey("EmployeeEntity")]
        public int? EmployeeID { get; set; }
        public virtual EmployeeEntity EmployeeEntity { get; set; }
    }
}
