using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DbEntities.Inventory
{
    [Table("Item")]
    public class ItemEntity : BaseEntity
    {
        [MaxLength(60)]
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        [ForeignKey("Employee")]
        public Guid? EmployeeID { get; set; }
        public virtual EmployeeEntity Employee { get; set; }
        public virtual ICollection<AssignmentEntity> Assignments { get; set; }
    }
}