using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.AssignViewModels
{
    public class AssignmentAssignViewModel : IAssignmentAssignViewModel
    {
        public Guid ID { get; set; }
        public  string Name { get; set; }
        public string Materials { get; set; }
        public DateTime? StartTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public string Note { get; set; }
        public  IEnumerable<Guid> AssignedEmployeeIDList { get; set; }
        public  IEnumerable<Guid> AssignedItemIDList { get; set; }
        public  IEnumerable<Guid> AssignedMeasuringDeviceIDList { get; set; }
        public  IEnumerable<Guid> AssignedVehicleIDList { get; set; }

        public  IEnumerable<IBaseEntity> EmployeeList { get; set; }
        public  IEnumerable<IBaseEntity> ItemList { get; set; }
        public  IEnumerable<IBaseEntity> MeasuringDeviceList { get; set; }
        public  IEnumerable<IBaseEntity> VehicleList { get; set; }
    }
}