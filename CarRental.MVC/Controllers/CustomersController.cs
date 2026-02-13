using Microsoft.AspNetCore.Mvc;
using Customer.WebAPI.Models;
using System.Text;
using System.Text.Json;

namespace CarRental.MVC.Controllers
{
	public class CustomersController : Controller
	{
		private readonly HttpClient _client;
		private readonly JsonSerializerOptions _jsonOptions = new()
		{
			PropertyNameCaseInsensitive = true
		};

		public CustomersController(IHttpClientFactory clientFactory)
		{
			_client = clientFactory.CreateClient("CustomerAPI");
		}

		public async Task<IActionResult> Index()
		{
			var response = await _client.GetAsync("Customers");
			var customers = await response.Content.ReadFromJsonAsync<List<Customer.WebAPI.Models.Customer>>(_jsonOptions);
			return View(customers);
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null) return NotFound();

			var response = await _client.GetAsync($"Customers/{id}");
			if (!response.IsSuccessStatusCode) return NotFound();

			var customer = await response.Content.ReadFromJsonAsync<Customer.WebAPI.Models.Customer>(_jsonOptions);
			return View(customer);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Customer.WebAPI.Models.Customer customer)
		{
			if (ModelState.IsValid)
			{
				var response = await _client.PostAsJsonAsync("Customers", customer);
				if (response.IsSuccessStatusCode)
					return RedirectToAction(nameof(Index));
			}
			return View(customer);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();

			var response = await _client.GetAsync($"Customers/{id}");
			if (!response.IsSuccessStatusCode) return NotFound();

			var customer = await response.Content.ReadFromJsonAsync<Customer.WebAPI.Models.Customer>(_jsonOptions);
			return View(customer);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Customer.WebAPI.Models.Customer customer)
		{
			if (id != customer.Id) return NotFound();

			if (ModelState.IsValid)
			{
				var response = await _client.PutAsJsonAsync($"Customers/{id}", customer);
				if (response.IsSuccessStatusCode)
					return RedirectToAction(nameof(Index));
			}
			return View(customer);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null) return NotFound();

			var response = await _client.GetAsync($"Customers/{id}");
			if (!response.IsSuccessStatusCode) return NotFound();

			var customer = await response.Content.ReadFromJsonAsync<Customer.WebAPI.Models.Customer>(_jsonOptions);
			return View(customer);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _client.DeleteAsync($"Customers/{id}");
			return RedirectToAction(nameof(Index));
		}
	}
}