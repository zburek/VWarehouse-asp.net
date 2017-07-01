using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.EmployeeViewModels
{
    public class EmployeeEditViewModel
    {
        public Guid ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}