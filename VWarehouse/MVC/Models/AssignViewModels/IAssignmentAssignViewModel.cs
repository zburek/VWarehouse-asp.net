using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.AssignViewModels
{
    public interface IAssignmentAssignViewModel
    {
        Guid ID { get; set; }
        string Name { get; set; }
        string Materials { get; set; }
        DateTime? StartTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        DateTime? EndTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        string Note { get; set; }
        IEnumerable<Guid> AssignedEmployeeIDList { get; set; }
        IEnumerable<Guid> AssignedItemIDList { get; set; }
        IEnumerable<Guid> AssignedMeasuringDeviceIDList { get; set; }
        IEnumerable<Guid> AssignedVehicleIDList { get; set; }

        IEnumerable<IBaseEntity> EmployeeList { get; set; }
        IEnumerable<IBaseEntity> ItemList { get; set; }
        IEnumerable<IBaseEntity> MeasuringDeviceList { get; set; }
        IEnumerable<IBaseEntity> VehicleList { get; set; }
    }
}
