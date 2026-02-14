using System.ComponentModel.DataAnnotations;

namespace CarRental.MVC.Models;

public class CustomerViewModel
{
	public int Id { get; set; }

	[Required]
	[MaxLength(50)]
	public string FirstName { get; set; } = null!;

	public string LastName { get; set; } = null!;

	public string Phone { get; set; } = null!;

	[EmailAddress]
	public string Email { get; set; } = null!;
}