using System.ComponentModel.DataAnnotations;


namespace MVC.Models.ItemViewModels
{
    public class ItemCreateViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(60)]
        public string Description { get; set; }
        public string SerialNumber { get; set; }
    }
}