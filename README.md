# CarRental Microservice Platform

PROG3176 Assignment 2

## Architecture Overview

CarRental is a microservice platform with 3 backend services, an API Gateway, and an MVC client. All client traffic flows through the Gateway — no direct access to backend services.

```
                     CarRental.MVC
                         │
                    [API Key Auth]
                         │
                   CarRental.Gateway
                    (YARP Reverse Proxy)
                         │
          ┌──────────────┼──────────────┐
          │              │              │
     [Rate Limit]        │              │
          │              │              │
          ▼              ▼              ▼
   Inventory        Maintenance     Customer
   WebAPI            WebAPI          WebAPI
   (:7253)           (:7228)        (:7238)
```

## Projects

| Project | Layer | Description |
|---------|-------|-------------|
| `CarRental.Gateway` | Gateway | YARP reverse proxy, API Key auth, Rate Limiting |
| `CarRental.SharedKernel` | Shared | GlobalExceptionHandler, GatewayOnlyMiddleware |
| `CarRental.MVC` | UI | MVC client — communicates only through Gateway |
| `Inventory.Domain` | Domain | SeedWork, InventoryAggregate (DDD) |
| `JK_Inventory.Application` | Application | VehicleService, DTOs, Interfaces |
| `JK_Inventory.Infrastructure` | Infrastructure | Code-First DbContext, Repository |
| `JK_Inventory.WebAPI` | WebAPI | REST endpoints for Inventory |
| `Maintenance.WebAPI` | WebAPI | Repair history service |
| `Customer.WebAPI` | WebAPI | Customer CRUD service |
| `JK_Inventory.Domain.Tests` | Tests | 23 unit tests |

## Assignment 2 Changes

### 1. API Gateway (YARP)
- Single entry point for all client-to-service communication
- YARP reverse proxy with 3 routes (inventory, maintenance, customer)
- Config split: `yarp.json` (routes) + `yarp.Development.json` (clusters) — DRY
- API Key authentication middleware — validates `X-Api-Key` header
- Rate Limiting on maintenance-route (5 req/10sec, 429 on exceed)
- Direct access blocking via `GatewayOnlyMiddleware` (SharedKernel) — 403 Forbidden

### 2. Global Exception Handling (SharedKernel)
- `GlobalExceptionHandler` implementing `IExceptionHandler` (.NET 8 pattern)
- Consistent ProblemDetails error format across all 3 WebAPIs
- Exception mapping: ArgumentException→400, KeyNotFoundException→404, fallback→500
- Replaced individual middleware in each WebAPI (DRY)

### 3. DDD Fixes (Inventory Service)
- Full domain redesign based on eShopOnContainers reference architecture
- `Inventory` as Aggregate Root (was Vehicle), `Vehicle` as Child Entity
- SeedWork: Entity, ValueObject, IAggregateRoot, IRepository<T>, IUnitOfWork
- Enum+Entity dual pattern for lookup tables (VehicleStatus, VehicleType, VehicleLocation)
- Repository interfaces in Domain layer (was Application)
- Code-First: Domain Entity = DB table (removed scaffold entities)
- 23 unit tests covering status transitions, entity relationships, value object equality

## Run Instructions

### Prerequisites
- .NET 10 SDK
- SQL Server (LocalDB or full instance)

### 1. Database Setup
```bash
# From solution root — creates DB from Code-First migration
dotnet ef database update --project Inventory/JK_Inventory.Infrastructure --startup-project Inventory/JK_Inventory.WebAPI
```

### 2. Run All Services
Set **Multiple Startup Projects** in Visual Studio:
1. JK_Inventory.WebAPI → Start
2. Customer.WebAPI → Start
3. Maintenance.WebAPI → Start
4. CarRental.Gateway → Start (last)

Or run individually:
```bash
dotnet run --project Inventory/JK_Inventory.WebAPI
dotnet run --project Customer.WebAPI
dotnet run --project Maintenance.WebAPI
dotnet run --project CarRental.Gateway
```

### 3. Test via Gateway
```bash
# All requests go through Gateway with API Key
curl -k https://localhost:7058/inventory-service/api/JK_Vehicles -H "X-Api-Key: MY_SECRET_KEY_123"
curl -k https://localhost:7058/customer-service/api/Customers -H "X-Api-Key: MY_SECRET_KEY_123"
curl -k https://localhost:7058/maintenance-service/api/RepairHistory/1 -H "X-Api-Key: MY_SECRET_KEY_123"
```

### 4. Run Unit Tests
```bash
dotnet test Inventory/JK_Inventory.Domain.Tests
```

## Service Ports

| Service | HTTPS | HTTP |
|---------|-------|------|
| Gateway | 7058 | 5210 |
| Inventory WebAPI | 7253 | 5189 |
| Maintenance WebAPI | 7228 | 5243 |
| Customer WebAPI | 7238 | 5180 |
