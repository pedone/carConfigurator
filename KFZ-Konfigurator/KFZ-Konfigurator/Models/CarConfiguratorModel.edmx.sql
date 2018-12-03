
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/03/2018 17:44:13
-- Generated from EDMX file: D:\Documents\Projects\CC-KFZ-Generator\git\KFZ-Konfigurator\KFZ-Konfigurator\Models\CarConfiguratorModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_EngineEngineSettings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EngineSettings] DROP CONSTRAINT [FK_EngineEngineSettings];
GO
IF OBJECT_ID(N'[dbo].[FK_CarModelEngineSettings]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EngineSettings] DROP CONSTRAINT [FK_CarModelEngineSettings];
GO
IF OBJECT_ID(N'[dbo].[FK_PaintConfiguration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Configurations] DROP CONSTRAINT [FK_PaintConfiguration];
GO
IF OBJECT_ID(N'[dbo].[FK_EngineSettingsConfiguration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Configurations] DROP CONSTRAINT [FK_EngineSettingsConfiguration];
GO
IF OBJECT_ID(N'[dbo].[FK_ConfigurationAccessory_Configuration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConfigurationAccessory] DROP CONSTRAINT [FK_ConfigurationAccessory_Configuration];
GO
IF OBJECT_ID(N'[dbo].[FK_ConfigurationAccessory_Accessory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConfigurationAccessory] DROP CONSTRAINT [FK_ConfigurationAccessory_Accessory];
GO
IF OBJECT_ID(N'[dbo].[FK_RimConfiguration]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Configurations] DROP CONSTRAINT [FK_RimConfiguration];
GO
IF OBJECT_ID(N'[dbo].[FK_ConfigurationOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Configurations] DROP CONSTRAINT [FK_ConfigurationOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_EngineKindEngine]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Engines] DROP CONSTRAINT [FK_EngineKindEngine];
GO
IF OBJECT_ID(N'[dbo].[FK_BodyKindCarModel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarModels] DROP CONSTRAINT [FK_BodyKindCarModel];
GO
IF OBJECT_ID(N'[dbo].[FK_PaintKindPaint]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Paints] DROP CONSTRAINT [FK_PaintKindPaint];
GO
IF OBJECT_ID(N'[dbo].[FK_AccessoryKindAccessory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Accessories] DROP CONSTRAINT [FK_AccessoryKindAccessory];
GO
IF OBJECT_ID(N'[dbo].[FK_CarSeriesCarModel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CarModels] DROP CONSTRAINT [FK_CarSeriesCarModel];
GO
IF OBJECT_ID(N'[dbo].[FK_FuelCategoryEngineCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories_EngineCategory] DROP CONSTRAINT [FK_FuelCategoryEngineCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_EngineCategory_inherits_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories_EngineCategory] DROP CONSTRAINT [FK_EngineCategory_inherits_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_BodyCategory_inherits_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories_BodyCategory] DROP CONSTRAINT [FK_BodyCategory_inherits_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_PaintCategory_inherits_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories_PaintCategory] DROP CONSTRAINT [FK_PaintCategory_inherits_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_AccessoryCategory_inherits_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories_AccessoryCategory] DROP CONSTRAINT [FK_AccessoryCategory_inherits_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_CarSeriesCategory_inherits_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories_CarSeriesCategory] DROP CONSTRAINT [FK_CarSeriesCategory_inherits_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_FuelCategory_inherits_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories_FuelCategory] DROP CONSTRAINT [FK_FuelCategory_inherits_Category];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accessories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accessories];
GO
IF OBJECT_ID(N'[dbo].[EngineSettings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EngineSettings];
GO
IF OBJECT_ID(N'[dbo].[Paints]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Paints];
GO
IF OBJECT_ID(N'[dbo].[Rims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rims];
GO
IF OBJECT_ID(N'[dbo].[CarModels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CarModels];
GO
IF OBJECT_ID(N'[dbo].[Engines]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Engines];
GO
IF OBJECT_ID(N'[dbo].[Configurations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Configurations];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[Categories_EngineCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories_EngineCategory];
GO
IF OBJECT_ID(N'[dbo].[Categories_BodyCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories_BodyCategory];
GO
IF OBJECT_ID(N'[dbo].[Categories_PaintCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories_PaintCategory];
GO
IF OBJECT_ID(N'[dbo].[Categories_AccessoryCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories_AccessoryCategory];
GO
IF OBJECT_ID(N'[dbo].[Categories_CarSeriesCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories_CarSeriesCategory];
GO
IF OBJECT_ID(N'[dbo].[Categories_FuelCategory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories_FuelCategory];
GO
IF OBJECT_ID(N'[dbo].[ConfigurationAccessory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConfigurationAccessory];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accessories'
CREATE TABLE [dbo].[Accessories] (
    [Id] int  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [Category_Id] int  NOT NULL
);
GO

-- Creating table 'EngineSettings'
CREATE TABLE [dbo].[EngineSettings] (
    [Id] int  NOT NULL,
    [Acceleration] float  NOT NULL,
    [Consumption] float  NOT NULL,
    [Price] float  NOT NULL,
    [TopSpeed] int  NOT NULL,
    [Emission] int  NOT NULL,
    [Gears] int  NOT NULL,
    [WheelDrive] int  NOT NULL,
    [Engine_Id] int  NOT NULL,
    [CarModel_Id] int  NOT NULL
);
GO

-- Creating table 'Paints'
CREATE TABLE [dbo].[Paints] (
    [Id] int  NOT NULL,
    [Color] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [IsDefault] bit  NULL,
    [Category_Id] int  NOT NULL
);
GO

-- Creating table 'Rims'
CREATE TABLE [dbo].[Rims] (
    [Id] int  NOT NULL,
    [Price] float  NOT NULL,
    [Size] int  NOT NULL,
    [IsDefault] bit  NULL
);
GO

-- Creating table 'CarModels'
CREATE TABLE [dbo].[CarModels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Year] int  NOT NULL,
    [BodyCategory_Id] int  NOT NULL,
    [SeriesCategory_Id] int  NOT NULL
);
GO

-- Creating table 'Engines'
CREATE TABLE [dbo].[Engines] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Liter] int  NOT NULL,
    [Size] int  NOT NULL,
    [Performance] int  NOT NULL,
    [Category_Id] int  NOT NULL
);
GO

-- Creating table 'Configurations'
CREATE TABLE [dbo].[Configurations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Paint_Id] int  NOT NULL,
    [EngineSetting_Id] int  NOT NULL,
    [Rims_Id] int  NOT NULL,
    [ConfigurationOrder_Configuration_Id] int  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Description] nvarchar(max)  NULL,
    [Guid] nvarchar(max)  NOT NULL,
    [ExtrasPrice] float  NOT NULL,
    [BasePrice] float  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Categories_EngineCategory'
CREATE TABLE [dbo].[Categories_EngineCategory] (
    [Id] int  NOT NULL,
    [FuelCategory_Id] int  NOT NULL
);
GO

-- Creating table 'Categories_BodyCategory'
CREATE TABLE [dbo].[Categories_BodyCategory] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'Categories_PaintCategory'
CREATE TABLE [dbo].[Categories_PaintCategory] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'Categories_AccessoryCategory'
CREATE TABLE [dbo].[Categories_AccessoryCategory] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'Categories_CarSeriesCategory'
CREATE TABLE [dbo].[Categories_CarSeriesCategory] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'Categories_FuelCategory'
CREATE TABLE [dbo].[Categories_FuelCategory] (
    [Id] int  NOT NULL
);
GO

-- Creating table 'ConfigurationAccessory'
CREATE TABLE [dbo].[ConfigurationAccessory] (
    [ConfigurationAccessory_Accessory_Id] int  NOT NULL,
    [Accessories_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Accessories'
ALTER TABLE [dbo].[Accessories]
ADD CONSTRAINT [PK_Accessories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EngineSettings'
ALTER TABLE [dbo].[EngineSettings]
ADD CONSTRAINT [PK_EngineSettings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Paints'
ALTER TABLE [dbo].[Paints]
ADD CONSTRAINT [PK_Paints]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Rims'
ALTER TABLE [dbo].[Rims]
ADD CONSTRAINT [PK_Rims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CarModels'
ALTER TABLE [dbo].[CarModels]
ADD CONSTRAINT [PK_CarModels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Engines'
ALTER TABLE [dbo].[Engines]
ADD CONSTRAINT [PK_Engines]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Configurations'
ALTER TABLE [dbo].[Configurations]
ADD CONSTRAINT [PK_Configurations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories_EngineCategory'
ALTER TABLE [dbo].[Categories_EngineCategory]
ADD CONSTRAINT [PK_Categories_EngineCategory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories_BodyCategory'
ALTER TABLE [dbo].[Categories_BodyCategory]
ADD CONSTRAINT [PK_Categories_BodyCategory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories_PaintCategory'
ALTER TABLE [dbo].[Categories_PaintCategory]
ADD CONSTRAINT [PK_Categories_PaintCategory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories_AccessoryCategory'
ALTER TABLE [dbo].[Categories_AccessoryCategory]
ADD CONSTRAINT [PK_Categories_AccessoryCategory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories_CarSeriesCategory'
ALTER TABLE [dbo].[Categories_CarSeriesCategory]
ADD CONSTRAINT [PK_Categories_CarSeriesCategory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories_FuelCategory'
ALTER TABLE [dbo].[Categories_FuelCategory]
ADD CONSTRAINT [PK_Categories_FuelCategory]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ConfigurationAccessory_Accessory_Id], [Accessories_Id] in table 'ConfigurationAccessory'
ALTER TABLE [dbo].[ConfigurationAccessory]
ADD CONSTRAINT [PK_ConfigurationAccessory]
    PRIMARY KEY CLUSTERED ([ConfigurationAccessory_Accessory_Id], [Accessories_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Engine_Id] in table 'EngineSettings'
ALTER TABLE [dbo].[EngineSettings]
ADD CONSTRAINT [FK_EngineEngineSettings]
    FOREIGN KEY ([Engine_Id])
    REFERENCES [dbo].[Engines]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EngineEngineSettings'
CREATE INDEX [IX_FK_EngineEngineSettings]
ON [dbo].[EngineSettings]
    ([Engine_Id]);
GO

-- Creating foreign key on [CarModel_Id] in table 'EngineSettings'
ALTER TABLE [dbo].[EngineSettings]
ADD CONSTRAINT [FK_CarModelEngineSettings]
    FOREIGN KEY ([CarModel_Id])
    REFERENCES [dbo].[CarModels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarModelEngineSettings'
CREATE INDEX [IX_FK_CarModelEngineSettings]
ON [dbo].[EngineSettings]
    ([CarModel_Id]);
GO

-- Creating foreign key on [Paint_Id] in table 'Configurations'
ALTER TABLE [dbo].[Configurations]
ADD CONSTRAINT [FK_PaintConfiguration]
    FOREIGN KEY ([Paint_Id])
    REFERENCES [dbo].[Paints]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaintConfiguration'
CREATE INDEX [IX_FK_PaintConfiguration]
ON [dbo].[Configurations]
    ([Paint_Id]);
GO

-- Creating foreign key on [EngineSetting_Id] in table 'Configurations'
ALTER TABLE [dbo].[Configurations]
ADD CONSTRAINT [FK_EngineSettingsConfiguration]
    FOREIGN KEY ([EngineSetting_Id])
    REFERENCES [dbo].[EngineSettings]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EngineSettingsConfiguration'
CREATE INDEX [IX_FK_EngineSettingsConfiguration]
ON [dbo].[Configurations]
    ([EngineSetting_Id]);
GO

-- Creating foreign key on [ConfigurationAccessory_Accessory_Id] in table 'ConfigurationAccessory'
ALTER TABLE [dbo].[ConfigurationAccessory]
ADD CONSTRAINT [FK_ConfigurationAccessory_Configuration]
    FOREIGN KEY ([ConfigurationAccessory_Accessory_Id])
    REFERENCES [dbo].[Configurations]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Accessories_Id] in table 'ConfigurationAccessory'
ALTER TABLE [dbo].[ConfigurationAccessory]
ADD CONSTRAINT [FK_ConfigurationAccessory_Accessory]
    FOREIGN KEY ([Accessories_Id])
    REFERENCES [dbo].[Accessories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConfigurationAccessory_Accessory'
CREATE INDEX [IX_FK_ConfigurationAccessory_Accessory]
ON [dbo].[ConfigurationAccessory]
    ([Accessories_Id]);
GO

-- Creating foreign key on [Rims_Id] in table 'Configurations'
ALTER TABLE [dbo].[Configurations]
ADD CONSTRAINT [FK_RimConfiguration]
    FOREIGN KEY ([Rims_Id])
    REFERENCES [dbo].[Rims]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RimConfiguration'
CREATE INDEX [IX_FK_RimConfiguration]
ON [dbo].[Configurations]
    ([Rims_Id]);
GO

-- Creating foreign key on [ConfigurationOrder_Configuration_Id] in table 'Configurations'
ALTER TABLE [dbo].[Configurations]
ADD CONSTRAINT [FK_ConfigurationOrder]
    FOREIGN KEY ([ConfigurationOrder_Configuration_Id])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConfigurationOrder'
CREATE INDEX [IX_FK_ConfigurationOrder]
ON [dbo].[Configurations]
    ([ConfigurationOrder_Configuration_Id]);
GO

-- Creating foreign key on [Category_Id] in table 'Engines'
ALTER TABLE [dbo].[Engines]
ADD CONSTRAINT [FK_EngineKindEngine]
    FOREIGN KEY ([Category_Id])
    REFERENCES [dbo].[Categories_EngineCategory]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EngineKindEngine'
CREATE INDEX [IX_FK_EngineKindEngine]
ON [dbo].[Engines]
    ([Category_Id]);
GO

-- Creating foreign key on [BodyCategory_Id] in table 'CarModels'
ALTER TABLE [dbo].[CarModels]
ADD CONSTRAINT [FK_BodyKindCarModel]
    FOREIGN KEY ([BodyCategory_Id])
    REFERENCES [dbo].[Categories_BodyCategory]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BodyKindCarModel'
CREATE INDEX [IX_FK_BodyKindCarModel]
ON [dbo].[CarModels]
    ([BodyCategory_Id]);
GO

-- Creating foreign key on [Category_Id] in table 'Paints'
ALTER TABLE [dbo].[Paints]
ADD CONSTRAINT [FK_PaintKindPaint]
    FOREIGN KEY ([Category_Id])
    REFERENCES [dbo].[Categories_PaintCategory]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PaintKindPaint'
CREATE INDEX [IX_FK_PaintKindPaint]
ON [dbo].[Paints]
    ([Category_Id]);
GO

-- Creating foreign key on [Category_Id] in table 'Accessories'
ALTER TABLE [dbo].[Accessories]
ADD CONSTRAINT [FK_AccessoryKindAccessory]
    FOREIGN KEY ([Category_Id])
    REFERENCES [dbo].[Categories_AccessoryCategory]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AccessoryKindAccessory'
CREATE INDEX [IX_FK_AccessoryKindAccessory]
ON [dbo].[Accessories]
    ([Category_Id]);
GO

-- Creating foreign key on [SeriesCategory_Id] in table 'CarModels'
ALTER TABLE [dbo].[CarModels]
ADD CONSTRAINT [FK_CarSeriesCarModel]
    FOREIGN KEY ([SeriesCategory_Id])
    REFERENCES [dbo].[Categories_CarSeriesCategory]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CarSeriesCarModel'
CREATE INDEX [IX_FK_CarSeriesCarModel]
ON [dbo].[CarModels]
    ([SeriesCategory_Id]);
GO

-- Creating foreign key on [FuelCategory_Id] in table 'Categories_EngineCategory'
ALTER TABLE [dbo].[Categories_EngineCategory]
ADD CONSTRAINT [FK_FuelCategoryEngineCategory]
    FOREIGN KEY ([FuelCategory_Id])
    REFERENCES [dbo].[Categories_FuelCategory]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FuelCategoryEngineCategory'
CREATE INDEX [IX_FK_FuelCategoryEngineCategory]
ON [dbo].[Categories_EngineCategory]
    ([FuelCategory_Id]);
GO

-- Creating foreign key on [Id] in table 'Categories_EngineCategory'
ALTER TABLE [dbo].[Categories_EngineCategory]
ADD CONSTRAINT [FK_EngineCategory_inherits_Category]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Categories_BodyCategory'
ALTER TABLE [dbo].[Categories_BodyCategory]
ADD CONSTRAINT [FK_BodyCategory_inherits_Category]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Categories_PaintCategory'
ALTER TABLE [dbo].[Categories_PaintCategory]
ADD CONSTRAINT [FK_PaintCategory_inherits_Category]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Categories_AccessoryCategory'
ALTER TABLE [dbo].[Categories_AccessoryCategory]
ADD CONSTRAINT [FK_AccessoryCategory_inherits_Category]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Categories_CarSeriesCategory'
ALTER TABLE [dbo].[Categories_CarSeriesCategory]
ADD CONSTRAINT [FK_CarSeriesCategory_inherits_Category]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'Categories_FuelCategory'
ALTER TABLE [dbo].[Categories_FuelCategory]
ADD CONSTRAINT [FK_FuelCategory_inherits_Category]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------