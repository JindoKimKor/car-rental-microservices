using Maintenance.WebAPI.Middleware;
using Maintenance.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Singleton to persist in-memory fake data across requests (no real DB)
builder.Services.AddSingleton<IRepairHistoryService, FakeRepairHistoryService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();
