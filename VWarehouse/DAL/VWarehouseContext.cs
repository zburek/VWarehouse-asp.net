using DAL.DbEntities;
using DAL.DbEntities.Inventory;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace DAL
{
    public interface IVWarehouseContext : IDisposable
    {
        DbSet<EmployeeEntity> Employees { get; set; }
        DbSet<ItemEntity> Items { get; set; }
        DbSet<MeasuringDeviceEntity> MeasuringDevices { get; set; }
        DbSet<VehicleEntity> Vehicles { get; set; }
        Task<int> SaveChangesAsync();
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry Entry(object entity);
    }

    public partial class VWarehouseContext : DbContext, IVWarehouseContext
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
