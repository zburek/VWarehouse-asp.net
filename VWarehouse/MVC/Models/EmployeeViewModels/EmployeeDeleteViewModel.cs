﻿using System;

namespace MVC.Models.EmployeeViewModels
{
    public class EmployeeDeleteViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}