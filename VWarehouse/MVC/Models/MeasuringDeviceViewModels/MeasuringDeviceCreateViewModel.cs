﻿using Model.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.MeasuringDeviceViewModels
{
    public class MeasuringDeviceCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CalibrationExpirationDate { get; set; }
    }
}