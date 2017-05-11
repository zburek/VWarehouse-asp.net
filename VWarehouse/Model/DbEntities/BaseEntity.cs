using Model.Common.DbEntities;
using System.ComponentModel.DataAnnotations;

namespace Model.DbEntities
{
    public class BaseEntity : IBaseEntity
    {
        [Required]
        public int ID { get; set; }
        [Required]
        [MaxLength(40)]
        public string Name { get; set; }
    }
}
