using Model.DbEntities;
using Model.DbEntities.Inventory;
using System.Data.Entity;

namespace DAL
{
    public partial class VWarehouseContext : DbContext
    {
        public VWarehouseContext()
            : base("VWarehouseContext")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        public virtual DbSet<EmployeeEntity> Employees { get; set; }
        public virtual DbSet<ItemEntity> Items { get; set; }
        public virtual DbSet<MeasuringDeviceEntity> MeasuringDevices { get; set; }
        public virtual DbSet<VehicleEntity> Vehicles { get; set; }
    }
}
