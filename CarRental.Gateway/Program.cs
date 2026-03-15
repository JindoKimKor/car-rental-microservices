using CarRental.Gateway.Middleware;
using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment.EnvironmentName;
builder.Configuration
	.AddJsonFile("yarp.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"yarp.{env}.json", optional: true, reloadOnChange: true);

builder.Services.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// RateLimiting

builder.Services.AddRateLimiter(options =>
{
	options.RejectionStatusCode = 429;
	options.AddFixedWindowLimiter("fixed", opt =>
	{
		opt.Window = TimeSpan.FromSeconds(10);
		opt.PermitLimit = 5;
		opt.QueueLimit = 0;
	});
});

var app = builder.Build();

app.UseMiddleware<ApiKeyMiddleware>();

app.UseRateLimiter();

app.MapReverseProxy();

app.Run();
