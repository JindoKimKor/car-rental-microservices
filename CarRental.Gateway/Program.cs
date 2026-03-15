var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment.EnvironmentName;
builder.Configuration
	.AddJsonFile("yarp.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"yarp.{env}.json", optional: true, reloadOnChange: true);

builder.Services.AddReverseProxy()
	.LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapReverseProxy();

app.Run();
