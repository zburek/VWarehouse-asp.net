using Model.Common;
using Model.Common.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Inventory
{
    public class Item : IItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(60)]
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public Guid? EmployeeID { get; set; }
        public virtual IEmployee Employee { get; set; }
        public virtual ICollection<IAssignment> Assignments { get; set; }
    }
}