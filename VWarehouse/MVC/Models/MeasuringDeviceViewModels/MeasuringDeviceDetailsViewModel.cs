using Model.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.MeasuringDeviceViewModels
{
    public class MeasuringDeviceDetailsViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CalibrationExpirationDate { get; set; }
        public Guid? EmployeeID { get; set; }
        public virtual IEmployee Employee { get; set; }
    }
}