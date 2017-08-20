using Common;
using System;
using System.Collections.Generic;

namespace MVC.Models.AssignmentViewModels
{
    public class AssignmentDetailsViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Materials { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Note { get; set; }
        public virtual ICollection<IBaseEntity> Employees { get; set; }
        public virtual ICollection<IBaseEntity> Items { get; set; }
        public virtual ICollection<IBaseEntity> MeasuringDevices { get; set; }
        public virtual ICollection<IBaseEntity> Vehicles { get; set; }
    }
}