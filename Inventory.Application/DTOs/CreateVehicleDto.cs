namespace Inventory.Application.DTOs
{
	public class CreateVehicleDto
	{
		public string VehicleCode { get; set; } = string.Empty;
		public int LocationId { get; set; }
		public string VehicleType { get; set; } = string.Empty;
	}
}
