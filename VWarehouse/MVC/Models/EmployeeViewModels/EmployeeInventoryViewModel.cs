using Common;
using System;
using System.Collections.Generic;


namespace MVC.Models.EmployeeViewModels
{
    public class EmployeeInventoryViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<IBaseEntity> Items { get; set; }
        public ICollection<IBaseEntity> MeasuringDevices { get; set; }
        public ICollection<IBaseEntity> Vehicles { get; set; }
    }
}