using DAL.DbEntities;
using DAL.DbEntities.Inventory;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Threading.Tasks;

namespace DAL
{
    public interface IVWarehouseContext : IDisposable
    {
        DbSet<EmployeeEntity> Employees { get; set; }
        DbSet<ItemEntity> Items { get; set; }
        DbSet<MeasuringDeviceEntity> MeasuringDevices { get; set; }
        DbSet<VehicleEntity> Vehicles { get; set; }
        DbSet<AssignmentEntity> Assignments {get;set;}
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
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<EmployeeEntity>()
                .HasMany(t => t.Assignments)
                .WithMany(t => t.Employees)
                .Map(m =>
                {
                    m.ToTable("EmployeeAssigments");
                    m.MapLeftKey("EmployeeID");
                    m.MapRightKey("AssignmentID");
                });

            modelBuilder.Entity<ItemEntity>()
                .HasMany(t => t.Assignments)
                .WithMany(t => t.Items)
                .Map(m =>
                {
                    m.ToTable("ItemAssigments");
                    m.MapLeftKey("ItemID");
                    m.MapRightKey("AssignmentID");
                });

            modelBuilder.Entity<MeasuringDeviceEntity>()
                .HasMany(t => t.Assignments)
                .WithMany(t => t.MeasuringDevices)
                .Map(m =>
                {
                    m.ToTable("MeasuringDeviceAssigments");
                    m.MapLeftKey("MeasuringDeviceID");
                    m.MapRightKey("AssignmentID");
                });

            modelBuilder.Entity<VehicleEntity>()
                .HasMany(t => t.Assignments)
                .WithMany(t => t.Vehicles)
                .Map(m =>
                {
                    m.ToTable("VehicleAssigments");
                    m.MapLeftKey("VehicleID");
                    m.MapRightKey("AssignmentID");
                });
        }
        public virtual DbSet<EmployeeEntity> Employees { get; set; }
        public virtual DbSet<ItemEntity> Items { get; set; }
        public virtual DbSet<MeasuringDeviceEntity> MeasuringDevices { get; set; }
        public virtual DbSet<VehicleEntity> Vehicles { get; set; }
        public virtual DbSet<AssignmentEntity> Assignments {get;set;}
    }
}
