using System;
using System.ComponentModel.DataAnnotations;

namespace MVC.Models.AssignmentViewModels
{
    public class AssignmentCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        public string Name { get; set; }
        public string Materials { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Note { get; set; }
    }
}