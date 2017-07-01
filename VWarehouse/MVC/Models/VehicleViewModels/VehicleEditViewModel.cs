using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.VehicleViewModels
{
    public class VehicleEditViewModel
    {
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        public string Name { get; set; }
        public string Type { get; set; }
        public string LicensePlate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime LicenseExpirationDate { get; set; }
        public int Mileage { get; set; }
        public int NextService { get; set; }
    }
}