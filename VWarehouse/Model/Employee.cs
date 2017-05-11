using Model.Common;
using Model.Inventory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public class Employee : IEmployee
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}

