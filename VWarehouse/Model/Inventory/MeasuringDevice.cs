
using Model.Common.Inventory;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Inventory
{
    public class MeasuringDevice : IMeasuringDevice
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public DateTime CalibrationExpirationDate { get; set; }
        public int? EmployeeID { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
