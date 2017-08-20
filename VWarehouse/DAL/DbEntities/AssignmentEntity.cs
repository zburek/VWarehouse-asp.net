using Common;
using DAL.DbEntities.Inventory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DbEntities
{
    [Table("Assignment")]
    public class AssignmentEntity : BaseEntity
    {
        public string Materials { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Note { get; set; }
        public virtual ICollection<EmployeeEntity> Employees { get; set; }
        public ICollection<ItemEntity> Items { get; set; }
        public ICollection<MeasuringDeviceEntity> MeasuringDevices { get; set; }
        public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}