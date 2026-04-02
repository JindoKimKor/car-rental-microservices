-- Create CustomerDB if not exists
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CustomerDB')
BEGIN
    CREATE DATABASE CustomerDB;
END
GO

USE CustomerDB;
GO

-- Create Customer table if not exists
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Customer' AND xtype='U')
BEGIN
    CREATE TABLE [Customer] (
        [Id] INT IDENTITY(1,1) NOT NULL,
        [FirstName] NVARCHAR(100) NULL,
        [LastName] NVARCHAR(100) NULL,
        [Phone] NVARCHAR(20) NULL,
        [Email] NVARCHAR(255) NULL,
        CONSTRAINT [PK__Customer__3214EC075A39FF18] PRIMARY KEY ([Id])
    );
END
GO

-- Seed data (only if table is empty)
IF NOT EXISTS (SELECT TOP 1 1 FROM [Customer])
BEGIN
    INSERT INTO [Customer] ([FirstName], [LastName], [Phone], [Email]) VALUES
    ('Freddie', 'Mercury', '555-101-0001', 'freddie.mercury@example.com'),
    ('Elvis', 'Presley', '555-101-0002', 'elvis.presley@example.com'),
    ('Madonna', 'Ciccone', '555-101-0003', 'madonna.ciccone@example.com'),
    ('Prince', 'Nelson', '555-101-0004', 'prince.nelson@example.com'),
    ('Taylor', 'Swift', '555-101-0005', 'taylor.swift@example.com'),
    ('Leonardo', 'DiCaprio', '555-202-0001', 'leonardo.dicaprio@example.com'),
    ('Scarlett', 'Johansson', '555-202-0002', 'scarlett.johansson@example.com');
END
GO
