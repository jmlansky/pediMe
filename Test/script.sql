/****** Object:  Database [DeliveryOnline]    Script Date: 8/4/2019 3:01:49 PM ******/
CREATE DATABASE [DeliveryOnline]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DeliveryOnline', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\DeliveryOnline.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DeliveryOnline_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\DeliveryOnline_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DeliveryOnline].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DeliveryOnline] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DeliveryOnline] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DeliveryOnline] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DeliveryOnline] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DeliveryOnline] SET ARITHABORT OFF 
GO
ALTER DATABASE [DeliveryOnline] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DeliveryOnline] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DeliveryOnline] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DeliveryOnline] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DeliveryOnline] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DeliveryOnline] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DeliveryOnline] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DeliveryOnline] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DeliveryOnline] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DeliveryOnline] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DeliveryOnline] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DeliveryOnline] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DeliveryOnline] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DeliveryOnline] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DeliveryOnline] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DeliveryOnline] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DeliveryOnline] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DeliveryOnline] SET RECOVERY FULL 
GO
ALTER DATABASE [DeliveryOnline] SET  MULTI_USER 
GO
ALTER DATABASE [DeliveryOnline] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DeliveryOnline] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DeliveryOnline] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DeliveryOnline] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DeliveryOnline] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DeliveryOnline', N'ON'
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[domicilio] [varchar](50) NULL,
	[telefono] [varchar](50) NULL,
	[idTipoCliente] [bigint] NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Combo]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Combo](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NULL,
	[precioCombo] [decimal](18, 2) NULL,
	[precioPedido] [decimal](18, 2) NULL,
	[activo] [bit] NULL,
	[fechaAlta] [datetime] NULL,
	[fechaBaja] [datetime] NULL,
 CONSTRAINT [PK_Combo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Delivery]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Delivery](
	[idDelivery] [bigint] IDENTITY(1,1) NOT NULL,
	[precioDelivery] [decimal](4, 2) NULL,
	[idSucursal] [bigint] NULL,
 CONSTRAINT [PK_Delivery] PRIMARY KEY CLUSTERED 
(
	[idDelivery] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleCombo]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleCombo](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[idCombo] [bigint] NULL,
	[idProducto] [bigint] NULL,
	[cantidad] [bigint] NULL,
	[observaciones] [varchar](200) NULL,
 CONSTRAINT [PK_DetalleCombo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetallePedido]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetallePedido](
	[idDetallePedido] [bigint] IDENTITY(1,1) NOT NULL,
	[idPedido] [bigint] NOT NULL,
	[idProducto] [bigint] NULL,
	[cantidadProductoDetallePedido] [bigint] NULL,
	[observacionDetallePedido] [varchar](50) NULL,
	[nombreProducto] [varchar](40) NULL,
	[precioProducto] [decimal](16, 2) NULL,
	[activo] [bit] NULL,
	[idTipoDetallePedido] [bigint] NULL,
 CONSTRAINT [PK_DetallePedido] PRIMARY KEY CLUSTERED 
(
	[idDetallePedido] ASC,
	[idPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empresa]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empresa](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NULL,
	[domicilio] [varchar](100) NULL,
	[telefono] [varchar](20) NULL,
	[cuit] [varchar](14) NULL,
	[nombreFantasia] [varchar](100) NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_Empresa] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoPedido]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoPedido](
	[idEstadoPedido] [bigint] IDENTITY(1,1) NOT NULL,
	[nombreEstado] [varchar](15) NOT NULL,
 CONSTRAINT [PK_EstadoPedido] PRIMARY KEY CLUSTERED 
(
	[idEstadoPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrigenPedido]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrigenPedido](
	[idOrigen] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_OrigenPedido] PRIMARY KEY CLUSTERED 
(
	[idOrigen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[idPedido] [bigint] IDENTITY(1,1) NOT NULL,
	[idCliente] [bigint] NULL,
	[montoTotalPedido] [decimal](16, 2) NULL,
	[fechaPedido] [datetime] NULL,
	[horaPedido] [varchar](5) NULL,
	[observacionPedido] [varchar](50) NULL,
	[domicilioEntregaPedido] [varchar](50) NULL,
	[idEstadoPedido] [bigint] NULL,
	[montoAbono] [decimal](16, 2) NULL,
	[tiempoDemora] [bigint] NULL,
	[descuento] [decimal](18, 2) NULL,
	[nombreCliente] [varchar](50) NULL,
	[idSucursal] [bigint] NULL,
	[activo] [bit] NULL,
	[idOrigen] [int] NULL,
 CONSTRAINT [PK_Pedido] PRIMARY KEY CLUSTERED 
(
	[idPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Producto]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[idProducto] [bigint] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
	[precioX1] [decimal](8, 2) NULL,
	[precioX2] [decimal](8, 2) NULL,
	[precioX3] [decimal](8, 2) NULL,
	[activo] [bit] NULL,
	[precio] [decimal](8, 2) NULL,
 CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Promocion]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Promocion](
	[idPromocion] [bigint] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[codigo] [varchar](50) NULL,
	[activo] [bit] NULL,
 CONSTRAINT [PK_Promocion] PRIMARY KEY CLUSTERED 
(
	[idPromocion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
 CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Security]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Security](
	[psw] [varchar](50) NULL,
	[idPassword] [int] NOT NULL,
	[fchRegistro] [varchar](10) NULL,
	[fchValidoDesde] [varchar](10) NULL,
	[fchValidoHasta] [varchar](10) NULL,
 CONSTRAINT [PK_Security] PRIMARY KEY CLUSTERED 
(
	[idPassword] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sucursal]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sucursal](
	[id] [bigint] IDENTITY(0,1) NOT NULL,
	[nombre] [varchar](50) NULL,
	[direccion] [varchar](50) NULL,
	[telefono] [varchar](50) NULL,
	[localidad] [varchar](50) NULL,
	[activo] [bit] NULL,
	[activaAdmin] [bit] NULL,
	[responsable] [varchar](100) NULL,
	[email] [varchar](100) NULL,
	[idEmpresa] [bigint] NULL,
 CONSTRAINT [PK_Sucursal] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoCliente]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoCliente](
	[idTipoCliente] [bigint] IDENTITY(1,1) NOT NULL,
	[nombreTipoCliente] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TipoCliente] PRIMARY KEY CLUSTERED 
(
	[idTipoCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoDetallePedido]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoDetallePedido](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NULL,
 CONSTRAINT [PK_TipoDetallePedido] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[nombreUsuario] [varchar](50) NULL,
	[contraseña] [varchar](max) NULL,
	[activo] [bit] NULL,
	[nombre] [varchar](50) NULL,
	[telefono] [varchar](50) NULL,
	[domicilio] [varchar](50) NULL,
	[idRol] [bigint] NULL,
	[idSucursal] [bigint] NULL,
	[idEmpresa] [bigint] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 

INSERT [dbo].[Cliente] ([id], [nombre], [domicilio], [telefono], [idTipoCliente]) VALUES (17, N'javier', N'mta 1019', N'3516819583', NULL)
INSERT [dbo].[Cliente] ([id], [nombre], [domicilio], [telefono], [idTipoCliente]) VALUES (18, N'gabriela', N'mta 1018', N'3518056479', NULL)
SET IDENTITY_INSERT [dbo].[Cliente] OFF
SET IDENTITY_INSERT [dbo].[Combo] ON 

INSERT [dbo].[Combo] ([id], [nombre], [precioCombo], [precioPedido], [activo], [fechaAlta], [fechaBaja]) VALUES (1, N'2x1 en ''alguno''', CAST(38.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, NULL, CAST(N'2019-05-20T15:55:10.087' AS DateTime))
INSERT [dbo].[Combo] ([id], [nombre], [precioCombo], [precioPedido], [activo], [fechaAlta], [fechaBaja]) VALUES (2, N'4x2 e ''alguno''', CAST(83.00 AS Decimal(18, 2)), CAST(83.00 AS Decimal(18, 2)), 0, NULL, CAST(N'2019-05-20T15:55:10.087' AS DateTime))
INSERT [dbo].[Combo] ([id], [nombre], [precioCombo], [precioPedido], [activo], [fechaAlta], [fechaBaja]) VALUES (3, N'4x2 e ''alguno''', CAST(83.00 AS Decimal(18, 2)), CAST(83.00 AS Decimal(18, 2)), 0, NULL, CAST(N'2019-05-20T15:55:10.087' AS DateTime))
INSERT [dbo].[Combo] ([id], [nombre], [precioCombo], [precioPedido], [activo], [fechaAlta], [fechaBaja]) VALUES (4, N'4x2 e ''alguno''', CAST(117.00 AS Decimal(18, 2)), CAST(117.00 AS Decimal(18, 2)), 1, CAST(N'2019-05-20T15:31:10.043' AS DateTime), NULL)
INSERT [dbo].[Combo] ([id], [nombre], [precioCombo], [precioPedido], [activo], [fechaAlta], [fechaBaja]) VALUES (5, N'algo bizarro', CAST(85.00 AS Decimal(18, 2)), CAST(90.00 AS Decimal(18, 2)), 0, CAST(N'2019-05-20T15:31:10.043' AS DateTime), CAST(N'2019-05-20T15:55:10.087' AS DateTime))
INSERT [dbo].[Combo] ([id], [nombre], [precioCombo], [precioPedido], [activo], [fechaAlta], [fechaBaja]) VALUES (6, N'algo bizarro', CAST(185.00 AS Decimal(18, 2)), CAST(204.00 AS Decimal(18, 2)), 0, CAST(N'2019-05-20T15:55:10.097' AS DateTime), CAST(N'2019-05-22T11:01:02.140' AS DateTime))
INSERT [dbo].[Combo] ([id], [nombre], [precioCombo], [precioPedido], [activo], [fechaAlta], [fechaBaja]) VALUES (7, N'todos baratos', CAST(350.00 AS Decimal(18, 2)), CAST(516.00 AS Decimal(18, 2)), 1, CAST(N'2019-05-21T15:07:22.330' AS DateTime), NULL)
INSERT [dbo].[Combo] ([id], [nombre], [precioCombo], [precioPedido], [activo], [fechaAlta], [fechaBaja]) VALUES (8, N'algo bizarro', CAST(185.00 AS Decimal(18, 2)), CAST(166.00 AS Decimal(18, 2)), 1, CAST(N'2019-05-22T11:01:02.197' AS DateTime), NULL)
INSERT [dbo].[Combo] ([id], [nombre], [precioCombo], [precioPedido], [activo], [fechaAlta], [fechaBaja]) VALUES (9, N'2x1 fernet', CAST(0.00 AS Decimal(18, 2)), CAST(596.00 AS Decimal(18, 2)), 0, CAST(N'2019-05-28T16:55:25.443' AS DateTime), CAST(N'2019-05-28T16:55:32.987' AS DateTime))
INSERT [dbo].[Combo] ([id], [nombre], [precioCombo], [precioPedido], [activo], [fechaAlta], [fechaBaja]) VALUES (10, N'2x1 fernet', CAST(500.00 AS Decimal(18, 2)), CAST(596.00 AS Decimal(18, 2)), 1, CAST(N'2019-05-28T16:55:32.987' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Combo] OFF
SET IDENTITY_INSERT [dbo].[DetalleCombo] ON 

INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (1, 1, 16, 2, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (2, 2, 16, 2, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (3, 2, 14, 2, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (4, 3, 16, 2, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (5, 4, 16, 2, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (6, 4, 14, 1, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (7, 5, 14, 2, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (8, 6, 16, 3, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (9, 6, 14, 2, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (10, 7, 24, 2, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (11, 8, 16, 2, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (12, 8, 14, 2, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (13, 9, 16, 2, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (14, 9, 15, 4, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (15, 10, 16, 2, NULL)
INSERT [dbo].[DetalleCombo] ([id], [idCombo], [idProducto], [cantidad], [observaciones]) VALUES (16, 10, 15, 4, NULL)
SET IDENTITY_INSERT [dbo].[DetalleCombo] OFF
SET IDENTITY_INSERT [dbo].[DetallePedido] ON 

INSERT [dbo].[DetallePedido] ([idDetallePedido], [idPedido], [idProducto], [cantidadProductoDetallePedido], [observacionDetallePedido], [nombreProducto], [precioProducto], [activo], [idTipoDetallePedido]) VALUES (1, 63, 12, 2, N'sin mayo', N'lomo completo', CAST(150.00 AS Decimal(16, 2)), 1, NULL)
INSERT [dbo].[DetallePedido] ([idDetallePedido], [idPedido], [idProducto], [cantidadProductoDetallePedido], [observacionDetallePedido], [nombreProducto], [precioProducto], [activo], [idTipoDetallePedido]) VALUES (56, 101, 12, 1, N'sin mayonesa', N'miProducto1', CAST(0.00 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[DetallePedido] ([idDetallePedido], [idPedido], [idProducto], [cantidadProductoDetallePedido], [observacionDetallePedido], [nombreProducto], [precioProducto], [activo], [idTipoDetallePedido]) VALUES (57, 101, 13, 3, N'', N'suProducto2', CAST(0.00 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[DetallePedido] ([idDetallePedido], [idPedido], [idProducto], [cantidadProductoDetallePedido], [observacionDetallePedido], [nombreProducto], [precioProducto], [activo], [idTipoDetallePedido]) VALUES (58, 103, 12, 1, N'sin mayonesa', N'miProducto1', CAST(0.00 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[DetallePedido] ([idDetallePedido], [idPedido], [idProducto], [cantidadProductoDetallePedido], [observacionDetallePedido], [nombreProducto], [precioProducto], [activo], [idTipoDetallePedido]) VALUES (59, 103, 13, 3, N'', N'suProducto2', CAST(0.00 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[DetallePedido] ([idDetallePedido], [idPedido], [idProducto], [cantidadProductoDetallePedido], [observacionDetallePedido], [nombreProducto], [precioProducto], [activo], [idTipoDetallePedido]) VALUES (62, 105, 12, 1, N'sin mayonesa', N'miProducto1', CAST(0.00 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[DetallePedido] ([idDetallePedido], [idPedido], [idProducto], [cantidadProductoDetallePedido], [observacionDetallePedido], [nombreProducto], [precioProducto], [activo], [idTipoDetallePedido]) VALUES (63, 105, 13, 3, N'', N'suProducto2', CAST(0.00 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[DetallePedido] ([idDetallePedido], [idPedido], [idProducto], [cantidadProductoDetallePedido], [observacionDetallePedido], [nombreProducto], [precioProducto], [activo], [idTipoDetallePedido]) VALUES (64, 109, 16, 1, N'', N'alguno', CAST(38.00 AS Decimal(16, 2)), NULL, NULL)
INSERT [dbo].[DetallePedido] ([idDetallePedido], [idPedido], [idProducto], [cantidadProductoDetallePedido], [observacionDetallePedido], [nombreProducto], [precioProducto], [activo], [idTipoDetallePedido]) VALUES (65, 110, 16, 2, N'', N'alguno', CAST(38.00 AS Decimal(16, 2)), NULL, NULL)
SET IDENTITY_INSERT [dbo].[DetallePedido] OFF
SET IDENTITY_INSERT [dbo].[Empresa] ON 

INSERT [dbo].[Empresa] ([id], [nombre], [domicilio], [telefono], [cuit], [nombreFantasia], [activo]) VALUES (1, N'n2s3', N'pueyrredon 375', NULL, NULL, N'No hay 2 sin 3', 1)
SET IDENTITY_INSERT [dbo].[Empresa] OFF
SET IDENTITY_INSERT [dbo].[OrigenPedido] ON 

INSERT [dbo].[OrigenPedido] ([idOrigen], [nombre], [activo]) VALUES (1, N'Mostrador', 1)
INSERT [dbo].[OrigenPedido] ([idOrigen], [nombre], [activo]) VALUES (2, N'Domicilio', 1)
INSERT [dbo].[OrigenPedido] ([idOrigen], [nombre], [activo]) VALUES (3, N'Mesa', 1)
SET IDENTITY_INSERT [dbo].[OrigenPedido] OFF
SET IDENTITY_INSERT [dbo].[Pedido] ON 

INSERT [dbo].[Pedido] ([idPedido], [idCliente], [montoTotalPedido], [fechaPedido], [horaPedido], [observacionPedido], [domicilioEntregaPedido], [idEstadoPedido], [montoAbono], [tiempoDemora], [descuento], [nombreCliente], [idSucursal], [activo], [idOrigen]) VALUES (63, 17, CAST(300.00 AS Decimal(16, 2)), CAST(N'2019-04-11T09:00:00.000' AS DateTime), N'10:22', N'bla bla', N'mta 1017 2b', NULL, CAST(500.00 AS Decimal(16, 2)), 5, CAST(0.00 AS Decimal(18, 2)), N'javier', 1, 1, NULL)
INSERT [dbo].[Pedido] ([idPedido], [idCliente], [montoTotalPedido], [fechaPedido], [horaPedido], [observacionPedido], [domicilioEntregaPedido], [idEstadoPedido], [montoAbono], [tiempoDemora], [descuento], [nombreCliente], [idSucursal], [activo], [idOrigen]) VALUES (101, 17, CAST(0.00 AS Decimal(16, 2)), CAST(N'2019-04-12T17:43:31.367' AS DateTime), NULL, N'www', N'mta 1019', NULL, CAST(0.00 AS Decimal(16, 2)), 0, CAST(0.00 AS Decimal(18, 2)), N'javier', 0, 1, NULL)
INSERT [dbo].[Pedido] ([idPedido], [idCliente], [montoTotalPedido], [fechaPedido], [horaPedido], [observacionPedido], [domicilioEntregaPedido], [idEstadoPedido], [montoAbono], [tiempoDemora], [descuento], [nombreCliente], [idSucursal], [activo], [idOrigen]) VALUES (103, 17, CAST(0.00 AS Decimal(16, 2)), CAST(N'2019-04-12T17:44:25.237' AS DateTime), NULL, N'333333', N'mta 1019', NULL, CAST(0.00 AS Decimal(16, 2)), 0, CAST(0.00 AS Decimal(18, 2)), N'javier', 0, 1, NULL)
INSERT [dbo].[Pedido] ([idPedido], [idCliente], [montoTotalPedido], [fechaPedido], [horaPedido], [observacionPedido], [domicilioEntregaPedido], [idEstadoPedido], [montoAbono], [tiempoDemora], [descuento], [nombreCliente], [idSucursal], [activo], [idOrigen]) VALUES (105, 17, CAST(0.00 AS Decimal(16, 2)), CAST(N'2019-04-15T11:01:48.437' AS DateTime), NULL, N'', N'mta 1019', NULL, CAST(343.00 AS Decimal(16, 2)), 4, CAST(0.00 AS Decimal(18, 2)), N'javier', 0, 1, NULL)
INSERT [dbo].[Pedido] ([idPedido], [idCliente], [montoTotalPedido], [fechaPedido], [horaPedido], [observacionPedido], [domicilioEntregaPedido], [idEstadoPedido], [montoAbono], [tiempoDemora], [descuento], [nombreCliente], [idSucursal], [activo], [idOrigen]) VALUES (109, 17, CAST(0.00 AS Decimal(16, 2)), CAST(N'2019-04-16T17:20:03.987' AS DateTime), NULL, N'', N'mta 1019', NULL, CAST(100.00 AS Decimal(16, 2)), 0, CAST(0.00 AS Decimal(18, 2)), N'javier', 0, 1, NULL)
INSERT [dbo].[Pedido] ([idPedido], [idCliente], [montoTotalPedido], [fechaPedido], [horaPedido], [observacionPedido], [domicilioEntregaPedido], [idEstadoPedido], [montoAbono], [tiempoDemora], [descuento], [nombreCliente], [idSucursal], [activo], [idOrigen]) VALUES (110, 17, CAST(0.00 AS Decimal(16, 2)), CAST(N'2019-04-16T17:49:22.607' AS DateTime), NULL, N'', N'mta 1019', NULL, CAST(100.00 AS Decimal(16, 2)), 5, CAST(0.00 AS Decimal(18, 2)), N'javier', 0, 1, NULL)
SET IDENTITY_INSERT [dbo].[Pedido] OFF
SET IDENTITY_INSERT [dbo].[Producto] ON 

INSERT [dbo].[Producto] ([idProducto], [nombre], [descripcion], [precioX1], [precioX2], [precioX3], [activo], [precio]) VALUES (12, N'lomito americano', N'descrip1', CAST(0.00 AS Decimal(8, 2)), CAST(0.00 AS Decimal(8, 2)), CAST(0.00 AS Decimal(8, 2)), 1, CAST(180.00 AS Decimal(8, 2)))
INSERT [dbo].[Producto] ([idProducto], [nombre], [descripcion], [precioX1], [precioX2], [precioX3], [activo], [precio]) VALUES (13, N'piadina', N'aaaaaa', CAST(0.00 AS Decimal(8, 2)), CAST(0.00 AS Decimal(8, 2)), CAST(0.00 AS Decimal(8, 2)), 0, CAST(150.00 AS Decimal(8, 2)))
INSERT [dbo].[Producto] ([idProducto], [nombre], [descripcion], [precioX1], [precioX2], [precioX3], [activo], [precio]) VALUES (14, N'empanada', N'de algas', NULL, NULL, NULL, 1, CAST(45.00 AS Decimal(8, 2)))
INSERT [dbo].[Producto] ([idProducto], [nombre], [descripcion], [precioX1], [precioX2], [precioX3], [activo], [precio]) VALUES (15, N'locro', N'pulsudo', NULL, NULL, NULL, 1, CAST(130.00 AS Decimal(8, 2)))
INSERT [dbo].[Producto] ([idProducto], [nombre], [descripcion], [precioX1], [precioX2], [precioX3], [activo], [precio]) VALUES (16, N'alguno', N'descripcion copada', NULL, NULL, NULL, 0, CAST(38.00 AS Decimal(8, 2)))
INSERT [dbo].[Producto] ([idProducto], [nombre], [descripcion], [precioX1], [precioX2], [precioX3], [activo], [precio]) VALUES (24, N'lomo blanco', N'algo blanco', NULL, NULL, NULL, 1, CAST(258.00 AS Decimal(8, 2)))
INSERT [dbo].[Producto] ([idProducto], [nombre], [descripcion], [precioX1], [precioX2], [precioX3], [activo], [precio]) VALUES (25, N'algo', N'algo copado', NULL, NULL, NULL, 1, CAST(36.00 AS Decimal(8, 2)))
INSERT [dbo].[Producto] ([idProducto], [nombre], [descripcion], [precioX1], [precioX2], [precioX3], [activo], [precio]) VALUES (33, N'celular', N'moto g7', NULL, NULL, NULL, 0, CAST(12000.00 AS Decimal(8, 2)))
INSERT [dbo].[Producto] ([idProducto], [nombre], [descripcion], [precioX1], [precioX2], [precioX3], [activo], [precio]) VALUES (34, N'ruben braca', N'', NULL, NULL, NULL, 0, CAST(22.00 AS Decimal(8, 2)))
INSERT [dbo].[Producto] ([idProducto], [nombre], [descripcion], [precioX1], [precioX2], [precioX3], [activo], [precio]) VALUES (35, N'elefante', N'mi bello hipopotamo', NULL, NULL, NULL, 1, CAST(5000.00 AS Decimal(8, 2)))
SET IDENTITY_INSERT [dbo].[Producto] OFF
SET IDENTITY_INSERT [dbo].[Rol] ON 

INSERT [dbo].[Rol] ([id], [nombre]) VALUES (1, N'Total')
INSERT [dbo].[Rol] ([id], [nombre]) VALUES (2, N'Propietario')
INSERT [dbo].[Rol] ([id], [nombre]) VALUES (3, N'Administrador')
INSERT [dbo].[Rol] ([id], [nombre]) VALUES (4, N'Empleado')
SET IDENTITY_INSERT [dbo].[Rol] OFF
INSERT [dbo].[Security] ([psw], [idPassword], [fchRegistro], [fchValidoDesde], [fchValidoHasta]) VALUES (N'2J7A0V5I8E5R', 1, N'12:00 a.m.', N'15/10/2015', N'23/10/2016')
SET IDENTITY_INSERT [dbo].[Sucursal] ON 

INSERT [dbo].[Sucursal] ([id], [nombre], [direccion], [telefono], [localidad], [activo], [activaAdmin], [responsable], [email], [idEmpresa]) VALUES (0, NULL, N'TODAS', N'0', N'0', 1, 1, NULL, NULL, NULL)
INSERT [dbo].[Sucursal] ([id], [nombre], [direccion], [telefono], [localidad], [activo], [activaAdmin], [responsable], [email], [idEmpresa]) VALUES (1, NULL, N'pueyrredon', N'1', N'cordoba', 0, 1, NULL, NULL, NULL)
INSERT [dbo].[Sucursal] ([id], [nombre], [direccion], [telefono], [localidad], [activo], [activaAdmin], [responsable], [email], [idEmpresa]) VALUES (2, NULL, N'24 de septiembre', N'1', N'cordoba', 1, 1, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Sucursal] OFF
SET IDENTITY_INSERT [dbo].[TipoDetallePedido] ON 

INSERT [dbo].[TipoDetallePedido] ([id], [nombre]) VALUES (1, N'Producto')
INSERT [dbo].[TipoDetallePedido] ([id], [nombre]) VALUES (2, N'Combo')
SET IDENTITY_INSERT [dbo].[TipoDetallePedido] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([id], [nombreUsuario], [contraseña], [activo], [nombre], [telefono], [domicilio], [idRol], [idSucursal], [idEmpresa]) VALUES (1, N'aaa', N'F6E0A1E2AC41945A9AA7FF8A8AAA0CEBC12A3BCC981A929AD5CF810A090E11AE', 1, N'aaa', NULL, NULL, 1, NULL, 1)
INSERT [dbo].[Usuario] ([id], [nombreUsuario], [contraseña], [activo], [nombre], [telefono], [domicilio], [idRol], [idSucursal], [idEmpresa]) VALUES (6, N'jmlansky', N'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e99', 1, N'Javier lansky', N'3516819583', N'mta 1017 2b', 1, 0, 1)
INSERT [dbo].[Usuario] ([id], [nombreUsuario], [contraseña], [activo], [nombre], [telefono], [domicilio], [idRol], [idSucursal], [idEmpresa]) VALUES (11, N'braca1', N'8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 1, N'ruben braca', N'1111111111', N'rondeau 285', 2, 0, 1)
INSERT [dbo].[Usuario] ([id], [nombreUsuario], [contraseña], [activo], [nombre], [telefono], [domicilio], [idRol], [idSucursal], [idEmpresa]) VALUES (12, N'braca2', N'da5511d2baa83c2e753852f1f2fba11003ed0c46c96820c7589b243a8ddb787a', 1, N'agus', N'1111111111', N'rondeau 285', 3, 0, 1)
INSERT [dbo].[Usuario] ([id], [nombreUsuario], [contraseña], [activo], [nombre], [telefono], [domicilio], [idRol], [idSucursal], [idEmpresa]) VALUES (13, N'braca3', N'F6E0A1E2AC41945A9AA7FF8A8AAA0CEBC12A3BCC981A929AD5CF810A090E11AE', 1, N'agus', N'1111111111', N'rondeau 285', 4, 0, 1)
INSERT [dbo].[Usuario] ([id], [nombreUsuario], [contraseña], [activo], [nombre], [telefono], [domicilio], [idRol], [idSucursal], [idEmpresa]) VALUES (14, N'lucas123', N'ef797c8118f02dfb649607dd5d3f8c7623048c9c063d532cc95c5ed7a898a64f', 1, N'lucas', N'22222222', N'vendepaco', 3, 0, 1)
INSERT [dbo].[Usuario] ([id], [nombreUsuario], [contraseña], [activo], [nombre], [telefono], [domicilio], [idRol], [idSucursal], [idEmpresa]) VALUES (15, N'a', N'ca978112ca1bbdcafac231b39a23dc4da786eff8147c4e72b9807785afee48bb', 1, N'a', N'a', N'a', 2, 0, 1)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
ALTER TABLE [dbo].[Combo] ADD  CONSTRAINT [DF_Combo_fechaAlta]  DEFAULT (getdate()) FOR [fechaAlta]
GO
ALTER TABLE [dbo].[Pedido] ADD  CONSTRAINT [DF__Pedido__nombreCl__1ED998B2]  DEFAULT ('') FOR [nombreCliente]
GO
ALTER TABLE [dbo].[Sucursal] ADD  CONSTRAINT [DF_Sucursal_activaAdmin]  DEFAULT ((1)) FOR [activaAdmin]
GO
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [FK_Cliente_TipoCliente1] FOREIGN KEY([idTipoCliente])
REFERENCES [dbo].[TipoCliente] ([idTipoCliente])
GO
ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [FK_Cliente_TipoCliente1]
GO
ALTER TABLE [dbo].[Delivery]  WITH CHECK ADD  CONSTRAINT [FK_Delivery_Sucursal] FOREIGN KEY([idSucursal])
REFERENCES [dbo].[Sucursal] ([id])
GO
ALTER TABLE [dbo].[Delivery] CHECK CONSTRAINT [FK_Delivery_Sucursal]
GO
ALTER TABLE [dbo].[DetalleCombo]  WITH CHECK ADD  CONSTRAINT [FK_DetalleCombo_Combo] FOREIGN KEY([idCombo])
REFERENCES [dbo].[Combo] ([id])
GO
ALTER TABLE [dbo].[DetalleCombo] CHECK CONSTRAINT [FK_DetalleCombo_Combo]
GO
ALTER TABLE [dbo].[DetalleCombo]  WITH CHECK ADD  CONSTRAINT [FK_DetalleCombo_Producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([idProducto])
GO
ALTER TABLE [dbo].[DetalleCombo] CHECK CONSTRAINT [FK_DetalleCombo_Producto]
GO
ALTER TABLE [dbo].[DetallePedido]  WITH CHECK ADD  CONSTRAINT [FK_DetallePedido_Pedido] FOREIGN KEY([idPedido])
REFERENCES [dbo].[Pedido] ([idPedido])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetallePedido] CHECK CONSTRAINT [FK_DetallePedido_Pedido]
GO
ALTER TABLE [dbo].[DetallePedido]  WITH CHECK ADD  CONSTRAINT [FK_DetallePedido_Producto] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Producto] ([idProducto])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[DetallePedido] CHECK CONSTRAINT [FK_DetallePedido_Producto]
GO
ALTER TABLE [dbo].[DetallePedido]  WITH CHECK ADD  CONSTRAINT [FK_DetallePedido_TipoDetallePedido] FOREIGN KEY([idTipoDetallePedido])
REFERENCES [dbo].[TipoDetallePedido] ([id])
GO
ALTER TABLE [dbo].[DetallePedido] CHECK CONSTRAINT [FK_DetallePedido_TipoDetallePedido]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Cliente] FOREIGN KEY([idCliente])
REFERENCES [dbo].[Cliente] ([id])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Cliente]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_EstadoPedido] FOREIGN KEY([idEstadoPedido])
REFERENCES [dbo].[EstadoPedido] ([idEstadoPedido])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_EstadoPedido]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_OrigenPedido] FOREIGN KEY([idOrigen])
REFERENCES [dbo].[OrigenPedido] ([idOrigen])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_OrigenPedido]
GO
ALTER TABLE [dbo].[Sucursal]  WITH CHECK ADD  CONSTRAINT [FK_Sucursal_Empresa] FOREIGN KEY([idEmpresa])
REFERENCES [dbo].[Empresa] ([id])
GO
ALTER TABLE [dbo].[Sucursal] CHECK CONSTRAINT [FK_Sucursal_Empresa]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Empresa] FOREIGN KEY([idEmpresa])
REFERENCES [dbo].[Empresa] ([id])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Empresa]
GO
ALTER TABLE [dbo].[Usuario]  WITH NOCHECK ADD  CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY([idRol])
REFERENCES [dbo].[Rol] ([id])
GO
ALTER TABLE [dbo].[Usuario] NOCHECK CONSTRAINT [FK_Usuario_Rol]
GO
ALTER TABLE [dbo].[Usuario]  WITH NOCHECK ADD  CONSTRAINT [FK_Usuario_Sucursal] FOREIGN KEY([idSucursal])
REFERENCES [dbo].[Sucursal] ([id])
GO
ALTER TABLE [dbo].[Usuario] NOCHECK CONSTRAINT [FK_Usuario_Sucursal]
GO
/****** Object:  StoredProcedure [dbo].[Pedidos_getSumaProductosPorSucursal]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Pedidos_getSumaProductosPorSucursal]
	@fechaDesde date,
	@fechaHasta date,
	@idSucursal bigint
AS
BEGIN
	select dp.nombreProducto, SUM(dp.cantidadProductoDetallePedido) as 'CantidadProducto'
	from pedido ped, detallePedido dp 
	where ped.idPedido = dp.idPedido 
	and ped.fechaPedido between @fechaDesde and @fechaHasta
	and ped.idSucursal = @idSucursal
	group by dp.nombreProducto
END
GO
/****** Object:  StoredProcedure [dbo].[Pedidos_getSumaProductosTodasSucursales]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Pedidos_getSumaProductosTodasSucursales]
	@fechaDesde date,
	@fechaHasta date
AS
BEGIN
	select dp.nombreProducto, SUM(dp.cantidadProductoDetallePedido) as 'CantidadProducto'
	from pedido ped, detallePedido dp 
	where ped.idPedido = dp.idPedido 
	and ped.fechaPedido between @fechaDesde and @fechaHasta
	group by dp.nombreProducto
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_AddNew]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Usuario_AddNew]
	@nombreUsuario varchar(50),
	@contraseña varchar(max),
	@idRol bigint,
	@idSucursal bigint,
	@nombre varchar(50),
	@telefono varchar(50),
	@domicilio varchar(50)
AS
BEGIN
	insert into Usuario(nombreUsuario, contraseña, idRol, idSucursal, nombre, telefono, domicilio, activo) 
		values (@nombreUsuario, @contraseña, @idRol, @idSucursal, @nombre, @telefono, @domicilio, 1)
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_cambiarContraseña]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[Usuario_cambiarContraseña]
	@nombreUsuario varchar(50),
	@contraseña varchar(50)
AS
BEGIN
	update Usuario
	set contraseña = @contraseña
	where nombreUsuario = @nombreUsuario
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_getAll]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Usuario_getAll]	
	@activo int,
	@nombre varchar(50)
AS
BEGIN
	select usr.idUsuario, usr.nombre, usr.nombreUsuario, per.nombrePerfil, suc.direccion as direccionSucursal, usr.idPerfil, usr.contraseña, usr.idSucursal, 
		usr.telefono, usr.domicilio		
	from Usuario usr, sucursal suc, perfil per
	where activo = @activo
	and nombre like '%' + @nombre + '%'
	and usr.idSucursal = suc.idSucursal
	and usr.idPerfil = per.idPerfil	
	order by idUsuario, idSucursal asc
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_getAll_bySucursal]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Usuario_getAll_bySucursal]
	@idSucursal bigint,
	@nombre varchar(50)
AS
BEGIN
	select usr.idUsuario, usr.nombre, usr.nombreUsuario, per.nombrePerfil, suc.direccion as direccionSucursal, usr.idPerfil, usr.contraseña, usr.idSucursal, 
		usr.telefono, usr.domicilio		
	from Usuario usr, sucursal suc, perfil per
	where activo = 1
	and usr.idSucursal = suc.idSucursal
	and usr.idPerfil = per.idPerfil
	and usr.idSucursal = @idSucursal
	and nombre like '%' + @nombre + '%'
	order by idUsuario, idSucursal asc
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_getAll_inactivos]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Usuario_getAll_inactivos]
	@idSucursal bigint,
	@nombre varchar(50)
AS
BEGIN
	select usr.idUsuario, usr.nombre, usr.nombreUsuario, per.nombrePerfil, suc.direccion as direccionSucursal, usr.idPerfil, usr.contraseña, usr.idSucursal, 
		usr.telefono, usr.domicilio
	from Usuario usr, sucursal suc, perfil per
	where activo = 0
	and usr.idSucursal = @idSucursal
	and usr.idSucursal = suc.idSucursal
	and usr.idPerfil = per.idPerfil
	and usr.nombre like '%' + @nombre + '%'
	order by usr.idUsuario, usr.idSucursal asc
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_getAll_inactvo_bySucursal]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Usuario_getAll_inactvo_bySucursal]
	@idSucursal bigint,
	@nombre varchar(50)
AS
BEGIN
	select usr.idUsuario, usr.nombre, usr.nombreUsuario, per.nombrePerfil, suc.direccion as direccionSucursal, usr.idPerfil, usr.contraseña, usr.idSucursal, 
		usr.telefono, usr.domicilio		
	from Usuario usr, sucursal suc, perfil per
	where activo = 0
	and usr.idSucursal = suc.idSucursal
	and usr.idPerfil = per.idPerfil
	and usr.idSucursal = @idSucursal
	and nombre like '%' + @nombre + '%'
	order by idUsuario, idSucursal asc
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_getOne]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Usuario_getOne]
	@idUsuario bigint
AS
BEGIN
	select usr.nombreUsuario, usr.contraseña, usr.idPerfil, per.nombrePerfil, usr.idSucursal, suc.direccion as dirSuc, usr.activo, usr.nombre, usr.telefono
	from Usuario usr, Perfil per, Sucursal suc
	where usr.idPerfil = per.idPerfil
	and usr.idSucursal = suc.idSucursal
	and usr.idUsuario = @idUsuario
END
GO
/****** Object:  StoredProcedure [dbo].[Usuario_getValidacion]    Script Date: 8/4/2019 3:01:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Usuario_getValidacion]
	@nombreUsuario varchar(50),
	@contraseña varchar(50)
AS
BEGIN
	select usr.idPerfil, per.nombrePerfil, usr.idSucursal
	from Usuario usr, Perfil per
	where usr.idPerfil = per.idPerfil
	and usr.nombreUsuario = @nombreUsuario
	and usr.contraseña = @contraseña
END
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = SI / 0= NO (dada de baja)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sucursal', @level2type=N'COLUMN',@level2name=N'activo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'1 = activa para funcionar (si tiene el pago al dia) - definido por el dueño del sistema (no admin de la cadena)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Sucursal', @level2type=N'COLUMN',@level2name=N'activaAdmin'
GO
ALTER DATABASE [DeliveryOnline] SET  READ_WRITE 
GO
