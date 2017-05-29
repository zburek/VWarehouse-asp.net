using Model.Common.DbEntities;
using Model.DbEntities.Inventory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DbEntities
{
    [Table("Employee")]
    public class EmployeeEntity : BaseEntity, IEmployeeEntity
    {
        public string PhoneNumber { get; set; }
        /// Add code for additional tables in VWarehouse
        public ICollection<ItemEntity> Items { get; set; }
        public ICollection<MeasuringDeviceEntity> MeasuringDevices { get; set; }
        public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}

