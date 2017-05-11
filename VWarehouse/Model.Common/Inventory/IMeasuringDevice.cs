using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common.Inventory
{
    public interface IMeasuringDevice
    {
        int ID { get; set; }
        string Name { get; set; }
        string SerialNumber { get; set; }
        DateTime CalibrationExpirationDate { get; set; }
        int? EmployeeID { get; set; }
    }
}
