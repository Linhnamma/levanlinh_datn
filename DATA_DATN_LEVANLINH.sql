USE [master]
GO
/****** Object:  Database [HHDaDung]    Script Date: 10/9/2024 6:20:50 AM ******/
CREATE DATABASE [HHDaDung]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HHDaDung', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HHDaDung.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HHDaDung_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\HHDaDung_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [HHDaDung] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HHDaDung].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HHDaDung] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HHDaDung] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HHDaDung] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HHDaDung] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HHDaDung] SET ARITHABORT OFF 
GO
ALTER DATABASE [HHDaDung] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HHDaDung] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HHDaDung] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HHDaDung] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HHDaDung] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HHDaDung] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HHDaDung] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HHDaDung] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HHDaDung] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HHDaDung] SET  ENABLE_BROKER 
GO
ALTER DATABASE [HHDaDung] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HHDaDung] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HHDaDung] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HHDaDung] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HHDaDung] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HHDaDung] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HHDaDung] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HHDaDung] SET RECOVERY FULL 
GO
ALTER DATABASE [HHDaDung] SET  MULTI_USER 
GO
ALTER DATABASE [HHDaDung] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HHDaDung] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HHDaDung] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HHDaDung] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HHDaDung] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HHDaDung] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'HHDaDung', N'ON'
GO
ALTER DATABASE [HHDaDung] SET QUERY_STORE = ON
GO
ALTER DATABASE [HHDaDung] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [HHDaDung]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 10/9/2024 6:20:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[cart_id] [int] IDENTITY(1,1) NOT NULL,
	[users_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
 CONSTRAINT [PK__cart__2EF52A27A74486DC] PRIMARY KEY CLUSTERED 
(
	[cart_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[categories]    Script Date: 10/9/2024 6:20:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categories](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__categori__D54EE9B4C5C95205] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 10/9/2024 6:20:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[order_id] [int] IDENTITY(1,1) NOT NULL,
	[users_id] [int] NULL,
	[order_date] [datetime] NULL,
	[discount] [int] NULL,
	[total_amount] [decimal](18, 2) NULL,
	[product_id] [int] NULL,
	[quantity] [int] NULL,
	[status] [nvarchar](50) NULL,
	[payment_status] [nvarchar](50) NULL,
 CONSTRAINT [PK__orders__4659622973DED1ED] PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 10/9/2024 6:20:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[product_id] [int] IDENTITY(1,1) NOT NULL,
	[product_name] [nvarchar](50) NOT NULL,
	[pic_name] [nvarchar](50) NOT NULL,
	[descriptions] [nvarchar](max) NULL,
	[price] [decimal](18, 2) NOT NULL,
	[quantity] [int] NOT NULL,
	[users_id] [int] NOT NULL,
	[category_id] [int] NOT NULL,
	[subcategory_id] [int] NULL,
 CONSTRAINT [PK__products__47027DF55AACAF42] PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrder]    Script Date: 10/9/2024 6:20:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrder](
	[purchaseOrder_id] [int] IDENTITY(1,1) NOT NULL,
	[purchaseOrder_date] [datetime2](7) NULL,
	[supplier_id] [int] NOT NULL,
	[product_id] [int] NOT NULL,
	[users_id] [int] NOT NULL,
	[quantity] [int] NOT NULL,
	[price] [decimal](18, 2) NOT NULL,
	[totalAmount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY CLUSTERED 
(
	[purchaseOrder_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subcategories]    Script Date: 10/9/2024 6:20:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subcategories](
	[subcategory_id] [int] NOT NULL,
	[subcategory_name] [nvarchar](50) NOT NULL,
	[category_id] [int] NOT NULL,
 CONSTRAINT [PK_subcategories] PRIMARY KEY CLUSTERED 
(
	[subcategory_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 10/9/2024 6:20:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[supplier_id] [int] NOT NULL,
	[supplier_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[supplier_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 10/9/2024 6:20:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[users_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[full_name] [nvarchar](max) NOT NULL,
	[address] [nvarchar](max) NULL,
	[phone] [int] NULL,
	[email] [varchar](255) NULL,
	[role_id] [int] NOT NULL,
	[user_name] [nvarchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
 CONSTRAINT [PK__users__EAA7D14B506D877E] PRIMARY KEY CLUSTERED 
(
	[users_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users_roles]    Script Date: 10/9/2024 6:20:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_roles](
	[role_id] [int] NOT NULL,
	[role_name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__users_ro__760965CC4D770488] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD  CONSTRAINT [FK_cart_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([product_id])
GO
ALTER TABLE [dbo].[cart] CHECK CONSTRAINT [FK_cart_products]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD  CONSTRAINT [fk_users_cart] FOREIGN KEY([users_id])
REFERENCES [dbo].[users] ([users_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[cart] CHECK CONSTRAINT [fk_users_cart]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_orders_products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([product_id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_orders_products]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [fk_users_orders] FOREIGN KEY([users_id])
REFERENCES [dbo].[users] ([users_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [fk_users_orders]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [fk_categories_products] FOREIGN KEY([category_id])
REFERENCES [dbo].[categories] ([category_id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [fk_categories_products]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [FK_products_subcategories] FOREIGN KEY([subcategory_id])
REFERENCES [dbo].[subcategories] ([subcategory_id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [FK_products_subcategories]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [fk_users_products] FOREIGN KEY([users_id])
REFERENCES [dbo].[users] ([users_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [fk_users_products]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_products1] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([product_id])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_products1]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_Supplier] FOREIGN KEY([supplier_id])
REFERENCES [dbo].[Supplier] ([supplier_id])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_Supplier]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_users] FOREIGN KEY([users_id])
REFERENCES [dbo].[users] ([users_id])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_users]
GO
ALTER TABLE [dbo].[subcategories]  WITH CHECK ADD  CONSTRAINT [FK_subcategories_categories] FOREIGN KEY([category_id])
REFERENCES [dbo].[categories] ([category_id])
GO
ALTER TABLE [dbo].[subcategories] CHECK CONSTRAINT [FK_subcategories_categories]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [fk_users_roles] FOREIGN KEY([role_id])
REFERENCES [dbo].[users_roles] ([role_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [fk_users_roles]
GO
USE [master]
GO
ALTER DATABASE [HHDaDung] SET  READ_WRITE 
GO
