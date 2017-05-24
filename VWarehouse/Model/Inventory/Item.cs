using Model.Common;
using Model.Common.Inventory;
using System.ComponentModel.DataAnnotations;

namespace Model.Inventory
{
    public class Item : IItem
    {
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(40)]
        public string Name { get; set; }
        [MaxLength(60)]
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public int? EmployeeID { get; set; }
        public virtual IEmployee Employee { get; set; }
    }
}