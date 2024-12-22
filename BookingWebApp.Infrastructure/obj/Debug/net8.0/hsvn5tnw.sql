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
CREATE TABLE [Villas] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [Price] float NOT NULL,
    [Sqft] int NOT NULL,
    [Occupancy] int NOT NULL,
    [ImageUrl] nvarchar(max) NULL,
    [CreateDate] datetime2 NULL,
    [UpdateDate] datetime2 NULL,
    CONSTRAINT [PK_Villas] PRIMARY KEY ([Id])
);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241204082758_addVillaToDb', N'9.0.0');

EXEC sp_rename N'[Villas].[UpdateDate]', N'Update_Date', 'COLUMN';

EXEC sp_rename N'[Villas].[CreateDate]', N'Create_Date', 'COLUMN';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241204083154_ModifyNameInVillaTable', N'9.0.0');

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Create_Date', N'Description', N'ImageUrl', N'Name', N'Occupancy', N'Price', N'Sqft', N'Update_Date') AND [object_id] = OBJECT_ID(N'[Villas]'))
    SET IDENTITY_INSERT [Villas] ON;
INSERT INTO [Villas] ([Id], [Create_Date], [Description], [ImageUrl], [Name], [Occupancy], [Price], [Sqft], [Update_Date])
VALUES (1, NULL, N'Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.', N'https://placehold.co/600x400', N'Royal Villa', 4, 200.0E0, 550, NULL),
(2, NULL, N'Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.', N'https://placehold.co/600x401', N'Premium Pool Villa', 4, 300.0E0, 550, NULL),
(3, NULL, N'Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.', N'https://placehold.co/600x402', N'Luxury Pool Villa', 4, 400.0E0, 750, NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Create_Date', N'Description', N'ImageUrl', N'Name', N'Occupancy', N'Price', N'Sqft', N'Update_Date') AND [object_id] = OBJECT_ID(N'[Villas]'))
    SET IDENTITY_INSERT [Villas] OFF;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241204084008_seedVillaToDb', N'9.0.0');

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Villas]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Villas] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Villas] ALTER COLUMN [Name] nvarchar(20) NOT NULL;

CREATE TABLE [VillaNumbers] (
    [Villa_Number] int NOT NULL,
    [VillaId] int NOT NULL,
    [SpecialDetails] nvarchar(max) NULL,
    CONSTRAINT [PK_VillaNumbers] PRIMARY KEY ([Villa_Number]),
    CONSTRAINT [FK_VillaNumbers_Villas_VillaId] FOREIGN KEY ([VillaId]) REFERENCES [Villas] ([Id]) ON DELETE CASCADE
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Villa_Number', N'SpecialDetails', N'VillaId') AND [object_id] = OBJECT_ID(N'[VillaNumbers]'))
    SET IDENTITY_INSERT [VillaNumbers] ON;
INSERT INTO [VillaNumbers] ([Villa_Number], [SpecialDetails], [VillaId])
VALUES (101, NULL, 1),
(102, NULL, 2),
(103, NULL, 3),
(104, NULL, 1),
(105, NULL, 2);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Villa_Number', N'SpecialDetails', N'VillaId') AND [object_id] = OBJECT_ID(N'[VillaNumbers]'))
    SET IDENTITY_INSERT [VillaNumbers] OFF;

CREATE INDEX [IX_VillaNumbers_VillaId] ON [VillaNumbers] ([VillaId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241209014032_addVillaNumber', N'9.0.0');

CREATE TABLE [Amenities] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NULL,
    [VillaId] int NOT NULL,
    CONSTRAINT [PK_Amenities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Amenities_Villas_VillaId] FOREIGN KEY ([VillaId]) REFERENCES [Villas] ([Id]) ON DELETE CASCADE
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name', N'VillaId') AND [object_id] = OBJECT_ID(N'[Amenities]'))
    SET IDENTITY_INSERT [Amenities] ON;
INSERT INTO [Amenities] ([Id], [Description], [Name], [VillaId])
VALUES (1, NULL, N'Private Pool', 1),
(2, NULL, N'Microwave', 1),
(3, NULL, N'Private Balcony', 1),
(4, NULL, N'1 king bed and 1 sofa bed', 1),
(5, NULL, N'Private Plunge Pool', 2),
(6, NULL, N'Microwave and Mini Refrigerator', 2),
(7, NULL, N'Private Balcony', 2),
(8, NULL, N'king bed or 2 double beds', 2),
(9, NULL, N'Private Pool', 3),
(10, NULL, N'Jacuzzi', 3),
(11, NULL, N'Private Balcony', 3);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Description', N'Name', N'VillaId') AND [object_id] = OBJECT_ID(N'[Amenities]'))
    SET IDENTITY_INSERT [Amenities] OFF;

CREATE INDEX [IX_Amenities_VillaId] ON [Amenities] ([VillaId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241212022945_AmenityMigration', N'9.0.0');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241221061857_inititDb', N'9.0.0');

COMMIT;
GO

