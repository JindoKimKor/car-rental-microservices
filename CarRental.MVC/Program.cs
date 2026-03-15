var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Gateway Configuration
var gatewayUrl = builder.Configuration["ApiSettings:GatewayBaseUrl"]!;
var apiKey = builder.Configuration["ApiSettings:ApiKey"]!;

builder.Services.AddHttpClient("CustomerAPI", client =>
{
	client.BaseAddress = new Uri($"{gatewayUrl}/customer-service/api/");
	client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
});

builder.Services.AddHttpClient("MaintenanceApi", client =>
{
	client.BaseAddress = new Uri($"{gatewayUrl}/maintenance-service/");
	client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
});

builder.Services.AddHttpClient("InventoryAPI", client =>
{
	client.BaseAddress = new Uri($"{gatewayUrl}/inventory-service/api/");
	client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
