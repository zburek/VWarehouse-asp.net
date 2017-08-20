using Common;
using DAL.DbEntities.Inventory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DbEntities
{
    [Table("Employee")]
    public class EmployeeEntity : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public ICollection<ItemEntity> Items { get; set; }
        public ICollection<MeasuringDeviceEntity> MeasuringDevices { get; set; }
        public ICollection<VehicleEntity> Vehicles { get; set; }
        public ICollection<AssignmentEntity> Assignments { get; set; }
    }
}

