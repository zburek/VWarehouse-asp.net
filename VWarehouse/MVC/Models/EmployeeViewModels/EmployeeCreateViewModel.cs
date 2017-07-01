using System.ComponentModel.DataAnnotations;


namespace MVC.Models.EmployeeViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
    }
}