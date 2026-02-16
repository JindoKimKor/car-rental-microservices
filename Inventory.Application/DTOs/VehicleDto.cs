namespace Inventory.Application.DTOs
{
	public class VehicleDto
	{
		public int Id { get; set; }
		public string VehicleCode { get; set; } = string.Empty;
		public int LocationId { get; set; }
		public string VehicleType { get; set; } = string.Empty;
		public string Status { get; set; } = string.Empty;
	}
}
