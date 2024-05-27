USE [master]
GO
/****** Object:  Database [u1662870_Zakupki]    Script Date: 23.07.2022 12:41:45 ******/
CREATE DATABASE [u1662870_Zakupki]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'u1662870_Zakupki', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\u1662870_Zakupki.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'u1662870_Zakupki_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\u1662870_Zakupki_log.ldf' , SIZE = 335872KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [u1662870_Zakupki] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [u1662870_Zakupki].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [u1662870_Zakupki] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET ARITHABORT OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [u1662870_Zakupki] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [u1662870_Zakupki] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET  ENABLE_BROKER 
GO
ALTER DATABASE [u1662870_Zakupki] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [u1662870_Zakupki] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET RECOVERY FULL 
GO
ALTER DATABASE [u1662870_Zakupki] SET  MULTI_USER 
GO
ALTER DATABASE [u1662870_Zakupki] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [u1662870_Zakupki] SET DB_CHAINING OFF 
GO
ALTER DATABASE [u1662870_Zakupki] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [u1662870_Zakupki] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [u1662870_Zakupki] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [u1662870_Zakupki] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [u1662870_Zakupki] SET QUERY_STORE = OFF
GO
USE [u1662870_Zakupki]
GO
/****** Object:  User [u1662870_konstantin]    Script Date: 23.07.2022 12:41:45 ******/
CREATE USER [u1662870_konstantin] FOR LOGIN [u1662870_konstantin] WITH DEFAULT_SCHEMA=[u1662870_konstantin]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [u1662870_konstantin]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [u1662870_konstantin]
GO
ALTER ROLE [db_datareader] ADD MEMBER [u1662870_konstantin]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [u1662870_konstantin]
GO
/****** Object:  Schema [u1662870_konstantin]    Script Date: 23.07.2022 12:41:46 ******/
CREATE SCHEMA [u1662870_konstantin]
GO
/****** Object:  View [u1662870_konstantin].[Company_Coeffitient_Edit_View]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [u1662870_konstantin].[Company_Coeffitient_Edit_View]
AS
SELECT TOP (100) PERCENT u1662870_konstantin.Company_Сoefficient_Values.Id, u1662870_konstantin.Company_Сoefficient_Values.Company_Inn, u1662870_konstantin.UserCompany.Company_Name, 
                  u1662870_konstantin.Company_Сoefficient_Values.Coefficient_Id, u1662870_konstantin.Criterion_Сoefficient.Name, u1662870_konstantin.Criterion_Сoefficient.Coefficient_Type, 
                  u1662870_konstantin.Criterion_Сoefficient_Type.Type_Name, u1662870_konstantin.Company_Сoefficient_Values.Value, u1662870_konstantin.Company_Сoefficient_Values.IsActive
FROM     u1662870_konstantin.Company_Сoefficient_Values INNER JOIN
                  u1662870_konstantin.UserCompany ON u1662870_konstantin.Company_Сoefficient_Values.Company_Inn = u1662870_konstantin.UserCompany.Inn INNER JOIN
                  u1662870_konstantin.Criterion_Сoefficient ON u1662870_konstantin.Company_Сoefficient_Values.Coefficient_Id = u1662870_konstantin.Criterion_Сoefficient.Id INNER JOIN
                  u1662870_konstantin.Criterion_Сoefficient_Type ON u1662870_konstantin.Criterion_Сoefficient.Coefficient_Type = u1662870_konstantin.Criterion_Сoefficient_Type.Id
ORDER BY u1662870_konstantin.Company_Сoefficient_Values.Company_Inn, u1662870_konstantin.Criterion_Сoefficient.Name
GO
/****** Object:  View [u1662870_konstantin].[Supplier_Info_View]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [u1662870_konstantin].[Supplier_Info_View]
AS
SELECT u1662870_konstantin.Supplier.Inn, u1662870_konstantin.Supplier.Name, u1662870_konstantin.Supplier.Email, u1662870_konstantin.Supplier.Phone, u1662870_konstantin.Supplier.Region, u1662870_konstantin.Supplier.Contact_Id, 
                  u1662870_konstantin.Contact.Name AS First_Name, u1662870_konstantin.Contact.Surname, u1662870_konstantin.Contact.Patronimic, u1662870_konstantin.Suppler_s_Products.Okpd2, 
                  u1662870_konstantin.Suppler_s_Products.Name AS Product_Name, u1662870_konstantin.Suppler_s_Products.Price, u1662870_konstantin.Suppler_s_Products.Count
FROM     u1662870_konstantin.Supplier INNER JOIN
                  u1662870_konstantin.Suppler_s_Products ON u1662870_konstantin.Supplier.Inn = u1662870_konstantin.Suppler_s_Products.Supplier_Id INNER JOIN
                  u1662870_konstantin.Contact ON u1662870_konstantin.Supplier.Contact_Id = u1662870_konstantin.Contact.Id
GO
/****** Object:  View [u1662870_konstantin].[Supplier_List_View]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [u1662870_konstantin].[Supplier_List_View]
AS
SELECT u1662870_konstantin.[User].Id, u1662870_konstantin.[User].Email, u1662870_konstantin.Suppliers_In_Supplier_List.Supplier_List_Id, u1662870_konstantin.Supplier_List.Name AS List_Name, u1662870_konstantin.Supplier_List.Date, 
                  u1662870_konstantin.Suppliers_In_Supplier_List.Id AS List_Id, u1662870_konstantin.Supplier.Name AS Supplier_Name, u1662870_konstantin.Suppliers_In_Supplier_List.Rank, u1662870_konstantin.Suppliers_In_Supplier_List.Okpd2, 
                  u1662870_konstantin.Suppliers_In_Supplier_List.Conflict, u1662870_konstantin.Suppler_s_Products.Name AS Product_Name, u1662870_konstantin.Suppler_s_Products.Price
FROM     u1662870_konstantin.Supplier INNER JOIN
                  u1662870_konstantin.Suppler_s_Products ON u1662870_konstantin.Supplier.Inn = u1662870_konstantin.Suppler_s_Products.Supplier_Id INNER JOIN
                  u1662870_konstantin.Suppliers_In_Supplier_List ON u1662870_konstantin.Supplier.Inn = u1662870_konstantin.Suppliers_In_Supplier_List.Supplier_Id INNER JOIN
                  u1662870_konstantin.Supplier_List ON u1662870_konstantin.Suppliers_In_Supplier_List.Supplier_List_Id = u1662870_konstantin.Supplier_List.Id INNER JOIN
                  u1662870_konstantin.[User] ON u1662870_konstantin.Supplier_List.User_Id = u1662870_konstantin.[User].Id
GO
/****** Object:  View [u1662870_konstantin].[User_Connections_View]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [u1662870_konstantin].[User_Connections_View]
AS
SELECT u1662870_konstantin.[User].Id, u1662870_konstantin.[User].Email, u1662870_konstantin.[User].Role, u1662870_konstantin.Role.Name AS Role_Name, u1662870_konstantin.[User].Company, 
                  u1662870_konstantin.UserCompany.Company_Name
FROM     u1662870_konstantin.Role INNER JOIN
                  u1662870_konstantin.[User] ON u1662870_konstantin.Role.Id = u1662870_konstantin.[User].Role INNER JOIN
                  u1662870_konstantin.UserCompany ON u1662870_konstantin.[User].Company = u1662870_konstantin.UserCompany.Inn
GO
/****** Object:  Table [u1662870_konstantin].[Company_Сoefficient_Values]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[Company_Сoefficient_Values](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Company_Inn] [char](12) NOT NULL,
	[Coefficient_Id] [uniqueidentifier] NOT NULL,
	[Value] [float] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_User_Criterion_values] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [u1662870_konstantin].[Contact]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[Contact](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Patronimic] [nvarchar](50) NULL,
	[Phone1] [nvarchar](12) NOT NULL,
	[Phone2] [nvarchar](12) NOT NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [u1662870_konstantin].[Criterion_Сoefficient]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[Criterion_Сoefficient](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Coefficient_Type] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Criterion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [u1662870_konstantin].[Criterion_Сoefficient_Type]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[Criterion_Сoefficient_Type](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Type_Name] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Criterion_Type] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [u1662870_konstantin].[Okpd2]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[Okpd2](
	[Code] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Parent] [nvarchar](50) NULL,
 CONSTRAINT [PK_Okpd2] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [u1662870_konstantin].[Role]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[Role](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [u1662870_konstantin].[Suppler_s_Products]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[Suppler_s_Products](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Okpd2] [nvarchar](50) NOT NULL,
	[Supplier_Id] [char](12) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Price] [decimal](19, 2) NOT NULL,
	[Count] [int] NOT NULL,
 CONSTRAINT [PK_Suppler_s_Okpd] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [u1662870_konstantin].[Supplier]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[Supplier](
	[Inn] [char](12) NOT NULL,
	[Name] [nvarchar](300) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Phone] [nvarchar](50) NULL,
	[Contact_Id] [uniqueidentifier] NOT NULL,
	[Region] [nvarchar](100) NOT NULL,
	[Kpp] [nvarchar](50) NULL,
	[Ogrn] [nvarchar](15) NULL,
	[Reputation] [float] NOT NULL,
	[Work_Since] [datetime] NOT NULL,
	[Dishonesty] [bit] NULL,
	[Bankruptcy_Or_Liquidation] [bit] NOT NULL,
	[Way_Of_Distribution] [bit] NOT NULL,
	[Small_Business_Entity] [bit] NOT NULL,
	[Is_Manufacturer] [bit] NOT NULL,
	[Minimum_Delivery_Days] [int] NOT NULL,
	[Conflict] [char](12) NOT NULL,
	[OverallContracts] [int] NOT NULL,
	[succededContracts] [int] NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[Inn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [u1662870_konstantin].[Supplier_List]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[Supplier_List](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[User_Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_Supplier_List] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [u1662870_konstantin].[Supplier_List_Critetions]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[Supplier_List_Critetions](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Supplier_List_Id] [uniqueidentifier] NOT NULL,
	[Cretition_Id] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Supplier_List_Critetions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [u1662870_konstantin].[Suppliers_In_Supplier_List]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[Suppliers_In_Supplier_List](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Supplier_Id] [char](12) NOT NULL,
	[Supplier_List_Id] [uniqueidentifier] NULL,
	[Rank] [float] NULL,
	[Conflict] [bit] NULL,
	[ProductId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Suppliers_In_Supplier_List] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [u1662870_konstantin].[User]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[User](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [binary](16) NOT NULL,
	[Name] [nvarchar](30) NULL,
	[Surname] [nvarchar](30) NULL,
	[Patronimic] [nvarchar](30) NULL,
	[Phone] [char](12) NULL,
	[Role] [uniqueidentifier] NOT NULL,
	[Company] [char](12) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [u1662870_konstantin].[User_Favourite_Suppliers]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[User_Favourite_Suppliers](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[User_Id] [uniqueidentifier] NOT NULL,
	[Supplier_Id] [char](12) NOT NULL,
 CONSTRAINT [PK_User_Favourite_Suppliers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [u1662870_konstantin].[UserCompany]    Script Date: 23.07.2022 12:41:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [u1662870_konstantin].[UserCompany](
	[Inn] [char](12) NOT NULL,
	[Company_Name] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_UserCompany] PRIMARY KEY CLUSTERED 
(
	[Inn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [u1662870_konstantin].[Company_Сoefficient_Values] ADD  CONSTRAINT [DF_Company_Сoefficient_Values_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [u1662870_konstantin].[Company_Сoefficient_Values] ADD  CONSTRAINT [DF_Company_Coefficient_Values_Value]  DEFAULT ((1)) FOR [Value]
GO
ALTER TABLE [u1662870_konstantin].[Company_Сoefficient_Values] ADD  CONSTRAINT [DF_Company_Coefficient_Values_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [u1662870_konstantin].[Contact] ADD  CONSTRAINT [DF_Contact_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [u1662870_konstantin].[Criterion_Сoefficient] ADD  CONSTRAINT [DF_Criteria_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [u1662870_konstantin].[Criterion_Сoefficient_Type] ADD  CONSTRAINT [DF_Criterion_Type_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [u1662870_konstantin].[Role] ADD  CONSTRAINT [DF_Role_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [u1662870_konstantin].[Suppler_s_Products] ADD  CONSTRAINT [DF_Suppler_s_Okpd_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [u1662870_konstantin].[Suppler_s_Products] ADD  CONSTRAINT [DF_Suppler_s_Products_Price_1]  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [u1662870_konstantin].[Suppler_s_Products] ADD  CONSTRAINT [DF_Suppler_s_Products_Count_1]  DEFAULT ((0)) FOR [Count]
GO
ALTER TABLE [u1662870_konstantin].[Supplier] ADD  CONSTRAINT [DF_Supplier_Id]  DEFAULT (newid()) FOR [Inn]
GO
ALTER TABLE [u1662870_konstantin].[Supplier] ADD  CONSTRAINT [DF_Supplier_OverallContracts]  DEFAULT ((0)) FOR [OverallContracts]
GO
ALTER TABLE [u1662870_konstantin].[Supplier] ADD  CONSTRAINT [DF_Supplier_succededContracts]  DEFAULT ((0)) FOR [succededContracts]
GO
ALTER TABLE [u1662870_konstantin].[Supplier_List] ADD  CONSTRAINT [DF_Supplier_List_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [u1662870_konstantin].[Supplier_List] ADD  CONSTRAINT [DF_Supplier_List_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [u1662870_konstantin].[Supplier_List_Critetions] ADD  CONSTRAINT [DF_Supplier_List_Critetions_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [u1662870_konstantin].[Suppliers_In_Supplier_List] ADD  CONSTRAINT [DF_Suppliers_In_Supplier_List_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [u1662870_konstantin].[Suppliers_In_Supplier_List] ADD  CONSTRAINT [DF_Suppliers_In_Supplier_List_Rank]  DEFAULT ((0)) FOR [Rank]
GO
ALTER TABLE [u1662870_konstantin].[User] ADD  CONSTRAINT [DF_User_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [u1662870_konstantin].[User] ADD  CONSTRAINT [DF_User_Password]  DEFAULT (0x21232F297A57A5A743894A0E4A801FC3) FOR [Password]
GO
ALTER TABLE [u1662870_konstantin].[User_Favourite_Suppliers] ADD  CONSTRAINT [DF_User_Favourite_Suppliers_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [u1662870_konstantin].[Company_Сoefficient_Values]  WITH NOCHECK ADD  CONSTRAINT [FK_Company_Criterion_values_Company] FOREIGN KEY([Company_Inn])
REFERENCES [u1662870_konstantin].[UserCompany] ([Inn])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [u1662870_konstantin].[Company_Сoefficient_Values] NOCHECK CONSTRAINT [FK_Company_Criterion_values_Company]
GO
ALTER TABLE [u1662870_konstantin].[Company_Сoefficient_Values]  WITH CHECK ADD  CONSTRAINT [FK_Company_Criterion_values_Criterion] FOREIGN KEY([Coefficient_Id])
REFERENCES [u1662870_konstantin].[Criterion_Сoefficient] ([Id])
GO
ALTER TABLE [u1662870_konstantin].[Company_Сoefficient_Values] CHECK CONSTRAINT [FK_Company_Criterion_values_Criterion]
GO
ALTER TABLE [u1662870_konstantin].[Criterion_Сoefficient]  WITH CHECK ADD  CONSTRAINT [FK_Criterion_Criterion_Type] FOREIGN KEY([Coefficient_Type])
REFERENCES [u1662870_konstantin].[Criterion_Сoefficient_Type] ([Id])
GO
ALTER TABLE [u1662870_konstantin].[Criterion_Сoefficient] CHECK CONSTRAINT [FK_Criterion_Criterion_Type]
GO
ALTER TABLE [u1662870_konstantin].[Okpd2]  WITH NOCHECK ADD  CONSTRAINT [FK_Okpd2_Okpd2] FOREIGN KEY([Parent])
REFERENCES [u1662870_konstantin].[Okpd2] ([Code])
GO
ALTER TABLE [u1662870_konstantin].[Okpd2] NOCHECK CONSTRAINT [FK_Okpd2_Okpd2]
GO
ALTER TABLE [u1662870_konstantin].[Suppler_s_Products]  WITH NOCHECK ADD  CONSTRAINT [FK_Suppler_s_Okpd_Okpd2] FOREIGN KEY([Okpd2])
REFERENCES [u1662870_konstantin].[Okpd2] ([Code])
ON UPDATE CASCADE
GO
ALTER TABLE [u1662870_konstantin].[Suppler_s_Products] NOCHECK CONSTRAINT [FK_Suppler_s_Okpd_Okpd2]
GO
ALTER TABLE [u1662870_konstantin].[Suppler_s_Products]  WITH CHECK ADD  CONSTRAINT [FK_Suppler_s_Okpd_Supplier] FOREIGN KEY([Supplier_Id])
REFERENCES [u1662870_konstantin].[Supplier] ([Inn])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [u1662870_konstantin].[Suppler_s_Products] CHECK CONSTRAINT [FK_Suppler_s_Okpd_Supplier]
GO
ALTER TABLE [u1662870_konstantin].[Supplier_List]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_List_User] FOREIGN KEY([User_Id])
REFERENCES [u1662870_konstantin].[User] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [u1662870_konstantin].[Supplier_List] CHECK CONSTRAINT [FK_Supplier_List_User]
GO
ALTER TABLE [u1662870_konstantin].[Supplier_List_Critetions]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_List_Critetions_Criterion] FOREIGN KEY([Cretition_Id])
REFERENCES [u1662870_konstantin].[Criterion_Сoefficient] ([Id])
GO
ALTER TABLE [u1662870_konstantin].[Supplier_List_Critetions] CHECK CONSTRAINT [FK_Supplier_List_Critetions_Criterion]
GO
ALTER TABLE [u1662870_konstantin].[Supplier_List_Critetions]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_List_Critetions_Supplier_List] FOREIGN KEY([Supplier_List_Id])
REFERENCES [u1662870_konstantin].[Supplier_List] ([Id])
GO
ALTER TABLE [u1662870_konstantin].[Supplier_List_Critetions] CHECK CONSTRAINT [FK_Supplier_List_Critetions_Supplier_List]
GO
ALTER TABLE [u1662870_konstantin].[Suppliers_In_Supplier_List]  WITH NOCHECK ADD  CONSTRAINT [FK_Suppliers_In_Supplier_List_Suppler_s_Products] FOREIGN KEY([ProductId])
REFERENCES [u1662870_konstantin].[Suppler_s_Products] ([Id])
GO
ALTER TABLE [u1662870_konstantin].[Suppliers_In_Supplier_List] NOCHECK CONSTRAINT [FK_Suppliers_In_Supplier_List_Suppler_s_Products]
GO
ALTER TABLE [u1662870_konstantin].[Suppliers_In_Supplier_List]  WITH CHECK ADD  CONSTRAINT [FK_Suppliers_In_Supplier_List_Supplier] FOREIGN KEY([Supplier_Id])
REFERENCES [u1662870_konstantin].[Supplier] ([Inn])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [u1662870_konstantin].[Suppliers_In_Supplier_List] CHECK CONSTRAINT [FK_Suppliers_In_Supplier_List_Supplier]
GO
ALTER TABLE [u1662870_konstantin].[Suppliers_In_Supplier_List]  WITH CHECK ADD  CONSTRAINT [FK_Suppliers_In_Supplier_List_Supplier_List] FOREIGN KEY([Supplier_List_Id])
REFERENCES [u1662870_konstantin].[Supplier_List] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [u1662870_konstantin].[Suppliers_In_Supplier_List] CHECK CONSTRAINT [FK_Suppliers_In_Supplier_List_Supplier_List]
GO
ALTER TABLE [u1662870_konstantin].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([Role])
REFERENCES [u1662870_konstantin].[Role] ([Id])
GO
ALTER TABLE [u1662870_konstantin].[User] CHECK CONSTRAINT [FK_User_Role]
GO
ALTER TABLE [u1662870_konstantin].[User]  WITH NOCHECK ADD  CONSTRAINT [FK_User_UserCompany] FOREIGN KEY([Company])
REFERENCES [u1662870_konstantin].[UserCompany] ([Inn])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [u1662870_konstantin].[User] NOCHECK CONSTRAINT [FK_User_UserCompany]
GO
ALTER TABLE [u1662870_konstantin].[User_Favourite_Suppliers]  WITH CHECK ADD  CONSTRAINT [FK_User_Favourite_Suppliers_Supplier] FOREIGN KEY([Supplier_Id])
REFERENCES [u1662870_konstantin].[Supplier] ([Inn])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [u1662870_konstantin].[User_Favourite_Suppliers] CHECK CONSTRAINT [FK_User_Favourite_Suppliers_Supplier]
GO
ALTER TABLE [u1662870_konstantin].[User_Favourite_Suppliers]  WITH CHECK ADD  CONSTRAINT [FK_User_Favourite_Suppliers_User] FOREIGN KEY([User_Id])
REFERENCES [u1662870_konstantin].[User] ([Id])
GO
ALTER TABLE [u1662870_konstantin].[User_Favourite_Suppliers] CHECK CONSTRAINT [FK_User_Favourite_Suppliers_User]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "UserCompany"
            Begin Extent = 
               Top = 0
               Left = 293
               Bottom = 141
               Right = 497
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Criterion_Сoefficient"
            Begin Extent = 
               Top = 142
               Left = 293
               Bottom = 283
               Right = 498
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Criterion_Сoefficient_Type"
            Begin Extent = 
               Top = 178
               Left = 545
               Bottom = 297
               Right = 739
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Company_Сoefficient_Values"
            Begin Extent = 
               Top = 7
               Left = 46
               Bottom = 197
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1356
         Filter = 1356
         Or = 1068
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'u1662870_konstantin', @level1type=N'VIEW',@level1name=N'Company_Coeffitient_Edit_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'u1662870_konstantin', @level1type=N'VIEW',@level1name=N'Company_Coeffitient_Edit_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Supplier"
            Begin Extent = 
               Top = 9
               Left = 282
               Bottom = 172
               Right = 553
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Suppler_s_Products"
            Begin Extent = 
               Top = 0
               Left = 605
               Bottom = 163
               Right = 799
            End
            DisplayFlags = 280
            TopColumn = 2
         End
         Begin Table = "Contact"
            Begin Extent = 
               Top = 9
               Left = 34
               Bottom = 172
               Right = 228
            End
            DisplayFlags = 280
            TopColumn = 2
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'u1662870_konstantin', @level1type=N'VIEW',@level1name=N'Supplier_Info_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'u1662870_konstantin', @level1type=N'VIEW',@level1name=N'Supplier_Info_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Supplier"
            Begin Extent = 
               Top = 0
               Left = 229
               Bottom = 163
               Right = 500
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Suppler_s_Products"
            Begin Extent = 
               Top = 3
               Left = 2
               Bottom = 166
               Right = 196
            End
            DisplayFlags = 280
            TopColumn = 1
         End
         Begin Table = "Suppliers_In_Supplier_List"
            Begin Extent = 
               Top = 0
               Left = 546
               Bottom = 163
               Right = 795
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Supplier_List"
            Begin Extent = 
               Top = 0
               Left = 837
               Bottom = 163
               Right = 1031
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 168
               Left = 48
               Bottom = 331
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
       ' , @level0type=N'SCHEMA',@level0name=N'u1662870_konstantin', @level1type=N'VIEW',@level1name=N'Supplier_List_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'  Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'u1662870_konstantin', @level1type=N'VIEW',@level1name=N'Supplier_List_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'u1662870_konstantin', @level1type=N'VIEW',@level1name=N'Supplier_List_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Role"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 126
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "User"
            Begin Extent = 
               Top = 126
               Left = 48
               Bottom = 289
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "UserCompany"
            Begin Extent = 
               Top = 7
               Left = 532
               Bottom = 148
               Right = 736
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1176
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1356
         SortOrder = 1416
         GroupBy = 1350
         Filter = 1356
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'u1662870_konstantin', @level1type=N'VIEW',@level1name=N'User_Connections_View'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'u1662870_konstantin', @level1type=N'VIEW',@level1name=N'User_Connections_View'
GO
USE [master]
GO
ALTER DATABASE [u1662870_Zakupki] SET  READ_WRITE 
GO
