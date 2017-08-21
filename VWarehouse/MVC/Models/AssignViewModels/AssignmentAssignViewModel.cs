using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.AssignViewModels
{
    public interface IAssignmentAssignViewModel
    {
        Guid? ID { get; set; }
        string Name { get; set; }
        string Materials { get; set; }
        DateTime? StartTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        DateTime? EndTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        string Note { get; set; }
        IEnumerable<IAssignedEntity> EmployeeList { get; set; }
        IEnumerable<IAssignedEntity> ItemList { get; set; }
        IEnumerable<IAssignedEntity> MeasuringDeviceList { get; set; }
        IEnumerable<IAssignedEntity> VehicleList { get; set; }
    }

    public class AssignmentAssignViewModel : IAssignmentAssignViewModel
    {
        public Guid? ID { get; set; }
        public  string Name { get; set; }
        public string Materials { get; set; }
        public DateTime? StartTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public string Note { get; set; }
        public  IEnumerable<IAssignedEntity> EmployeeList { get; set; }
        public  IEnumerable<IAssignedEntity> ItemList { get; set; }
        public  IEnumerable<IAssignedEntity> MeasuringDeviceList { get; set; }
        public  IEnumerable<IAssignedEntity> VehicleList { get; set; }
    }
}