﻿using Model.Common.DbEntities;
using Model.Common.DbEntities.Inventory;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DbEntities.Inventory
{
    [Table("Item")]
    public class ItemEntity : BaseEntity, IItemEntity, IBaseEntity
    {
        [MaxLength(60)]
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        [ForeignKey("EmployeeEntity")]
        public int? EmployeeID { get; set; }
        public virtual EmployeeEntity EmployeeEntity { get; set; }
    }
}