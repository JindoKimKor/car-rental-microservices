namespace JK_Inventory.Application.DTOs
{
	public class JK_VehicleDto
	{
		public int Id { get; set; }
		public string Make { get; set; } = string.Empty;
		public string Model { get; set; } = string.Empty;
		public int LocationId { get; set; }
		public int VehicleTypeId { get; set; }
		public string Status { get; set; } = string.Empty;
	}
}
