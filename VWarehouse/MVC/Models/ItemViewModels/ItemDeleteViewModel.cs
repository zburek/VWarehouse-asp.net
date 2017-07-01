using Model.Common;
using System;

namespace MVC.Models.ItemViewModels
{
    public class ItemDeleteViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public Guid? EmployeeID { get; set; }
        public virtual IEmployee Employee { get; set; }
    }
}