using Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DbEntities.Inventory
{
    [Table("MeasuringDevice")]
    public class MeasuringDeviceEntity : BaseEntity
    {
        public string SerialNumber { get; set; }
        public DateTime CalibrationExpirationDate { get; set; }
        [ForeignKey("Employee")]
        public Guid? EmployeeID { get; set; }
        public virtual EmployeeEntity Employee { get; set; }
    }
}
