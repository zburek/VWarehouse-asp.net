
using System.Collections.Generic;

namespace Model.Common
{
    public interface IEmployee
    {
        int ID { get; set; }
        string Name { get; set; }
        string PhoneNumber { get; set; }
        ICollection<IBaseInfo> Items { get; set; }
        ICollection<IBaseInfo> MeasuringDevices { get; set; }
        ICollection<IBaseInfo> Vehicles { get; set; }
    }
}
