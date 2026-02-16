USE [master]
GO
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'JK_VehicleInventoryDb')
    DROP DATABASE [JK_VehicleInventoryDb]
GO
CREATE DATABASE [JK_VehicleInventoryDb]
GO
USE [JK_VehicleInventoryDb]
GO

CREATE TABLE [dbo].[JK_VehicleType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JK_VehicleStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JK_VehicleLocation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JK_Vehicle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Make] [nvarchar](50) NOT NULL,
	[Model] [nvarchar](50) NOT NULL,
	[VehicleTypeId] [int] NOT NULL,
PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JK_Inventory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleId] [int] NOT NULL,
	[VehicleLocationId] [int] NOT NULL,
	[VehicleStatusId] [int] NOT NULL,
	[LastUpdated] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED ([Id] ASC)
) ON [PRIMARY]
GO

-- Unique constraints
ALTER TABLE [dbo].[JK_VehicleLocation] ADD UNIQUE NONCLUSTERED ([Name] ASC)
GO
ALTER TABLE [dbo].[JK_VehicleStatus] ADD UNIQUE NONCLUSTERED ([Name] ASC)
GO
ALTER TABLE [dbo].[JK_VehicleType] ADD UNIQUE NONCLUSTERED ([Name] ASC)
GO

-- Default
ALTER TABLE [dbo].[JK_Inventory] ADD DEFAULT (sysdatetime()) FOR [LastUpdated]
GO

-- Foreign keys
ALTER TABLE [dbo].[JK_Vehicle] WITH CHECK ADD CONSTRAINT [FK_JK_Vehicle_JK_VehicleType]
    FOREIGN KEY([VehicleTypeId]) REFERENCES [dbo].[JK_VehicleType] ([Id])
GO
ALTER TABLE [dbo].[JK_Inventory] WITH CHECK ADD CONSTRAINT [FK_JK_Inventory_JK_Vehicle]
    FOREIGN KEY([VehicleId]) REFERENCES [dbo].[JK_Vehicle] ([Id])
GO
ALTER TABLE [dbo].[JK_Inventory] WITH CHECK ADD CONSTRAINT [FK_JK_Inventory_JK_VehicleLocation]
    FOREIGN KEY([VehicleLocationId]) REFERENCES [dbo].[JK_VehicleLocation] ([Id])
GO
ALTER TABLE [dbo].[JK_Inventory] WITH CHECK ADD CONSTRAINT [FK_JK_Inventory_JK_VehicleStatus]
    FOREIGN KEY([VehicleStatusId]) REFERENCES [dbo].[JK_VehicleStatus] ([Id])
GO

-- Seed data
SET IDENTITY_INSERT [dbo].[JK_VehicleType] ON
GO
INSERT [dbo].[JK_VehicleType] ([Id], [Name]) VALUES (1, N'Sedan')
GO
INSERT [dbo].[JK_VehicleType] ([Id], [Name]) VALUES (2, N'SUV')
GO
INSERT [dbo].[JK_VehicleType] ([Id], [Name]) VALUES (3, N'Truck')
GO
INSERT [dbo].[JK_VehicleType] ([Id], [Name]) VALUES (4, N'Van')
GO
SET IDENTITY_INSERT [dbo].[JK_VehicleType] OFF
GO

SET IDENTITY_INSERT [dbo].[JK_VehicleStatus] ON
GO
INSERT [dbo].[JK_VehicleStatus] ([Id], [Name]) VALUES (1, N'Available')
GO
INSERT [dbo].[JK_VehicleStatus] ([Id], [Name]) VALUES (2, N'Reserved')
GO
INSERT [dbo].[JK_VehicleStatus] ([Id], [Name]) VALUES (3, N'Rented')
GO
INSERT [dbo].[JK_VehicleStatus] ([Id], [Name]) VALUES (4, N'Maintenance')
GO
SET IDENTITY_INSERT [dbo].[JK_VehicleStatus] OFF
GO

SET IDENTITY_INSERT [dbo].[JK_VehicleLocation] ON
GO
INSERT [dbo].[JK_VehicleLocation] ([Id], [Name]) VALUES (1, N'Kitchener')
GO
INSERT [dbo].[JK_VehicleLocation] ([Id], [Name]) VALUES (2, N'Waterloo')
GO
INSERT [dbo].[JK_VehicleLocation] ([Id], [Name]) VALUES (3, N'Cambridge')
GO
INSERT [dbo].[JK_VehicleLocation] ([Id], [Name]) VALUES (4, N'Guelph')
GO
SET IDENTITY_INSERT [dbo].[JK_VehicleLocation] OFF
GO

SET IDENTITY_INSERT [dbo].[JK_Vehicle] ON
GO
INSERT [dbo].[JK_Vehicle] ([Id], [Make], [Model], [VehicleTypeId]) VALUES (1, N'Toyota', N'Camry', 1)
GO
INSERT [dbo].[JK_Vehicle] ([Id], [Make], [Model], [VehicleTypeId]) VALUES (2, N'Honda', N'Civic', 1)
GO
INSERT [dbo].[JK_Vehicle] ([Id], [Make], [Model], [VehicleTypeId]) VALUES (3, N'Ford', N'Escape', 2)
GO
INSERT [dbo].[JK_Vehicle] ([Id], [Make], [Model], [VehicleTypeId]) VALUES (4, N'Toyota', N'RAV4', 2)
GO
INSERT [dbo].[JK_Vehicle] ([Id], [Make], [Model], [VehicleTypeId]) VALUES (5, N'Ford', N'F-150', 3)
GO
INSERT [dbo].[JK_Vehicle] ([Id], [Make], [Model], [VehicleTypeId]) VALUES (6, N'Chevy', N'Express', 4)
GO
SET IDENTITY_INSERT [dbo].[JK_Vehicle] OFF
GO

SET IDENTITY_INSERT [dbo].[JK_Inventory] ON
GO
INSERT [dbo].[JK_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (1, 1, 1, 1, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[JK_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (2, 2, 1, 2, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[JK_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (3, 3, 2, 1, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[JK_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (4, 4, 2, 3, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[JK_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (5, 5, 3, 1, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[JK_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (6, 6, 3, 4, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[JK_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (7, 1, 4, 1, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[JK_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (8, 2, 4, 2, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[JK_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (9, 3, 1, 1, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
INSERT [dbo].[JK_Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId], [LastUpdated]) VALUES (10, 4, 2, 3, CAST(N'2026-02-01T18:38:44.5689735' AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[JK_Inventory] OFF
GO