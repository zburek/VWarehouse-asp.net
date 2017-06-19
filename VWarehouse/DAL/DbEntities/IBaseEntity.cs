
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DbEntities
{
   public interface IBaseEntity
    {
        Guid ID { get; set; }
        string Name { get; set; }
    }
}
