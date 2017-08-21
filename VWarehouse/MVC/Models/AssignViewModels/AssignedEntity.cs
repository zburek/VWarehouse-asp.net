using Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC.Models.AssignViewModels
{
    public interface IAssignedEntity
    {
        Guid ID { get; set; }
        string Name { get; set; }
        bool isSelected { get; set; }

    }

    public class AssignedEntity : IAssignedEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid ID { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
        public bool isSelected { get; set; }
    }
}