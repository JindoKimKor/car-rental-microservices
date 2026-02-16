using System;
using System.Collections.Generic;
using JK_Inventory.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace JK_Inventory.Infrastructure.Persistence;

public partial class JK_InventoryDbContext : DbContext
{
    public JK_InventoryDbContext()
    {
    }

    public JK_InventoryDbContext(DbContextOptions<JK_InventoryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<JkInventory> JkInventories { get; set; }

    public virtual DbSet<JkVehicle> JkVehicles { get; set; }

    public virtual DbSet<JkVehicleLocation> JkVehicleLocations { get; set; }

    public virtual DbSet<JkVehicleStatus> JkVehicleStatuses { get; set; }

    public virtual DbSet<JkVehicleType> JkVehicleTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=JK_VehicleInventoryDb;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<JkInventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JK_Inven__3214EC079D5A4056");

            entity.ToTable("JK_Inventory");

            entity.Property(e => e.LastUpdated).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.JkInventories)
                .HasForeignKey(d => d.VehicleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JK_Inventory_JK_Vehicle");

            entity.HasOne(d => d.VehicleLocation).WithMany(p => p.JkInventories)
                .HasForeignKey(d => d.VehicleLocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JK_Inventory_JK_VehicleLocation");

            entity.HasOne(d => d.VehicleStatus).WithMany(p => p.JkInventories)
                .HasForeignKey(d => d.VehicleStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JK_Inventory_JK_VehicleStatus");
        });

        modelBuilder.Entity<JkVehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JK_Vehic__3214EC07AFA42C60");

            entity.ToTable("JK_Vehicle");

            entity.Property(e => e.Make).HasMaxLength(50);
            entity.Property(e => e.Model).HasMaxLength(50);

            entity.HasOne(d => d.VehicleType).WithMany(p => p.JkVehicles)
                .HasForeignKey(d => d.VehicleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JK_Vehicle_JK_VehicleType");
        });

        modelBuilder.Entity<JkVehicleLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JK_Vehic__3214EC078B3027B0");

            entity.ToTable("JK_VehicleLocation");

            entity.HasIndex(e => e.Name, "UQ__JK_Vehic__737584F6F18B7446").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<JkVehicleStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JK_Vehic__3214EC0706801015");

            entity.ToTable("JK_VehicleStatus");

            entity.HasIndex(e => e.Name, "UQ__JK_Vehic__737584F6967D7E2F").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<JkVehicleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JK_Vehic__3214EC078D8FD2FE");

            entity.ToTable("JK_VehicleType");

            entity.HasIndex(e => e.Name, "UQ__JK_Vehic__737584F6319E8563").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
