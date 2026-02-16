using System;
using System.Collections.Generic;

namespace JK_Inventory.Infrastructure.Persistence.Entities;

public partial class JkVehicleStatus
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<JkInventory> JkInventories { get; set; } = new List<JkInventory>();
}
