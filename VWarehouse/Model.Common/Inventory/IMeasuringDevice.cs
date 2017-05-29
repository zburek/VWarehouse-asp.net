using System;
using System.ComponentModel.DataAnnotations;

namespace Model.Common.Inventory
{
    public interface IMeasuringDevice
    {
        int ID { get; set; }
        string Name { get; set; }
        string SerialNumber { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString= "{0:dd.MM.yyyy}", ApplyFormatInEditMode= true)]
        DateTime CalibrationExpirationDate { get; set; }
        int? EmployeeID { get; set; }
        IEmployee Employee { get; set; }

    }
}
