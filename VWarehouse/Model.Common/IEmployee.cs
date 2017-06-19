using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Common
{
    public interface IEmployee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        Guid ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        string Name { get; set; }
        string PhoneNumber { get; set; }
        ICollection<IBaseInfo> Items { get; set; }
        ICollection<IBaseInfo> MeasuringDevices { get; set; }
        ICollection<IBaseInfo> Vehicles { get; set; }
    }
}
