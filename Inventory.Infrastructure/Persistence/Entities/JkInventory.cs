using System;
using System.Collections.Generic;

namespace JK_Inventory.Infrastructure.Persistence.Entities;

public partial class JkInventory
{
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public int VehicleLocationId { get; set; }

    public int VehicleStatusId { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual JkVehicle Vehicle { get; set; } = null!;

    public virtual JkVehicleLocation VehicleLocation { get; set; } = null!;

    public virtual JkVehicleStatus VehicleStatus { get; set; } = null!;
}
