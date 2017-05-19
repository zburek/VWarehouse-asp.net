
namespace Model.Common.DbEntities.Inventory
{
    public interface IItemEntity : IBaseEntity
    {
        string Description { get; set; }
        string SerialNumber { get; set; }
        int? EmployeeID { get; set; }

    }
}
