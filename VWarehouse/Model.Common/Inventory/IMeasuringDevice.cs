using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Common.Inventory
{
    public interface IMeasuringDevice
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        Guid ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        string Name { get; set; }
        string SerialNumber { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString= "{0:dd.MM.yyyy}", ApplyFormatInEditMode= true)]
        DateTime CalibrationExpirationDate { get; set; }
        Guid? EmployeeID { get; set; }
        IEmployee Employee { get; set; }

    }
}
