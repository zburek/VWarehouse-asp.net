
namespace Model.Common.Inventory
{
    public interface IItem
    {
        int ID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string SerialNumber { get; set; }
        int? EmployeeID { get; set; }
        IEmployee Employee { get; set; }
    }
}
