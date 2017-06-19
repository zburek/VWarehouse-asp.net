using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Common
{
    public interface IBaseInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        Guid ID { get; set; }
        string Name { get; set; }
    }

    // Created for displaying Employee Name in Inventory tables
    // When IEmployee<IItem> or other inventory table is used IIS stops working
    // My guess is that infinitve loop is created (IEmployee<IItem> creates
    // IItem that has IEmployee which creates new IEmployee<IItem> which 
    // creates IItem that has IEmployee which creates ...)

    // IBaseInfo can be expanded into more inventory clases for displaying more info then just Name
}
