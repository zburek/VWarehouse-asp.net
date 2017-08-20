using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Common
{
    public interface IAssignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        Guid ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        string Name { get; set; }
        string Materials { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        DateTime? StartTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        DateTime? EndTime { get; set; }
        string Note { get; set; }
        ICollection<IBaseEntity> Employees { get; set; }
        ICollection<IBaseEntity> Items { get; set; }
        ICollection<IBaseEntity> MeasuringDevices { get; set; }
        ICollection<IBaseEntity> Vehicles { get; set; }
    }
}