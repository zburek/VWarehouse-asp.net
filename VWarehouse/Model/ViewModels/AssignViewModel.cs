using Model.Common;
using Model.Common.ViewModels;
using System.Collections.Generic;

namespace Model.ViewModels
{
    public class AssignViewModel : IAssignViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? EmployeeID { get; set; }
        public IEnumerable<IEmployee> EmployeeList { get; set; }
    }
}
