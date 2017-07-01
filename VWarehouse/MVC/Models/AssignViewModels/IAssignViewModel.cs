using Model.Common;
using System;
using System.Collections.Generic;

namespace MVC.Models.AssignViewModels
{
    public interface IAssignViewModel
    {
        Guid ID { get; set; }
        string Name { get; set; }
        Guid? EmployeeID { get; set; }
        IEnumerable<IEmployee> EmployeeList { get; set; }
    }
}
