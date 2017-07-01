using Model.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.VehicleViewModels
{
    public class VehicleDeleteViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string LicensePlate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LicenseExpirationDate { get; set; }
        public int Mileage { get; set; }
        public int NextService { get; set; }
        public Guid? EmployeeID { get; set; }
        public virtual IEmployee Employee { get; set; }
    }
}