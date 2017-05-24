using Model.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class Employee : IEmployee
    {
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<IBaseInfo> Items { get; set; }
        public ICollection<IBaseInfo> MeasuringDevices { get; set; }
        public ICollection<IBaseInfo> Vehicles { get; set; }
    }
}

