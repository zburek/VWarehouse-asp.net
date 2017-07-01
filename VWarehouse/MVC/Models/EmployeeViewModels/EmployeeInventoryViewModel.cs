using Model.Common;
using Model.Common.Inventory;
using System;
using System.Collections.Generic;


namespace MVC.Models.EmployeeViewModels
{
    public class EmployeeInventoryViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<IBaseInfo> Items { get; set; }
        public ICollection<IBaseInfo> MeasuringDevices { get; set; }
        public ICollection<IBaseInfo> Vehicles { get; set; }
    }
}