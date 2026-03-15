using Inventory.Application.Services;
using Inventory.Domain.AggregatesModel.InventoryAggregate;
using JK_Inventory.Application.Interfaces;
using JK_Inventory.Infrastructure;
using JK_Inventory.Infrastructure.Repositories;
using CarRental.SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<JK_InventoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InventoryDb")));

builder.Services.AddScoped<IInventoryRepository, JK_InventoryRepository>();
builder.Services.AddScoped<IVehicleService, JK_VehicleService>();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
