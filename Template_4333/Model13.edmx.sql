
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/02/2023 00:33:03
-- Generated from EDMX file: C:\Users\amirg\source\repos\Template\Template_4333\Model13.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [isrpo2];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tableispro2]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tableispro2];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tableispro2'
CREATE TABLE [dbo].[tableispro2] (
    [Айди] nvarchar(max)  NOT NULL,
    [КодЗаказ] nchar(50)  NULL,
    [Датасоздания] nvarchar(max)  NULL,
    [Времязаказ] nvarchar(max)  NULL,
    [АйдиКлиент] nvarchar(max)  NULL,
    [Услуга] nchar(50)  NULL,
    [Статус] nchar(50)  NULL,
    [Датазакрытия] nvarchar(max)  NULL,
    [Времяпроката] nchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Айди] in table 'tableispro2'
ALTER TABLE [dbo].[tableispro2]
ADD CONSTRAINT [PK_tableispro2]
    PRIMARY KEY CLUSTERED ([Айди] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------