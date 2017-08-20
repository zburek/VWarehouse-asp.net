using Common;
using Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Assignment : IAssignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        public string Name { get; set; }
        public string Materials { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }
        public string Note { get; set; }
        public virtual ICollection<IBaseEntity> Employees { get; set; }
        public virtual ICollection<IBaseEntity> Items { get; set; }
        public virtual ICollection<IBaseEntity> MeasuringDevices { get; set; }
        public virtual ICollection<IBaseEntity> Vehicles { get; set; }
    }
}