using System.Net.Http.Json;
using CarRental.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.MVC.Controllers
{
	public class MaintenanceController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public MaintenanceController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		[HttpGet]
		public IActionResult History()
		{
			return View(new List<RepairHistoryViewModel>());
		}

		[HttpPost]
		public async Task<IActionResult> History(int vehicleId)
		{
			var client = _httpClientFactory.CreateClient("MaintenanceApi");

			var repairs = await client.GetFromJsonAsync<List<RepairHistoryViewModel>>(
				$"api/maintenance/vehicles/{vehicleId}/repairs");

			return View(repairs ?? new List<RepairHistoryViewModel>());
		}

		[HttpGet]
		public async Task<IActionResult> Usage()
		{
			var client = _httpClientFactory.CreateClient("MaintenanceApi");
			var result = await client.GetFromJsonAsync<UsageViewModel>("api/RepairHistory/usage");
			return View(result);
		}

		[HttpGet]
		public async Task<IActionResult> Transfer(int fromId, int toId, decimal amount)
		{
			var client = _httpClientFactory.CreateClient("MaintenanceApi");

			var response = await client.PostAsync(
				$"api/RepairHistory/transfer?fromId={fromId}&toId={toId}&amount={amount}",
				null);

			var content = await response.Content.ReadAsStringAsync();

			ViewBag.Result = content;
			return View();
		}


	}
}
