using Common;
using Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Employee : IEmployee
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<IBaseEntity> Items { get; set; }
        public ICollection<IBaseEntity> MeasuringDevices { get; set; }
        public ICollection<IBaseEntity> Vehicles { get; set; }
        public ICollection<IBaseEntity> Assignments { get; set; }
    }
}

