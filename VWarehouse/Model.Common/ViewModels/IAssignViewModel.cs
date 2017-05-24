

using System.Collections.Generic;

namespace Model.Common.ViewModels
{
    public interface IAssignViewModel
    {
        int ID { get; set; }
        string Name { get; set; }
        int? EmployeeID { get; set; }
        IEnumerable<IEmployee> EmployeeList { get; set; }
    }
}
