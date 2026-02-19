using Maintenance.WebAPI.Middleware;
using Maintenance.WebAPI.Services;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Singleton to persist in-memory fake data across requests (no real DB)
builder.Services.AddSingleton<IRepairHistoryService, FakeRepairHistoryService>();

var usageCounts = new Dictionary<string, int>();
builder.Services.AddSingleton(usageCounts);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
	{
		Name = "X-Api-Key",
		Type = SecuritySchemeType.ApiKey,
		In = ParameterLocation.Header,
		Description = "Enter your API key"
	});

	options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
	{
		[new OpenApiSecuritySchemeReference("ApiKey", document)] = []
	});
});

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();


// --- Part 2: API Key Authentication (inline middleware) ---
const string API_KEY = "MY_SECRET_KEY_123";

app.Use(async (context, next) =>
{
	if (!context.Request.Headers.TryGetValue("X-Api-Key", out var key) ||
		key != API_KEY)
	{
		context.Response.StatusCode = 401;
		await context.Response.WriteAsJsonAsync(new
		{
			error = "Unauthorized",
			message = "Missing or invalid API key."
		});
		return;
	}

	await next();
});

// --- Part 1: Global Exception Handling ---
// Inline version (replaced by GlobalExceptionMiddleware class):
//app.Use(async (context, next) =>
//{
//	try
//	{
//		await next();
//	}
//	catch (Exception ex)
//	{
//		Console.WriteLine(ex.Message);
//		context.Response.StatusCode = 500;
//		context.Response.ContentType = "application/json";
//		await context.Response.WriteAsJsonAsync(new
//		{
//			error = "ServerError",
//			message = "An unexpected error occurred."
//		});
//	}
//});

// Class-based version (uses ILogger instead of Console.WriteLine):
app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
