using System;
using System.Collections.Generic;

namespace JK_Inventory.Infrastructure.Persistence.Entities;

public partial class JkVehicleType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<JkVehicle> JkVehicles { get; set; } = new List<JkVehicle>();
}
