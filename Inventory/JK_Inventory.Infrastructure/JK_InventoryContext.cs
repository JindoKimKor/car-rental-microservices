using Inventory.Domain.AggregatesModel.InventoryAggregate;
using Inventory.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using InventoryEntity = Inventory.Domain.AggregatesModel.InventoryAggregate.Inventory;

namespace JK_Inventory.Infrastructure
{
	public class JK_InventoryContext : DbContext, IUnitOfWork
	{
		public DbSet<InventoryEntity> Inventories { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<VehicleLocation> VehicleLocations { get; set; }
		public DbSet<VehicleStatus> VehicleStatuses { get; set; }
		public DbSet<VehicleType> VehicleTypes { get; set; }

		public JK_InventoryContext(DbContextOptions<JK_InventoryContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Inventory table
			modelBuilder.Entity<InventoryEntity>(entity =>
			{
				entity.ToTable("Inventory");
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id).ValueGeneratedOnAdd();

				entity.HasOne(e => e.Vehicle)
					.WithMany()
					.HasForeignKey("VehicleId")
					.OnDelete(DeleteBehavior.ClientSetNull);

				entity.HasOne(e => e.Location)
					.WithMany()
					.HasForeignKey(e => e.VehicleLocationId)
					.OnDelete(DeleteBehavior.ClientSetNull);

				entity.HasOne(e => e.Status)
					.WithMany()
					.HasForeignKey(e => e.VehicleStatusId)
					.OnDelete(DeleteBehavior.ClientSetNull);
			});

			// Vehicle table
			modelBuilder.Entity<Vehicle>(entity =>
			{
				entity.ToTable("Vehicle");
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Id).ValueGeneratedOnAdd();

				entity.OwnsOne(e => e.VehicleCode, vc =>
				{
					vc.Property(v => v.Make).HasColumnName("Make").HasMaxLength(50).IsRequired();
					vc.Property(v => v.Model).HasColumnName("Model").HasMaxLength(50).IsRequired();
					vc.Property(v => v.Type).HasColumnName("VehicleTypeId");
				});
			});

			// VehicleLocation table
			modelBuilder.Entity<VehicleLocation>(entity =>
			{
				entity.ToTable("VehicleLocation");
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
				entity.HasIndex(e => e.Name).IsUnique();
			});

			// VehicleStatus table
			modelBuilder.Entity<VehicleStatus>(entity =>
			{
				entity.ToTable("VehicleStatus");
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Name).HasMaxLength(30).IsRequired();
				entity.HasIndex(e => e.Name).IsUnique();
			});

			// VehicleType table
			modelBuilder.Entity<VehicleType>(entity =>
			{
				entity.ToTable("VehicleType");
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Name).HasMaxLength(50).IsRequired();
				entity.HasIndex(e => e.Name).IsUnique();
			});

			// Seed data — reference from SQL script
			modelBuilder.Entity<VehicleType>().HasData(
				new { Id = 1, Name = "Sedan" },
				new { Id = 2, Name = "SUV" },
				new { Id = 3, Name = "Truck" },
				new { Id = 4, Name = "Van" }
			);

			modelBuilder.Entity<VehicleLocation>().HasData(
				new { Id = 1, Name = "Kitchener" },
				new { Id = 2, Name = "Waterloo" },
				new { Id = 3, Name = "Cambridge" },
				new { Id = 4, Name = "Guelph" }
			);

			modelBuilder.Entity<VehicleStatus>().HasData(
				new { Id = 1, Name = "Available" },
				new { Id = 2, Name = "Reserved" },
				new { Id = 3, Name = "Rented" },
				new { Id = 4, Name = "Maintenance" }
			);

			// Vehicle seed data
			modelBuilder.Entity<Vehicle>().HasData(
				new { Id = 1 },
				new { Id = 2 },
				new { Id = 3 },
				new { Id = 4 },
				new { Id = 5 },
				new { Id = 6 }
			);

			modelBuilder.Entity<Vehicle>().OwnsOne(e => e.VehicleCode).HasData(
				new { VehicleId = 1, Make = "Toyota", Model = "Camry", Type = VehicleTypeEnum.Sedan },
				new { VehicleId = 2, Make = "Honda", Model = "Civic", Type = VehicleTypeEnum.Sedan },
				new { VehicleId = 3, Make = "Ford", Model = "Escape", Type = VehicleTypeEnum.SUV },
				new { VehicleId = 4, Make = "Toyota", Model = "RAV4", Type = VehicleTypeEnum.SUV },
				new { VehicleId = 5, Make = "Ford", Model = "F-150", Type = VehicleTypeEnum.Truck },
				new { VehicleId = 6, Make = "Chevy", Model = "Express", Type = VehicleTypeEnum.Van }
			);

			// Inventory seed data
			modelBuilder.Entity<InventoryEntity>().HasData(
				new { Id = 1, VehicleId = 1, VehicleLocationId = 1, VehicleStatusId = 1 },
				new { Id = 2, VehicleId = 2, VehicleLocationId = 1, VehicleStatusId = 2 },
				new { Id = 3, VehicleId = 3, VehicleLocationId = 2, VehicleStatusId = 1 },
				new { Id = 4, VehicleId = 4, VehicleLocationId = 2, VehicleStatusId = 3 },
				new { Id = 5, VehicleId = 5, VehicleLocationId = 3, VehicleStatusId = 1 },
				new { Id = 6, VehicleId = 6, VehicleLocationId = 3, VehicleStatusId = 4 },
				new { Id = 7, VehicleId = 1, VehicleLocationId = 4, VehicleStatusId = 1 },
				new { Id = 8, VehicleId = 2, VehicleLocationId = 4, VehicleStatusId = 2 },
				new { Id = 9, VehicleId = 3, VehicleLocationId = 1, VehicleStatusId = 1 },
				new { Id = 10, VehicleId = 4, VehicleLocationId = 2, VehicleStatusId = 3 }
			);
		}
	}
}
