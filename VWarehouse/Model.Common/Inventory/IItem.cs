
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Common.Inventory
{
    public interface IItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        Guid ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        string Name { get; set; }
        [MaxLength(60)]
        string Description { get; set; }
        string SerialNumber { get; set; }
        Guid? EmployeeID { get; set; }
        IEmployee Employee { get; set; }
    }
}
