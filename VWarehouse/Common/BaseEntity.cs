using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    public interface IBaseEntity
    {
        Guid ID { get; set; }
        string Name { get; set; }
    }
    public class BaseEntity : IBaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid ID { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
    }
}