-- Create Inventory DB if not exists
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'JK_VehicleInventoryDb')
BEGIN
    CREATE DATABASE JK_VehicleInventoryDb;
END
GO

USE JK_VehicleInventoryDb;
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    CREATE TABLE [Vehicle] (
        [Id] int NOT NULL IDENTITY,
        [Make] nvarchar(50) NOT NULL,
        [Model] nvarchar(50) NOT NULL,
        [VehicleTypeId] int NOT NULL,
        CONSTRAINT [PK_Vehicle] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    CREATE TABLE [VehicleLocation] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_VehicleLocation] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    CREATE TABLE [VehicleStatus] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(30) NOT NULL,
        CONSTRAINT [PK_VehicleStatus] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    CREATE TABLE [VehicleType] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(50) NOT NULL,
        CONSTRAINT [PK_VehicleType] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    CREATE TABLE [Inventory] (
        [Id] int NOT NULL IDENTITY,
        [VehicleLocationId] int NOT NULL,
        [VehicleStatusId] int NOT NULL,
        [VehicleId] int NOT NULL,
        CONSTRAINT [PK_Inventory] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Inventory_VehicleLocation_VehicleLocationId] FOREIGN KEY ([VehicleLocationId]) REFERENCES [VehicleLocation] ([Id]),
        CONSTRAINT [FK_Inventory_VehicleStatus_VehicleStatusId] FOREIGN KEY ([VehicleStatusId]) REFERENCES [VehicleStatus] ([Id]),
        CONSTRAINT [FK_Inventory_Vehicle_VehicleId] FOREIGN KEY ([VehicleId]) REFERENCES [Vehicle] ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Make', N'Model', N'VehicleTypeId') AND [object_id] = OBJECT_ID(N'[Vehicle]'))
        SET IDENTITY_INSERT [Vehicle] ON;
    EXEC(N'INSERT INTO [Vehicle] ([Id], [Make], [Model], [VehicleTypeId])
    VALUES (1, N''Toyota'', N''Camry'', 1),
    (2, N''Honda'', N''Civic'', 1),
    (3, N''Ford'', N''Escape'', 2),
    (4, N''Toyota'', N''RAV4'', 2),
    (5, N''Ford'', N''F-150'', 3),
    (6, N''Chevy'', N''Express'', 4)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Make', N'Model', N'VehicleTypeId') AND [object_id] = OBJECT_ID(N'[Vehicle]'))
        SET IDENTITY_INSERT [Vehicle] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[VehicleLocation]'))
        SET IDENTITY_INSERT [VehicleLocation] ON;
    EXEC(N'INSERT INTO [VehicleLocation] ([Id], [Name])
    VALUES (1, N''Kitchener''),
    (2, N''Waterloo''),
    (3, N''Cambridge''),
    (4, N''Guelph'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[VehicleLocation]'))
        SET IDENTITY_INSERT [VehicleLocation] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[VehicleStatus]'))
        SET IDENTITY_INSERT [VehicleStatus] ON;
    EXEC(N'INSERT INTO [VehicleStatus] ([Id], [Name])
    VALUES (1, N''Available''),
    (2, N''Reserved''),
    (3, N''Rented''),
    (4, N''Maintenance'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[VehicleStatus]'))
        SET IDENTITY_INSERT [VehicleStatus] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[VehicleType]'))
        SET IDENTITY_INSERT [VehicleType] ON;
    EXEC(N'INSERT INTO [VehicleType] ([Id], [Name])
    VALUES (1, N''Sedan''),
    (2, N''SUV''),
    (3, N''Truck''),
    (4, N''Van'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name') AND [object_id] = OBJECT_ID(N'[VehicleType]'))
        SET IDENTITY_INSERT [VehicleType] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'VehicleId', N'VehicleLocationId', N'VehicleStatusId') AND [object_id] = OBJECT_ID(N'[Inventory]'))
        SET IDENTITY_INSERT [Inventory] ON;
    EXEC(N'INSERT INTO [Inventory] ([Id], [VehicleId], [VehicleLocationId], [VehicleStatusId])
    VALUES (1, 1, 1, 1),
    (2, 2, 1, 2),
    (3, 3, 2, 1),
    (4, 4, 2, 3),
    (5, 5, 3, 1),
    (6, 6, 3, 4),
    (7, 1, 4, 1),
    (8, 2, 4, 2),
    (9, 3, 1, 1),
    (10, 4, 2, 3)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'VehicleId', N'VehicleLocationId', N'VehicleStatusId') AND [object_id] = OBJECT_ID(N'[Inventory]'))
        SET IDENTITY_INSERT [Inventory] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Inventory_VehicleId] ON [Inventory] ([VehicleId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Inventory_VehicleLocationId] ON [Inventory] ([VehicleLocationId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Inventory_VehicleStatusId] ON [Inventory] ([VehicleStatusId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_VehicleLocation_Name] ON [VehicleLocation] ([Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_VehicleStatus_Name] ON [VehicleStatus] ([Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_VehicleType_Name] ON [VehicleType] ([Name]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260315035149_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260315035149_InitialCreate', N'10.0.0');
END;

COMMIT;
GO

