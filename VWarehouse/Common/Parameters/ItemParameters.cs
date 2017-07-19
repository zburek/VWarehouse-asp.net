using System;

namespace Common.Parameters
{
    public interface IItemParameters : IParameters
    {
        Guid? EmployeeID { get; set; }
        bool OnStock { get; set; }
    }

    public class ItemParameters : Parameters, IItemParameters
    {
        public Guid? EmployeeID { get; set; }
        public bool OnStock { get; set; }
    }
}