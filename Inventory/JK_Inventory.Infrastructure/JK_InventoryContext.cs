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
					.HasForeignKey("VehicleLocationId")
					.OnDelete(DeleteBehavior.ClientSetNull);

				entity.HasOne(e => e.Status)
					.WithMany()
					.HasForeignKey("VehicleStatusId")
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

					vc.HasOne(v => v.Type)
						.WithMany()
						.HasForeignKey("VehicleTypeId")
						.OnDelete(DeleteBehavior.ClientSetNull);
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
		}
	}
}
