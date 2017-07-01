using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.MeasuringDeviceViewModels
{
    public class MeasuringDeviceOnStockViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CalibrationExpirationDate { get; set; }
    }
}