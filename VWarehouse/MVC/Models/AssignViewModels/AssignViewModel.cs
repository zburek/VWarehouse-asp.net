using Model.Common;
using System;
using System.Collections.Generic;

namespace MVC.Models.AssignViewModels
{
    public class AssignViewModel : IAssignViewModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid? EmployeeID { get; set; }
        public IEnumerable<IEmployee> EmployeeList { get; set; }
    }
}
