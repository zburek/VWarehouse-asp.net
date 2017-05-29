
using Model.Common;
using Model.Common.Inventory;
using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Inventory
{
    public class MeasuringDevice : IMeasuringDevice
    {
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CalibrationExpirationDate { get; set; }
        public int? EmployeeID { get; set; }
        public virtual IEmployee Employee { get; set; }
    }
}
