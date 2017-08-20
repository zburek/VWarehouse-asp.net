using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Common.Inventory
{
    public interface IVehicle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        Guid ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        string Name { get; set; }
        string Type { get; set; }
        string LicensePlate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        DateTime? LicenseExpirationDate { get; set; }
        int Mileage { get; set; }
        int NextService { get; set; }
        Guid? EmployeeID { get; set; }
        IEmployee Employee { get; set; }
        ICollection<IAssignment> Assignments { get; set; }
    }
}
