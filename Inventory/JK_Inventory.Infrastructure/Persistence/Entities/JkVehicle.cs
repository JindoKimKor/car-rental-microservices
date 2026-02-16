using System;
using System.Collections.Generic;

namespace JK_Inventory.Infrastructure.Persistence.Entities;

public partial class JkVehicle
{
    public int Id { get; set; }

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int VehicleTypeId { get; set; }

    public virtual ICollection<JkInventory> JkInventories { get; set; } = new List<JkInventory>();

    public virtual JkVehicleType VehicleType { get; set; } = null!;
}
