using Model.Common.DbEntities;
using Model.DbEntities.Inventory;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.DbEntities
{
    [Table("Employee")]
    public class EmployeeEntity : BaseEntity, IEmployeeEntity, IBaseEntity
    {
        public string PhoneNumber { get; set; }
        /// Add code for additional tables in VWarehouse
        public ICollection<ItemEntity> Items { get; set; }
        public ICollection<MeasuringDeviceEntity> MeasuringDevices { get; set; }
        public ICollection<VehicleEntity> Vehicles { get; set; }
    }
}
/*
 Try implement Generic class for adding additional tables into VWarehouse

public class MyClass<T> where T : BaseEntity, IBaseEntity
    {
        public List<T> NewList { get; set; }

    }

*/
