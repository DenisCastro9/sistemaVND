USE [master]
GO
/****** Object:  Database [sistemaVND]    Script Date: 31/7/2023 01:11:25 ******/
CREATE DATABASE [sistemaVND]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'sistemaVND', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\sistemaVND.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'sistemaVND_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\sistemaVND_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [sistemaVND] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [sistemaVND].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [sistemaVND] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [sistemaVND] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [sistemaVND] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [sistemaVND] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [sistemaVND] SET ARITHABORT OFF 
GO
ALTER DATABASE [sistemaVND] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [sistemaVND] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [sistemaVND] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [sistemaVND] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [sistemaVND] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [sistemaVND] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [sistemaVND] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [sistemaVND] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [sistemaVND] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [sistemaVND] SET  DISABLE_BROKER 
GO
ALTER DATABASE [sistemaVND] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [sistemaVND] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [sistemaVND] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [sistemaVND] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [sistemaVND] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [sistemaVND] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [sistemaVND] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [sistemaVND] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [sistemaVND] SET  MULTI_USER 
GO
ALTER DATABASE [sistemaVND] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [sistemaVND] SET DB_CHAINING OFF 
GO
ALTER DATABASE [sistemaVND] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [sistemaVND] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [sistemaVND] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [sistemaVND] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'sistemaVND', N'ON'
GO
ALTER DATABASE [sistemaVND] SET QUERY_STORE = OFF
GO
USE [sistemaVND]
GO
/****** Object:  Table [dbo].[articulo]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[articulo](
	[idarticulo] [int] IDENTITY(1,1) NOT NULL,
	[codigoArticulo] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[tipoArticulo] [varchar](50) NULL,
	[descripcion] [varchar](50) NULL,
	[marca] [varchar](50) NULL,
	[fabricacion] [varchar](5) NULL,
	[precioUnitario] [decimal](18, 2) NULL,
	[idmateriaPrimaPorArticulo] [int] NULL,
	[talleNombre] [varchar](50) NULL,
	[talle] [int] NOT NULL,
	[idCantidadMPxArticulo] [int] NULL,
	[cantidadEnStock] [int] NULL,
	[reservado] [int] NULL,
 CONSTRAINT [PK__articulo__BCE2F8F77ED23CF9] PRIMARY KEY CLUSTERED 
(
	[idarticulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArticulosGeneral]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArticulosGeneral](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[precioNino] [decimal](18, 2) NULL,
	[precioDama] [decimal](18, 2) NULL,
	[precioHombre] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cantidadMPxArticulo]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cantidadMPxArticulo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[articulo] [varchar](50) NULL,
	[cantidad] [varchar](50) NULL,
	[area] [varchar](50) NULL,
	[idMateriaPrima] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cantidadMPxPedido]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cantidadMPxPedido](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numeroPedido] [int] NULL,
	[articulo] [varchar](50) NULL,
	[cantidadXpares] [varchar](50) NULL,
	[area] [varchar](50) NULL,
	[totalPares] [int] NULL,
	[idMateriaPrima] [int] NULL,
 CONSTRAINT [PK__cantidad__3213E83F225FBE1F] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[razonSocial] [varchar](50) NOT NULL,
	[cuit] [varchar](50) NOT NULL,
	[mail] [varchar](50) NULL,
	[telefono] [varchar](50) NOT NULL,
	[ingresosBrutos] [varchar](50) NULL,
	[condicionIva] [varchar](50) NULL,
	[domicilioDeEntrega] [varchar](50) NULL,
	[codigoPostal] [varchar](10) NULL,
	[calle] [varchar](50) NULL,
	[altura] [int] NULL,
	[barrio] [varchar](50) NULL,
	[idLocalidad] [int] NULL,
	[idProvincia] [int] NULL,
	[idCredito] [int] NULL,
	[idDescuento] [int] NULL,
	[idJurisdiccion] [int] NULL,
	[idTransportista] [int] NULL,
 CONSTRAINT [PK__cliente__3213E83F8D2E9F8A] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[credito]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[credito](
	[idcreditO] [int] IDENTITY(1,1) NOT NULL,
	[estado] [varchar](50) NULL,
	[limite] [float] NULL,
PRIMARY KEY CLUSTERED 
(
	[idcreditO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[descuento]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[descuento](
	[iddescuento] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[iddescuento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalleDePedido]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalleDePedido](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[idArticulo] [int] NULL,
	[precio] [decimal](18, 2) NULL,
 CONSTRAINT [PK__detalleD__3213E83F51E23FD9] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalleFactura]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalleFactura](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [bigint] NULL,
	[idPedido] [int] NULL,
 CONSTRAINT [PK__detalleF__3213E83F9ECAB07A] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalleOrdenDeCompra]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalleOrdenDeCompra](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [bigint] NOT NULL,
	[codigoItem] [varchar](50) NULL,
	[cantidad] [varchar](50) NULL,
	[precioUnitario] [decimal](18, 2) NULL,
	[importeItem] [decimal](18, 2) NULL,
	[idMateriaPrima] [int] NULL,
 CONSTRAINT [PK__detalleO__3213E83FEBCA1333] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[detalleOrdenF]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[detalleOrdenF](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [int] NULL,
	[cantidad] [int] NOT NULL,
	[idArticulo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[domicilio]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[domicilio](
	[numero] [int] NOT NULL,
	[calle] [varchar](50) NOT NULL,
	[nombreBarrio] [varchar](50) NOT NULL,
	[idLocalidad] [int] NOT NULL,
	[idProvincia] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[estadoOrdenC]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estadoOrdenC](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[estadoOrdenF]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estadoOrdenF](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[estadoPedido]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[estadoPedido](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[factura]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[factura](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [bigint] NOT NULL,
	[fecha] [date] NOT NULL,
	[montoTotal] [decimal](18, 2) NOT NULL,
	[tipoFactura] [varchar](2) NOT NULL,
	[PorcentajeAlicuotaIva] [int] NOT NULL,
	[importeIva] [decimal](18, 2) NOT NULL,
	[importeTributos] [decimal](18, 2) NULL,
	[idRemito] [int] NULL,
	[idCliente] [int] NULL,
	[idDetalleFactura] [int] NULL,
	[idFormaDePago] [int] NULL,
 CONSTRAINT [PK__factura__3213E83FF98FC4F7] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[formaDePago]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[formaDePago](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[iva]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iva](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[jurisdiccion]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[jurisdiccion](
	[idjurisdiccion] [int] IDENTITY(1,1) NOT NULL,
	[jurisdiccion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idjurisdiccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[localidad]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[localidad](
	[idLocalidad] [int] IDENTITY(1,1) NOT NULL,
	[nombreLocalidad] [varchar](50) NOT NULL,
	[codigoPostal] [varchar](50) NOT NULL,
	[idProvincia] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[marcaMP]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[marcaMP](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
	[idProveedor] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[materiaPrima]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[materiaPrima](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [varchar](40) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
	[stockMinimo] [int] NOT NULL,
	[cantidad] [int] NULL,
	[IdUnidadDeMedida] [int] NULL,
	[cantidadQueContiene] [decimal](18, 2) NULL,
	[IdSubUnidadDeMedidda] [int] NULL,
	[idMarcaMP] [int] NULL,
	[idtipoMP] [int] NULL,
	[reservado] [int] NULL,
	[ultimoPrecio] [decimal](18, 2) NULL,
 CONSTRAINT [PK__materiaP__3213E83F00A9C3B7] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[materiaPrimaPorArticulo]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[materiaPrimaPorArticulo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigoArt] [varchar](50) NOT NULL,
	[cuero1] [varchar](50) NULL,
	[cuero2] [varchar](50) NULL,
	[forro1] [varchar](50) NULL,
	[forro2] [varchar](50) NULL,
	[refuerzo1] [varchar](50) NULL,
	[refuerzo2] [varchar](50) NULL,
	[refuerzo3] [varchar](50) NULL,
	[hilo1] [varchar](50) NULL,
	[hilo2] [varchar](50) NULL,
	[costuron] [varchar](50) NULL,
	[apliqueI] [varchar](50) NULL,
	[apliqueII] [varchar](50) NULL,
	[apliqueIII] [varchar](50) NULL,
	[contrafuerte] [varchar](50) NULL,
	[elastico] [varchar](50) NULL,
	[ojalillos] [varchar](50) NULL,
	[plantillaArmado] [varchar](50) NULL,
	[caja] [varchar](50) NULL,
	[plantilla] [varchar](50) NULL,
	[cordon] [varchar](50) NULL,
	[horma] [varchar](50) NULL,
	[fondo] [varchar](50) NULL,
	[comentario1] [varchar](50) NULL,
	[comentario2] [varchar](50) NULL,
	[comentario3] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ordenDeCompra]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ordenDeCompra](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [bigint] NOT NULL,
	[fecha] [date] NOT NULL,
	[subtotal] [decimal](18, 2) NULL,
	[importeDescuento] [decimal](18, 2) NULL,
	[ivaPorcentaje] [varchar](10) NULL,
	[importeIva] [decimal](18, 2) NULL,
	[importeEnvio] [decimal](18, 2) NULL,
	[importeTotal] [decimal](18, 2) NULL,
	[fechaDeIngreso] [date] NULL,
	[enviarEmpresa] [varchar](50) NOT NULL,
	[enviarDomicilio] [varchar](50) NOT NULL,
	[enviarTelefono] [varchar](50) NOT NULL,
	[idProveedor] [int] NULL,
	[idDetalleOrdenDeCompra] [int] NULL,
	[idEstadoOrdenC] [int] NULL,
 CONSTRAINT [PK__ordenDeC__3213E83FED6BF9D0] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ordenDeFabricacion]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ordenDeFabricacion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [int] NOT NULL,
	[fechaCreacion] [date] NOT NULL,
	[fechaPrevistaFin] [date] NULL,
	[totalPares] [int] NOT NULL,
	[idEstadoOrdenF] [int] NULL,
	[idPuntoDeControl] [int] NULL,
	[idDetalleOrdenF] [int] NULL,
	[cliente] [varchar](50) NULL,
	[idPedido] [int] NULL,
	[fechaEmision] [date] NULL,
	[fechaRealInicio] [date] NULL,
	[fechaRealFin] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pedido]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pedido](
	[numero] [int] IDENTITY(1,1) NOT NULL,
	[fechaDePedido] [date] NOT NULL,
	[importeTotal] [decimal](18, 2) NULL,
	[fechaSalidaDePedido] [date] NULL,
	[totalPares] [int] NULL,
	[idDetallePedido] [int] NULL,
	[idCliente] [int] NOT NULL,
	[idOrdenDeFabricacion] [int] NULL,
	[idUsuario] [int] NULL,
	[idEstadoPedido] [int] NULL,
	[idArt] [int] NULL,
	[idFormaDePago] [int] NOT NULL,
 CONSTRAINT [PK__pedido__FC77F210F6CB19AF] PRIMARY KEY CLUSTERED 
(
	[numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[proveedor]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proveedor](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[cuit] [varchar](50) NULL,
	[mail] [varchar](45) NULL,
	[telefono] [varchar](50) NULL,
	[condicionIva] [varchar](50) NULL,
	[idDomicilio] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[provincia]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[provincia](
	[idprovincia] [int] IDENTITY(1,1) NOT NULL,
	[nombreProvincia] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[idprovincia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[puntoDeControl]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[puntoDeControl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[remito]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[remito](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [bigint] NOT NULL,
	[fecha] [date] NOT NULL,
	[bultos] [int] NULL,
	[idPedido] [int] NULL,
 CONSTRAINT [PK__remito__3213E83F2FFA2845] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubUnidadDeMedida]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubUnidadDeMedida](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tipoMP]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tipoMP](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
	[idProveedor] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transportista]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transportista](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cuit] [varchar](50) NULL,
	[nombre] [varchar](50) NOT NULL,
	[mail] [varchar](45) NULL,
	[telefono] [varchar](50) NOT NULL,
	[idDomicilio] [int] NULL,
 CONSTRAINT [PK__transpor__3213E83F7E4FAC25] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[unidadDeMedida]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[unidadDeMedida](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 31/7/2023 01:11:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[dni] [bigint] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[clave] [varchar](10) NOT NULL,
	[fechaIngreso] [datetime] NULL,
	[fechaEgreso] [datetime] NULL,
	[preg1] [varchar](50) NOT NULL,
	[preg2] [varchar](50) NULL,
	[preg3] [varchar](50) NULL,
	[nivel] [varchar](50) NOT NULL,
	[area] [varchar](50) NOT NULL,
	[region] [varchar](50) NULL,
 CONSTRAINT [PK__usuarios__645723A6F14EA09C] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[articulo] ON 

INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (6, 0, N'siena acordonado negro', N'Mocasin', N'Cuero azul engrasado, apliques en negro y gris.', N'Scarpino', N'S', CAST(4950.80 AS Decimal(18, 2)), NULL, N'Hombre', 39, NULL, 24, 19)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (7, 1, N'siena acordonado negro', N'Mocasin', N'Cuero azul engrasado, apliques en negro y gris.', N'Scarpino', N'S', CAST(4950.80 AS Decimal(18, 2)), NULL, N'Hombre', 40, NULL, 12, 8)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (8, 2, N'siena acordonado negro', N'Mocasin', N'Cuero azul engrasado, apliques en negro y gris.', N'Scarpino', N'S', CAST(4950.80 AS Decimal(18, 2)), NULL, N'Hombre', 41, NULL, 22, 6)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (9, 3, N'siena acordonado negro', N'Mocasin', N'Cuero azul engrasado, apliques en negro y gris.', N'Scarpino', N'S', CAST(4950.80 AS Decimal(18, 2)), NULL, N'Hombre', 42, NULL, 7, 3)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (10, 4, N'siena acordonado negro', N'Mocasin', N'Cuero azul engrasado, apliques en negro y gris.', N'Scarpino', N'S', CAST(4950.80 AS Decimal(18, 2)), NULL, N'Hombre', 43, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (11, 5, N'siena acordonado negro', N'Mocasin', N'Cuero azul engrasado, apliques en negro y gris.', N'Scarpino', N'S', CAST(4950.80 AS Decimal(18, 2)), NULL, N'Hombre', 44, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (12, 6, N'siena acordonado negro', N'Mocasin', N'Cuero azul engrasado, apliques en negro y gris.', N'Scarpino', N'S', CAST(4950.80 AS Decimal(18, 2)), NULL, N'Hombre', 45, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (13, 7, N'siena acordonado negro', N'Mocasin', N'Cuero azul engrasado, apliques en negro y gris.', N'Scarpino', N'S', CAST(4950.80 AS Decimal(18, 2)), NULL, N'Hombre', 46, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (27, 21, N'siena slack azul', N'Mocasin', N'Cuero engrasado azul, apliques y elastico gris.', N'Scarpino', N'S', CAST(5500.00 AS Decimal(18, 2)), NULL, N'Hombre', 39, NULL, 30, 8)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (28, 22, N'siena slack azul', N'Mocasin', N'Cuero engrasado azul, apliques y elastico gris.', N'Scarpino', N'S', CAST(5500.00 AS Decimal(18, 2)), NULL, N'Hombre', 40, NULL, 30, 8)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (29, 23, N'siena slack azul', N'Mocasin', N'Cuero engrasado azul, apliques y elastico gris.', N'Scarpino', N'S', CAST(5500.00 AS Decimal(18, 2)), NULL, N'Hombre', 41, NULL, 30, 6)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (30, 24, N'siena slack azul', N'Mocasin', N'Cuero engrasado azul, apliques y elastico gris.', N'Scarpino', N'S', CAST(5500.00 AS Decimal(18, 2)), NULL, N'Hombre', 42, NULL, 30, 6)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (31, 25, N'siena slack azul', N'Mocasin', N'Cuero engrasado azul, apliques y elastico gris.', N'Scarpino', N'S', CAST(5500.00 AS Decimal(18, 2)), NULL, N'Hombre', 43, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (32, 26, N'siena slack azul', N'Mocasin', N'Cuero engrasado azul, apliques y elastico gris.', N'Scarpino', N'S', CAST(5500.00 AS Decimal(18, 2)), NULL, N'Hombre', 44, NULL, 0, 3)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (33, 27, N'siena slack azul', N'Mocasin', N'Cuero engrasado azul, apliques y elastico gris.', N'Scarpino', N'S', CAST(5500.00 AS Decimal(18, 2)), NULL, N'Hombre', 45, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (34, 28, N'siena slack azul', N'Mocasin', N'Cuero engrasado azul, apliques y elastico gris.', N'Scarpino', N'S', CAST(5500.00 AS Decimal(18, 2)), NULL, N'Hombre', 46, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (48, 42, N'verona acordonado marron', N'Mocasin', N'Cuero engrasado marron, apliques beige.', N'Scarpino', N'S', CAST(4600.50 AS Decimal(18, 2)), NULL, N'Hombre', 39, NULL, 12, 18)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (49, 43, N'verona acordonado marron', N'Mocasin', N'Cuero engrasado marron, apliques beige.', N'Scarpino', N'S', CAST(4600.50 AS Decimal(18, 2)), NULL, N'Hombre', 40, NULL, 1, 9)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (50, 44, N'verona acordonado marron', N'Mocasin', N'Cuero engrasado marron, apliques beige.', N'Scarpino', N'S', CAST(4600.50 AS Decimal(18, 2)), NULL, N'Hombre', 41, NULL, 3, 7)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (51, 45, N'verona acordonado marron', N'Mocasin', N'Cuero engrasado marron, apliques beige.', N'Scarpino', N'S', CAST(4600.50 AS Decimal(18, 2)), NULL, N'Hombre', 42, NULL, 1, 9)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (52, 46, N'verona acordonado marron', N'Mocasin', N'Cuero engrasado marron, apliques beige.', N'Scarpino', N'S', CAST(4600.50 AS Decimal(18, 2)), NULL, N'Hombre', 43, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (53, 47, N'verona acordonado marron', N'Mocasin', N'Cuero engrasado marron, apliques beige.', N'Scarpino', N'S', CAST(4600.50 AS Decimal(18, 2)), NULL, N'Hombre', 44, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (54, 48, N'verona acordonado marron', N'Mocasin', N'Cuero engrasado marron, apliques beige.', N'Scarpino', N'S', CAST(4600.50 AS Decimal(18, 2)), NULL, N'Hombre', 45, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (55, 49, N'verona acordonado marron', N'Mocasin', N'Cuero engrasado marron, apliques beige.', N'Scarpino', N'S', CAST(4600.50 AS Decimal(18, 2)), NULL, N'Hombre', 46, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (56, 50, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(4600.75 AS Decimal(18, 2)), NULL, N'Hombre', 39, NULL, 26, 12)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (57, 51, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(4600.75 AS Decimal(18, 2)), NULL, N'Hombre', 40, NULL, 24, 6)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (58, 52, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(4600.75 AS Decimal(18, 2)), NULL, N'Hombre', 41, NULL, 27, 3)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (59, 53, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(4600.75 AS Decimal(18, 2)), NULL, N'Hombre', 42, NULL, 26, 4)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (60, 54, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(4600.75 AS Decimal(18, 2)), NULL, N'Hombre', 43, NULL, 10, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (61, 55, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(4600.75 AS Decimal(18, 2)), NULL, N'Hombre', 44, NULL, 12, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (62, 56, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(4600.75 AS Decimal(18, 2)), NULL, N'Hombre', 45, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (63, 57, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(4600.75 AS Decimal(18, 2)), NULL, N'Hombre', 46, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (69, 58, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(4200.00 AS Decimal(18, 2)), NULL, N'Dama', 34, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (70, 59, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(4200.00 AS Decimal(18, 2)), NULL, N'Dama', 35, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (71, 60, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(4200.00 AS Decimal(18, 2)), NULL, N'Dama', 36, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (72, 61, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(4200.00 AS Decimal(18, 2)), NULL, N'Dama', 37, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (73, 62, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(4200.00 AS Decimal(18, 2)), NULL, N'Dama', 38, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (74, 63, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(3600.20 AS Decimal(18, 2)), NULL, N'nino', 27, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (75, 64, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(3600.20 AS Decimal(18, 2)), NULL, N'nino', 28, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (76, 65, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(3600.20 AS Decimal(18, 2)), NULL, N'nino', 29, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (77, 66, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(3600.20 AS Decimal(18, 2)), NULL, N'nino', 30, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (78, 67, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(3600.20 AS Decimal(18, 2)), NULL, N'nino', 31, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (79, 68, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(3600.20 AS Decimal(18, 2)), NULL, N'nino', 32, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (80, 69, N'verona slack negro', N'Mocasin', N'Cuero engrasado negro, apliques y elastico gris.', N'Scarpino', N'S', CAST(3600.20 AS Decimal(18, 2)), NULL, N'nino', 33, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (81, 70, N'confort office slack negro', N'Zapato de vestir', N'Cuero floutter negro, forro negro.', N'Scarpino', N'S', CAST(5230.75 AS Decimal(18, 2)), NULL, N'Hombre', 39, NULL, 14, 20)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (82, 71, N'confort office slack negro', N'Zapato de vestir', N'Cuero floutter negro, forro negro.', N'Scarpino', N'S', CAST(5230.75 AS Decimal(18, 2)), NULL, N'Hombre', 40, NULL, 4, 23)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (83, 72, N'confort office slack negro', N'Zapato de vestir', N'Cuero floutter negro, forro negro.', N'Scarpino', N'S', CAST(5230.75 AS Decimal(18, 2)), NULL, N'Hombre', 41, NULL, 0, 10)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (84, 73, N'confort office slack negro', N'Zapato de vestir', N'Cuero floutter negro, forro negro.', N'Scarpino', N'S', CAST(5230.75 AS Decimal(18, 2)), NULL, N'Hombre', 42, NULL, 20, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (85, 74, N'confort office slack negro', N'Zapato de vestir', N'Cuero floutter negro, forro negro.', N'Scarpino', N'S', CAST(5230.75 AS Decimal(18, 2)), NULL, N'Hombre', 43, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (86, 75, N'confort office slack negro', N'Zapato de vestir', N'Cuero floutter negro, forro negro.', N'Scarpino', N'S', CAST(5230.75 AS Decimal(18, 2)), NULL, N'Hombre', 44, NULL, 0, 10)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (87, 76, N'confort office slack negro', N'Zapato de vestir', N'Cuero floutter negro, forro negro.', N'Scarpino', N'S', CAST(5230.75 AS Decimal(18, 2)), NULL, N'Hombre', 45, NULL, 6, 4)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (88, 77, N'confort office slack negro', N'Zapato de vestir', N'Cuero floutter negro, forro negro.', N'Scarpino', N'S', CAST(5230.75 AS Decimal(18, 2)), NULL, N'Hombre', 46, NULL, 8, 2)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (89, 78, N'siena acordonado negro', N'Mocasin', N'Cuero azul engrasado, apliques en negro y gris.', N'Scarpino', N'S', CAST(3600.00 AS Decimal(18, 2)), NULL, N'Dama', 34, NULL, 0, 20)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (90, 79, N'siena acordonado negro', N'Mocasin', N'Cuero azul engrasado, apliques en negro y gris.', N'Scarpino', N'S', CAST(3600.00 AS Decimal(18, 2)), NULL, N'Dama', 35, NULL, 0, 20)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (91, 80, N'siena acordonado negro', N'Mocasin', N'Cuero azul engrasado, apliques en negro y gris.', N'Scarpino', N'S', CAST(3600.00 AS Decimal(18, 2)), NULL, N'Dama', 36, NULL, 0, 20)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (92, 81, N'siena acordonado negro', N'Mocasin', N'Cuero azul engrasado, apliques en negro y gris.', N'Scarpino', N'S', CAST(3600.00 AS Decimal(18, 2)), NULL, N'Dama', 37, NULL, 10, 10)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (93, 82, N'siena acordonado negro', N'Mocasin', N'Cuero azul engrasado, apliques en negro y gris.', N'Scarpino', N'S', CAST(3600.00 AS Decimal(18, 2)), NULL, N'Dama', 38, NULL, 10, 10)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (94, 83, N'verona acordonado marron', N'Mocasin', N'Cuero engrasado marron, apliques beige.', N'Scarpino', N'S', CAST(3200.75 AS Decimal(18, 2)), NULL, N'Dama', 34, NULL, 0, 10)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (95, 84, N'verona acordonado marron', N'Mocasin', N'Cuero engrasado marron, apliques beige.', N'Scarpino', N'S', CAST(3200.75 AS Decimal(18, 2)), NULL, N'Dama', 35, NULL, 6, 2)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (96, 85, N'verona acordonado marron', N'Mocasin', N'Cuero engrasado marron, apliques beige.', N'Scarpino', N'S', CAST(3200.75 AS Decimal(18, 2)), NULL, N'Dama', 36, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (97, 86, N'verona acordonado marron', N'Mocasin', N'Cuero engrasado marron, apliques beige.', N'Scarpino', N'S', CAST(3200.75 AS Decimal(18, 2)), NULL, N'Dama', 37, NULL, 0, 0)
INSERT [dbo].[articulo] ([idarticulo], [codigoArticulo], [nombre], [tipoArticulo], [descripcion], [marca], [fabricacion], [precioUnitario], [idmateriaPrimaPorArticulo], [talleNombre], [talle], [idCantidadMPxArticulo], [cantidadEnStock], [reservado]) VALUES (98, 87, N'verona acordonado marron', N'Mocasin', N'Cuero engrasado marron, apliques beige.', N'Scarpino', N'S', CAST(3200.75 AS Decimal(18, 2)), NULL, N'Dama', 38, NULL, 0, 0)
SET IDENTITY_INSERT [dbo].[articulo] OFF
GO
SET IDENTITY_INSERT [dbo].[ArticulosGeneral] ON 

INSERT [dbo].[ArticulosGeneral] ([id], [nombre], [precioNino], [precioDama], [precioHombre]) VALUES (47, N'siena acordonado negro', NULL, CAST(3600.00 AS Decimal(18, 2)), CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[ArticulosGeneral] ([id], [nombre], [precioNino], [precioDama], [precioHombre]) VALUES (49, N'siena slack azul', NULL, NULL, CAST(5500.00 AS Decimal(18, 2)))
INSERT [dbo].[ArticulosGeneral] ([id], [nombre], [precioNino], [precioDama], [precioHombre]) VALUES (51, N'verona acordonado marron', NULL, CAST(3200.75 AS Decimal(18, 2)), CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[ArticulosGeneral] ([id], [nombre], [precioNino], [precioDama], [precioHombre]) VALUES (52, N'verona slack negro', CAST(3600.20 AS Decimal(18, 2)), CAST(4200.00 AS Decimal(18, 2)), CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[ArticulosGeneral] ([id], [nombre], [precioNino], [precioDama], [precioHombre]) VALUES (54, N'confort office slack negro', NULL, NULL, CAST(5230.75 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[ArticulosGeneral] OFF
GO
SET IDENTITY_INSERT [dbo].[cantidadMPxArticulo] ON 

INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (87, N'siena acordonado negro', N'0,06', N'Cortado', 1004)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (88, N'siena acordonado negro', N'1000', N'Ojalillado', 1007)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (89, N'siena acordonado negro', N'0,20', N'Cortado', 1009)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (90, N'siena acordonado negro', N'1', N'Empaque', 1013)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (91, N'siena acordonado negro', N'1', N'Empaque', 1025)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (92, N'siena acordonado negro', N'1', N'Empaque', 1022)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (93, N'verona acordonado marron', N'3', N'Ojalillado', 1006)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (94, N'verona acordonado marron', N'0,20', N'Cortado', 1008)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (95, N'verona acordonado marron', N'1', N'Empaque', 1010)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (96, N'verona acordonado marron', N'1', N'Empaque', 1023)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (97, N'verona acordonado marron', N'1', N'Empaque', 1013)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (98, N'verona slack negro', N'0,13', N'Cortado', 1004)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (99, N'verona slack negro', N'3', N'Ojalillado', 1007)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (100, N'verona slack negro', N'0,20', N'Cortado', 1009)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (101, N'verona slack negro', N'1', N'Empaque', 1025)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (102, N'verona slack negro', N'1', N'Empaque', 1013)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (103, N'confort office slack negro', N'0,20', N'Cortado', 1003)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (104, N'confort office slack negro', N'0,06', N'Cortado', 1004)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (105, N'confort office slack negro', N'3', N'Ojalillado', 1006)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (106, N'confort office slack negro', N'1', N'Empaque', 1016)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (107, N'confort office slack negro', N'1', N'Empaque', 1022)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (108, N'confort office slack negro', N'1', N'Empaque', 1025)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (112, N'siena slack azul', N'0,20', N'Cortado', 1011)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (113, N'siena slack azul', N'0,08', N'Cortado', 1012)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (114, N'siena slack azul', N'1', N'Empaque', 1013)
INSERT [dbo].[cantidadMPxArticulo] ([id], [articulo], [cantidad], [area], [idMateriaPrima]) VALUES (115, N'siena slack azul', N'2', N'Ojalillado', 1015)
SET IDENTITY_INSERT [dbo].[cantidadMPxArticulo] OFF
GO
SET IDENTITY_INSERT [dbo].[cantidadMPxPedido] ON 

INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (27, 19, N'confort office slack negro', N'2,80 Mts', N'Cortado', 14, 1003)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (28, 19, N'confort office slack negro', N'0,84 Mts', N'Cortado', 14, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (29, 19, N'confort office slack negro', N'4,2 Mt2', N'Empaque', 14, 1005)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (30, 19, N'confort office slack negro', N'42 Gramos', N'Ojalillado', 14, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (31, 10, N'confort office slack negro', N'3,60 Mts', N'Cortado', 18, 1003)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (32, 10, N'confort office slack negro', N'1,08 Mts', N'Cortado', 18, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (33, 10, N'confort office slack negro', N'5,4 Mt2', N'Empaque', 18, 1005)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (34, 10, N'confort office slack negro', N'54 Gramos', N'Ojalillado', 18, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (35, 16, N'confort office slack negro', N'6,00 Mts', N'Cortado', 30, 1003)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (36, 16, N'confort office slack negro', N'1,80 Mts', N'Cortado', 30, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (37, 16, N'confort office slack negro', N'9,0 Mt2', N'Empaque', 30, 1005)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (38, 16, N'confort office slack negro', N'90 Gramos', N'Ojalillado', 30, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (42, 23, N'verona slack negro', N'3,20 Mts', N'Cortado', 32, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (43, 23, N'verona slack negro', N'96 Gramos', N'Ojalillado', 32, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (44, 23, N'verona slack negro', N'6,40 Mts', N'Cortado', 32, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (45, 23, N'verona slack negro', N'32 Unidades', N'Empaque', 32, 1010)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (50, 25, N'verona slack negro', N'1,00 Mts', N'Cortado', 10, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (51, 25, N'verona slack negro', N'30 Gramos', N'Ojalillado', 10, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (52, 25, N'verona slack negro', N'2,00 Mts', N'Cortado', 10, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (53, 25, N'verona slack negro', N'10 Unidades', N'Empaque', 10, 1010)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (54, 26, N'verona slack negro', N'0,40 Mts', N'Cortado', 4, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (55, 26, N'verona slack negro', N'12 Gramos', N'Ojalillado', 4, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (56, 26, N'verona slack negro', N'0,80 Mts', N'Cortado', 4, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (57, 26, N'verona slack negro', N'4 Unidades', N'Empaque', 4, 1010)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (58, 27, N'verona slack negro', N'1,00 Mts', N'Cortado', 10, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (59, 27, N'verona slack negro', N'30 Gramos', N'Ojalillado', 10, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (60, 27, N'verona slack negro', N'2,00 Mts', N'Cortado', 10, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (61, 27, N'verona slack negro', N'10 Unidades', N'Empaque', 10, 1010)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (66, 21, N'verona acordonado marron', N'21 Gramos', N'Ojalillado', 7, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (67, 21, N'verona acordonado marron', N'21 Gramos', N'Ojalillado', 7, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (68, 21, N'verona acordonado marron', N'1,40 Mts', N'Cortado', 7, 1008)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (69, 21, N'verona acordonado marron', N'1,40 Mts', N'Cortado', 7, 1008)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (70, 21, N'verona acordonado marron', N'7 Unidades', N'Empaque', 7, 1010)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (71, 31, N'confort office slack negro', N'4,80 Mts', N'Cortado', 24, 1003)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (72, 31, N'confort office slack negro', N'1,44 Mts', N'Cortado', 24, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (73, 31, N'confort office slack negro', N'7,2 Mt2', N'Empaque', 24, 1005)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (74, 31, N'confort office slack negro', N'72 Gramos', N'Ojalillado', 24, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (75, 31, N'confort office slack negro', N'24 Ps', N'Empaque', 24, 1016)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (76, 24, N'verona slack negro', N'3,00 Mts', N'Cortado', 30, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (77, 24, N'verona slack negro', N'90 Gramos', N'Ojalillado', 30, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (78, 24, N'verona slack negro', N'6,00 Mts', N'Cortado', 30, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (79, 24, N'verona slack negro', N'30 Unidades', N'Empaque', 30, 1010)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (80, 30, N'siena slack azul', N'1,40 Mts', N'Cortado', 7, 1011)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (81, 30, N'siena slack azul', N'0,56 Mts', N'Cortado', 7, 1012)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (82, 30, N'siena slack azul', N'7 Ps', N'Empaque', 7, 1013)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (83, 30, N'siena slack azul', N'14 Gramos', N'Ojalillado', 7, 1015)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (153, 44, N'confort office slack negro', N'5,40 Mts', N'Cortado', 27, 1003)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (154, 44, N'confort office slack negro', N'1,62 Mts', N'Cortado', 27, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (155, 44, N'confort office slack negro', N'8,1 Mt2', N'Empaque', 27, 1005)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (156, 44, N'confort office slack negro', N'81 Gramos', N'Ojalillado', 27, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (157, 44, N'confort office slack negro', N'27 Ps', N'Empaque', 27, 1016)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (158, 43, N'confort office slack negro', N'6,20 Mts', N'Cortado', 31, 1003)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (159, 43, N'confort office slack negro', N'1,86 Mts', N'Cortado', 31, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (160, 43, N'confort office slack negro', N'9,3 Mt2', N'Empaque', 31, 1005)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (161, 43, N'confort office slack negro', N'93 Gramos', N'Ojalillado', 31, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (162, 43, N'confort office slack negro', N'31 Ps', N'Empaque', 31, 1016)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (167, 41, N'verona slack negro', N'2,20 Mts', N'Cortado', 22, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (168, 41, N'verona slack negro', N'66 Gramos', N'Ojalillado', 22, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (169, 41, N'verona slack negro', N'4,40 Mts', N'Cortado', 22, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (170, 41, N'verona slack negro', N'22 Unidades', N'Empaque', 22, 1010)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (171, 47, N'siena slack azul', N'2,40 Mts', N'Cortado', 12, 1011)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (172, 47, N'siena slack azul', N'0,96 Mts', N'Cortado', 12, 1012)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (173, 47, N'siena slack azul', N'12 Ps', N'Empaque', 12, 1013)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (174, 47, N'siena slack azul', N'24 Gramos', N'Ojalillado', 12, 1015)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (175, 45, N'siena slack azul', N'3,40 Mts', N'Cortado', 17, 1011)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (176, 45, N'siena slack azul', N'1,36 Mts', N'Cortado', 17, 1012)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (177, 45, N'siena slack azul', N'17 Ps', N'Empaque', 17, 1013)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (178, 45, N'siena slack azul', N'34 Gramos', N'Ojalillado', 17, 1015)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (179, 40, N'verona slack negro', N'1,50 Mts', N'Cortado', 15, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (180, 40, N'verona slack negro', N'45 Gramos', N'Ojalillado', 15, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (181, 40, N'verona slack negro', N'3,00 Mts', N'Cortado', 15, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (182, 40, N'verona slack negro', N'15 Unidades', N'Empaque', 15, 1010)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (183, 46, N'siena acordonado negro', N'1,68 Mts', N'Cortado', 28, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (184, 46, N'siena acordonado negro', N'28000 Gramos', N'Ojalillado', 28, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (185, 46, N'siena acordonado negro', N'5,60 Mts', N'Cortado', 28, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (186, 38, N'confort office slack negro', N'2,60 Mts', N'Cortado', 13, 1003)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (187, 38, N'confort office slack negro', N'0,78 Mts', N'Cortado', 13, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (188, 38, N'confort office slack negro', N'3,9 Mt2', N'Empaque', 13, 1005)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (189, 38, N'confort office slack negro', N'39 Gramos', N'Ojalillado', 13, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (190, 38, N'confort office slack negro', N'13 Ps', N'Empaque', 13, 1016)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (191, 37, N'siena acordonado negro', N'1,14 Mts', N'Cortado', 19, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (192, 37, N'siena acordonado negro', N'19000 Gramos', N'Ojalillado', 19, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (193, 37, N'siena acordonado negro', N'3,80 Mts', N'Cortado', 19, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (194, 48, N'siena acordonado negro', N'2,40 Mts', N'Cortado', 40, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (195, 48, N'siena acordonado negro', N'40000 Gramos', N'Ojalillado', 40, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (196, 48, N'siena acordonado negro', N'8,00 Mts', N'Cortado', 40, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (197, 51, N'siena slack azul', N'4,80 Mts', N'Cortado', 24, 1011)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (198, 51, N'siena slack azul', N'1,92 Mts', N'Cortado', 24, 1012)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (199, 51, N'siena slack azul', N'24 Ps', N'Empaque', 24, 1013)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (200, 51, N'siena slack azul', N'48 Gramos', N'Ojalillado', 24, 1015)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (201, 50, N'siena acordonado negro', N'1,02 Mts', N'Cortado', 17, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (202, 50, N'siena acordonado negro', N'17000 Gramos', N'Ojalillado', 17, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (203, 50, N'siena acordonado negro', N'3,40 Mts', N'Cortado', 17, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (204, 34, N'siena acordonado negro', N'0,12 Mts', N'Cortado', 2, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (205, 34, N'siena acordonado negro', N'2000 Gramos', N'Ojalillado', 2, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (206, 34, N'siena acordonado negro', N'0,40 Mts', N'Cortado', 2, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (207, 54, N'verona slack negro', N'6,50 Mts', N'Cortado', 50, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (208, 54, N'verona slack negro', N'150 Gramos', N'Ojalillado', 50, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (209, 54, N'verona slack negro', N'10,00 Mts', N'Cortado', 50, 1009)
GO
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (210, 54, N'verona slack negro', N'50 Unidades', N'Empaque', 50, 1010)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (211, 53, N'siena slack azul', N'5,20 Mts', N'Cortado', 26, 1011)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (212, 53, N'siena slack azul', N'2,08 Mts', N'Cortado', 26, 1012)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (213, 53, N'siena slack azul', N'26 Ps', N'Empaque', 26, 1013)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (214, 53, N'siena slack azul', N'52 Gramos', N'Ojalillado', 26, 1015)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (215, 59, N'verona acordonado marron', N'27 Gramos', N'Ojalillado', 9, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (216, 59, N'verona acordonado marron', N'1,80 Mts', N'Cortado', 9, 1008)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (217, 59, N'verona acordonado marron', N'9 Unidades', N'Empaque', 9, 1010)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (218, 59, N'verona acordonado marron', N'9 Ps', N'Empaque', 9, 1013)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (219, 59, N'verona acordonado marron', N'9 Ps', N'Empaque', 9, 1023)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (220, 60, N'siena slack azul', N'3,20 Mts', N'Cortado', 16, 1011)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (221, 60, N'siena slack azul', N'1,28 Mts', N'Cortado', 16, 1012)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (222, 60, N'siena slack azul', N'16 Ps', N'Empaque', 16, 1013)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (223, 60, N'siena slack azul', N'32 Gramos', N'Ojalillado', 16, 1015)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (224, 58, N'verona slack negro', N'1,82 Mts', N'Cortado', 14, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (225, 58, N'verona slack negro', N'42 Gramos', N'Ojalillado', 14, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (226, 58, N'verona slack negro', N'2,80 Mts', N'Cortado', 14, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (227, 58, N'verona slack negro', N'14 Ps', N'Empaque', 14, 1013)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (228, 58, N'verona slack negro', N'14 Unidades', N'Empaque', 14, 1025)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (229, 57, N'verona acordonado marron', N'72 Gramos', N'Ojalillado', 24, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (230, 57, N'verona acordonado marron', N'4,80 Mts', N'Cortado', 24, 1008)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (231, 57, N'verona acordonado marron', N'24 Unidades', N'Empaque', 24, 1010)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (232, 57, N'verona acordonado marron', N'24 Ps', N'Empaque', 24, 1013)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (233, 57, N'verona acordonado marron', N'24 Ps', N'Empaque', 24, 1023)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (234, 61, N'siena acordonado negro', N'1,38 Mts', N'Cortado', 23, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (235, 61, N'siena acordonado negro', N'23000 Gramos', N'Ojalillado', 23, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (236, 61, N'siena acordonado negro', N'4,60 Mts', N'Cortado', 23, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (237, 61, N'siena acordonado negro', N'23 Ps', N'Empaque', 23, 1013)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (238, 61, N'siena acordonado negro', N'23 Ps', N'Empaque', 23, 1022)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (239, 61, N'siena acordonado negro', N'23 Unidades', N'Empaque', 23, 1025)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (240, 62, N'verona acordonado marron', N'63 Gramos', N'Ojalillado', 21, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (241, 62, N'verona acordonado marron', N'4,20 Mts', N'Cortado', 21, 1008)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (242, 62, N'verona acordonado marron', N'21 Unidades', N'Empaque', 21, 1010)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (243, 62, N'verona acordonado marron', N'21 Ps', N'Empaque', 21, 1013)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (244, 62, N'verona acordonado marron', N'21 Ps', N'Empaque', 21, 1023)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (245, 56, N'confort office slack negro', N'4,80 Mts', N'Cortado', 24, 1003)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (246, 56, N'confort office slack negro', N'1,44 Mts', N'Cortado', 24, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (247, 56, N'confort office slack negro', N'72 Gramos', N'Ojalillado', 24, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (248, 56, N'confort office slack negro', N'24 Ps', N'Empaque', 24, 1016)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (249, 56, N'confort office slack negro', N'24 Ps', N'Empaque', 24, 1022)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (250, 56, N'confort office slack negro', N'24 Unidades', N'Empaque', 24, 1025)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (251, 52, N'siena acordonado negro', N'0,36 Mts', N'Cortado', 6, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (252, 52, N'siena acordonado negro', N'6000 Gramos', N'Ojalillado', 6, 1007)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (253, 52, N'siena acordonado negro', N'1,20 Mts', N'Cortado', 6, 1009)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (254, 52, N'siena acordonado negro', N'6 Ps', N'Empaque', 6, 1013)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (255, 52, N'siena acordonado negro', N'6 Ps', N'Empaque', 6, 1022)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (256, 52, N'siena acordonado negro', N'6 Unidades', N'Empaque', 6, 1025)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (257, 64, N'verona acordonado marron', N'45 Gramos', N'Ojalillado', 15, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (258, 64, N'verona acordonado marron', N'3,00 Mts', N'Cortado', 15, 1008)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (259, 64, N'verona acordonado marron', N'15 Unidades', N'Empaque', 15, 1010)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (260, 64, N'verona acordonado marron', N'15 Ps', N'Empaque', 15, 1013)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (261, 64, N'verona acordonado marron', N'15 Ps', N'Empaque', 15, 1023)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (262, 70, N'confort office slack negro', N'0,60 Mts', N'Cortado', 3, 1003)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (263, 70, N'confort office slack negro', N'0,18 Mts', N'Cortado', 3, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (264, 70, N'confort office slack negro', N'9 Gramos', N'Ojalillado', 3, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (265, 70, N'confort office slack negro', N'3 Ps', N'Empaque', 3, 1016)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (266, 70, N'confort office slack negro', N'3 Ps', N'Empaque', 3, 1022)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (267, 70, N'confort office slack negro', N'3 Unidades', N'Empaque', 3, 1025)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (268, 70, N'confort office slack negro', N'0,60 Mts', N'Cortado', 3, 1003)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (269, 70, N'confort office slack negro', N'0,18 Mts', N'Cortado', 3, 1004)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (270, 70, N'confort office slack negro', N'9 Gramos', N'Ojalillado', 3, 1006)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (271, 70, N'confort office slack negro', N'3 Ps', N'Empaque', 3, 1016)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (272, 70, N'confort office slack negro', N'3 Ps', N'Empaque', 3, 1022)
INSERT [dbo].[cantidadMPxPedido] ([id], [numeroPedido], [articulo], [cantidadXpares], [area], [totalPares], [idMateriaPrima]) VALUES (273, 70, N'confort office slack negro', N'3 Unidades', N'Empaque', 3, 1025)
SET IDENTITY_INSERT [dbo].[cantidadMPxPedido] OFF
GO
SET IDENTITY_INSERT [dbo].[cliente] ON 

INSERT [dbo].[cliente] ([id], [razonSocial], [cuit], [mail], [telefono], [ingresosBrutos], [condicionIva], [domicilioDeEntrega], [codigoPostal], [calle], [altura], [barrio], [idLocalidad], [idProvincia], [idCredito], [idDescuento], [idJurisdiccion], [idTransportista]) VALUES (5, N'Amanda SA', N'30-70973569-8', N'ventasAmanda@gmail.com', N'(0351) 15-640560', N'30-70973569-8-924', N'Responsable inscripto', N'Santa Isabel 320 Villa Leon', NULL, N'Santa Isabel', 327, N'Santos', 1, 22, 7, 2, 25, 3)
INSERT [dbo].[cliente] ([id], [razonSocial], [cuit], [mail], [telefono], [ingresosBrutos], [condicionIva], [domicilioDeEntrega], [codigoPostal], [calle], [altura], [barrio], [idLocalidad], [idProvincia], [idCredito], [idDescuento], [idJurisdiccion], [idTransportista]) VALUES (6, N'Mary shoes', N'20-12674539-8', N'maryshoesventas@gmail.com', N'(351)-466606', N'20-12674539-8-904', N'Responsable inscripto', N'Tedin 111 esq Hoggigins pacheco', NULL, N'Peripi esq RIO IV', 1386, N'Parque Patricios', 1, 22, 5, 2, 5, 3)
INSERT [dbo].[cliente] ([id], [razonSocial], [cuit], [mail], [telefono], [ingresosBrutos], [condicionIva], [domicilioDeEntrega], [codigoPostal], [calle], [altura], [barrio], [idLocalidad], [idProvincia], [idCredito], [idDescuento], [idJurisdiccion], [idTransportista]) VALUES (7, N'Briganti calzados', N'30-70973569-8', N'BrigantiVentas@gmail.com', N'(351) 3891760', N'30-70973569-8-902', N'Responsable inscripto', N'San Martin 910 Caballito', NULL, N'San Martin', 910, N'Central', 1, 22, 4, 1, 3, 3)
SET IDENTITY_INSERT [dbo].[cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[credito] ON 

INSERT [dbo].[credito] ([idcreditO], [estado], [limite]) VALUES (1, NULL, 0)
INSERT [dbo].[credito] ([idcreditO], [estado], [limite]) VALUES (2, NULL, 200000)
INSERT [dbo].[credito] ([idcreditO], [estado], [limite]) VALUES (3, NULL, 400000)
INSERT [dbo].[credito] ([idcreditO], [estado], [limite]) VALUES (4, NULL, 600000)
INSERT [dbo].[credito] ([idcreditO], [estado], [limite]) VALUES (5, NULL, 800000)
INSERT [dbo].[credito] ([idcreditO], [estado], [limite]) VALUES (6, NULL, 900000)
INSERT [dbo].[credito] ([idcreditO], [estado], [limite]) VALUES (7, NULL, 10000)
SET IDENTITY_INSERT [dbo].[credito] OFF
GO
SET IDENTITY_INSERT [dbo].[descuento] ON 

INSERT [dbo].[descuento] ([iddescuento], [descripcion]) VALUES (1, N'0%')
INSERT [dbo].[descuento] ([iddescuento], [descripcion]) VALUES (2, N'10%')
INSERT [dbo].[descuento] ([iddescuento], [descripcion]) VALUES (3, N'15%')
INSERT [dbo].[descuento] ([iddescuento], [descripcion]) VALUES (4, N'20%')
INSERT [dbo].[descuento] ([iddescuento], [descripcion]) VALUES (5, N'25%')
SET IDENTITY_INSERT [dbo].[descuento] OFF
GO
SET IDENTITY_INSERT [dbo].[detalleDePedido] ON 

INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (156, 10, 10, 82, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (157, 10, 10, 81, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (158, 11, 2, 7, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (159, 11, 6, 6, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (161, 13, 11, 6, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (162, 14, 11, 82, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (163, 14, 6, 81, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (164, 15, 8, 58, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (165, 15, 6, 57, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (166, 15, 3, 56, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (167, 16, 10, 82, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (168, 16, 20, 81, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (169, 17, 6, 51, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (170, 17, 6, 50, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (171, 17, 4, 49, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (172, 17, 3, 48, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (173, 18, 3, 9, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (174, 18, 3, 8, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (175, 18, 3, 7, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (176, 18, 3, 6, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (177, 19, 2, 88, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (178, 19, 2, 87, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (179, 19, 10, 86, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (180, 20, 6, 51, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (181, 20, 4, 50, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (182, 20, 3, 49, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (183, 20, 3, 48, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (184, 21, 7, 50, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (185, 21, 3, 49, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (186, 21, 2, 48, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (187, 22, 4, 83, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (188, 22, 6, 82, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (189, 22, 6, 81, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (190, 23, 6, 80, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (191, 23, 6, 79, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (192, 23, 10, 78, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (193, 23, 10, 77, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (194, 24, 10, 71, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (195, 24, 10, 70, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (196, 24, 10, 69, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (197, 25, 3, 58, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (198, 25, 3, 57, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (199, 25, 6, 56, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (200, 26, 1, 58, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (201, 26, 2, 57, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (202, 26, 3, 56, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (203, 27, 3, 73, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (204, 27, 6, 72, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (205, 27, 5, 71, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (209, 29, 6, 30, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (210, 29, 6, 29, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (211, 29, 4, 28, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (212, 29, 4, 27, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (213, 30, 1, 34, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (214, 30, 1, 33, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (215, 30, 6, 32, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (216, 30, 2, 31, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (217, 31, 6, 84, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (218, 31, 10, 83, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (219, 31, 12, 82, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (220, 31, 12, 81, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (221, 32, 3, 8, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (222, 32, 2, 7, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (223, 32, 4, 6, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (224, 33, 2, 6, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (225, 34, 2, 6, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (226, 35, 2, 87, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (227, 36, 12, 74, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (234, 28, 6, 58, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (261, 41, 10, 72, CAST(3500.10 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (262, 41, 6, 71, CAST(3500.10 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (263, 41, 3, 70, CAST(3500.10 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (264, 41, 3, 69, CAST(3500.10 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (265, 42, 10, 81, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (266, 42, 6, 82, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (267, 42, 5, 83, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (268, 43, 0, 85, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (269, 43, 6, 84, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (270, 43, 5, 83, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (271, 43, 10, 82, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (272, 43, 10, 81, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (273, 44, 2, 85, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (274, 44, 5, 86, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (275, 44, 10, 87, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (276, 44, 10, 88, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (281, 45, 5, 27, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (282, 45, 10, 28, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (283, 45, 6, 29, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (284, 45, 4, 30, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (285, 46, 10, 89, CAST(3600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (286, 46, 10, 90, CAST(3600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (287, 46, 8, 91, CAST(3600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (288, 40, 15, 72, CAST(3500.10 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (289, 38, 13, 83, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (290, 37, 22, 6, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (291, 12, 1, 6, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (292, 47, 6, 32, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (293, 47, 6, 33, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (294, 48, 10, 6, CAST(4950.80 AS Decimal(18, 2)))
GO
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (295, 48, 10, 7, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (296, 48, 10, 8, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (297, 48, 10, 9, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (310, 49, 6, 48, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (311, 49, 6, 49, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (312, 49, 3, 50, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (313, 49, 3, 51, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (314, 50, 4, 6, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (315, 50, 4, 7, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (316, 50, 3, 8, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (317, 50, 6, 9, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (323, 51, 6, 27, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (324, 51, 6, 28, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (325, 51, 5, 29, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (326, 51, 4, 30, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (327, 51, 3, 31, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (328, 52, 3, 6, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (329, 52, 3, 7, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (330, 52, 6, 8, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (331, 53, 10, 27, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (332, 53, 6, 28, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (333, 53, 4, 29, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (334, 53, 6, 30, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (335, 54, 10, 56, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (336, 54, 10, 57, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (337, 54, 10, 58, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (338, 54, 10, 59, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (339, 54, 10, 60, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (345, 55, 10, 89, CAST(3600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (346, 55, 10, 90, CAST(3600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (347, 55, 10, 91, CAST(3600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (348, 55, 5, 92, CAST(3600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (349, 55, 5, 93, CAST(3600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (350, 56, 6, 81, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (351, 56, 6, 82, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (352, 56, 4, 83, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (353, 56, 8, 84, CAST(4600.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (354, 57, 4, 94, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (355, 57, 4, 95, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (356, 57, 6, 96, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (357, 57, 9, 97, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (358, 57, 1, 98, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (359, 58, 1, 69, CAST(4200.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (360, 58, 2, 70, CAST(4200.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (361, 58, 6, 71, CAST(4200.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (362, 58, 5, 72, CAST(4200.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (363, 59, 1, 94, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (364, 59, 2, 95, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (365, 59, 6, 96, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (366, 59, 3, 97, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (367, 60, 4, 28, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (368, 60, 4, 29, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (369, 60, 6, 30, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (370, 60, 9, 31, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (371, 60, 1, 32, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (375, 61, 6, 6, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (376, 61, 8, 7, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (377, 61, 9, 8, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (383, 62, 3, 98, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (384, 62, 3, 97, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (385, 62, 6, 96, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (386, 62, 9, 95, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (387, 62, 9, 94, CAST(4500.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (388, 63, 6, 6, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (389, 64, 9, 48, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (390, 64, 9, 49, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (391, 64, 6, 50, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (392, 65, 4, 6, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (393, 65, 4, 7, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (394, 65, 3, 8, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (395, 65, 9, 9, CAST(4950.80 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (396, 66, 6, 27, CAST(5500.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (397, 66, 6, 28, CAST(5500.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (398, 66, 5, 29, CAST(5500.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (399, 66, 5, 30, CAST(5500.00 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (400, 67, 3, 48, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (401, 67, 3, 49, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (402, 67, 6, 50, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (403, 67, 5, 51, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (404, 68, 6, 56, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (405, 68, 6, 57, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (406, 68, 3, 58, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (407, 68, 4, 59, CAST(4600.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (408, 69, 6, 81, CAST(5230.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (409, 69, 6, 82, CAST(5230.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (410, 69, 3, 83, CAST(5230.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (411, 70, 6, 81, CAST(5230.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (412, 70, 6, 82, CAST(5230.75 AS Decimal(18, 2)))
INSERT [dbo].[detalleDePedido] ([id], [numero], [cantidad], [idArticulo], [precio]) VALUES (413, 70, 3, 83, CAST(5230.75 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[detalleDePedido] OFF
GO
SET IDENTITY_INSERT [dbo].[detalleFactura] ON 

INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (12, 265, 10)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (13, 266, 11)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (14, 267, 15)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (15, 268, 16)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (16, 269, 12)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (17, 270, 51)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (18, 271, 14)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (19, 272, 33)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (20, 273, 18)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (21, 274, 19)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (22, 275, 59)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (23, 276, 20)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (24, 277, 26)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (25, 278, 61)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (26, 279, 21)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (27, 280, 22)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (28, 281, 23)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (29, 282, 24)
INSERT [dbo].[detalleFactura] ([id], [numero], [idPedido]) VALUES (30, 283, 25)
SET IDENTITY_INSERT [dbo].[detalleFactura] OFF
GO
SET IDENTITY_INSERT [dbo].[detalleOrdenDeCompra] ON 

INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (58, 0, N'1000', N'3', CAST(4600.00 AS Decimal(18, 2)), CAST(13800.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (59, 1, N'1001', N'2', CAST(4600.80 AS Decimal(18, 2)), CAST(9201.60 AS Decimal(18, 2)), 2)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (60, 2, N'1011', N'3', CAST(12599.99 AS Decimal(18, 2)), CAST(37799.97 AS Decimal(18, 2)), 12)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (61, 2, N'1009', N'15', CAST(10600.50 AS Decimal(18, 2)), CAST(159007.50 AS Decimal(18, 2)), 10)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (62, 3, N'1017', N'3', CAST(1500.50 AS Decimal(18, 2)), CAST(4501.50 AS Decimal(18, 2)), 18)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (63, 3, N'1014', N'3', CAST(1500.50 AS Decimal(18, 2)), CAST(4501.50 AS Decimal(18, 2)), 15)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (64, 4, N'1004', N'9', CAST(2680.00 AS Decimal(18, 2)), CAST(24120.00 AS Decimal(18, 2)), 5)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (65, 5, N'1000', N'2', CAST(1850.00 AS Decimal(18, 2)), CAST(3700.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (66, 5, N'1001', N'2', CAST(1300.00 AS Decimal(18, 2)), CAST(2600.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (67, 5, N'1002', N'1', CAST(4600.00 AS Decimal(18, 2)), CAST(4600.00 AS Decimal(18, 2)), 3)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (68, 6, N'1000', N'3', CAST(1850.00 AS Decimal(18, 2)), CAST(5550.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (69, 6, N'1001', N'4', CAST(1906.00 AS Decimal(18, 2)), CAST(7624.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (70, 6, N'1002', N'2', CAST(4600.20 AS Decimal(18, 2)), CAST(9200.40 AS Decimal(18, 2)), 3)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (71, 7, N'1001', N'5', CAST(1500.60 AS Decimal(18, 2)), CAST(7503.00 AS Decimal(18, 2)), 2)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (72, 8, N'1025', N'50', CAST(2600.50 AS Decimal(18, 2)), CAST(130025.00 AS Decimal(18, 2)), 26)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (73, 9, N'1003', N'3', CAST(12300.50 AS Decimal(18, 2)), CAST(36901.50 AS Decimal(18, 2)), 4)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (74, 9, N'1004', N'2', CAST(6890.00 AS Decimal(18, 2)), CAST(13780.00 AS Decimal(18, 2)), 5)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (75, 9, N'1008', N'3', CAST(11560.00 AS Decimal(18, 2)), CAST(34680.00 AS Decimal(18, 2)), 9)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (76, 10, N'1018', N'2', CAST(4600.50 AS Decimal(18, 2)), CAST(9201.00 AS Decimal(18, 2)), 19)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (77, 10, N'1021', N'1', CAST(4600.50 AS Decimal(18, 2)), CAST(4600.50 AS Decimal(18, 2)), 22)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (78, 10, N'1019', N'2', CAST(4600.50 AS Decimal(18, 2)), CAST(9201.00 AS Decimal(18, 2)), 20)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (79, 10, N'1018', N'1', CAST(4600.50 AS Decimal(18, 2)), CAST(4600.50 AS Decimal(18, 2)), 19)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (80, 10, N'1001', N'2', CAST(1500.60 AS Decimal(18, 2)), CAST(3001.20 AS Decimal(18, 2)), 2)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (81, 11, N'1000', N'2', CAST(1850.00 AS Decimal(18, 2)), CAST(3700.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (82, 11, N'1001', N'3', CAST(1500.60 AS Decimal(18, 2)), CAST(4501.80 AS Decimal(18, 2)), 2)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (83, 12, N'1002', N'2', CAST(4600.20 AS Decimal(18, 2)), CAST(9200.40 AS Decimal(18, 2)), 3)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (84, 13, N'1024', N'3', CAST(5680.00 AS Decimal(18, 2)), CAST(17040.00 AS Decimal(18, 2)), 25)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (85, 13, N'1025', N'2', CAST(2600.50 AS Decimal(18, 2)), CAST(5201.00 AS Decimal(18, 2)), 26)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (86, 14, N'1014', N'3', CAST(680.00 AS Decimal(18, 2)), CAST(2040.00 AS Decimal(18, 2)), 15)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (87, 14, N'1027', N'3', CAST(680.00 AS Decimal(18, 2)), CAST(2040.00 AS Decimal(18, 2)), 28)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (88, 14, N'1026', N'2', CAST(680.00 AS Decimal(18, 2)), CAST(1360.00 AS Decimal(18, 2)), 27)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (89, 15, N'1000', N'2', CAST(1850.00 AS Decimal(18, 2)), CAST(3700.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (90, 15, N'1001', N'3', CAST(1600.60 AS Decimal(18, 2)), CAST(4801.80 AS Decimal(18, 2)), 2)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (91, 16, N'1001', N'3', CAST(1600.60 AS Decimal(18, 2)), CAST(4801.80 AS Decimal(18, 2)), 2)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (92, 17, N'1000', N'2', CAST(1850.00 AS Decimal(18, 2)), CAST(3700.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (93, 17, N'1001', N'3', CAST(1800.60 AS Decimal(18, 2)), CAST(5401.80 AS Decimal(18, 2)), 2)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (94, 18, N'1000', N'2', CAST(1850.00 AS Decimal(18, 2)), CAST(3700.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (95, 18, N'1001', N'3', CAST(1950.50 AS Decimal(18, 2)), CAST(5851.50 AS Decimal(18, 2)), 2)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (96, 19, N'1000', N'3', CAST(1850.00 AS Decimal(18, 2)), CAST(5550.00 AS Decimal(18, 2)), 1)
INSERT [dbo].[detalleOrdenDeCompra] ([id], [numero], [codigoItem], [cantidad], [precioUnitario], [importeItem], [idMateriaPrima]) VALUES (97, 19, N'1001', N'3', CAST(1980.10 AS Decimal(18, 2)), CAST(5940.30 AS Decimal(18, 2)), 2)
SET IDENTITY_INSERT [dbo].[detalleOrdenDeCompra] OFF
GO
SET IDENTITY_INSERT [dbo].[detalleOrdenF] ON 

INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (1, 0, 10, 71)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (2, 0, 20, 70)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (3, 1, 11, 0)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (4, 2, 11, 71)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (5, 2, 6, 70)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (6, 3, 8, 52)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (7, 3, 6, 51)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (8, 3, 3, 50)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (9, 4, 6, 45)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (10, 4, 6, 44)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (11, 4, 4, 43)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (12, 4, 3, 42)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (13, 5, 3, 3)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (14, 5, 3, 2)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (15, 5, 0, 1)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (16, 5, 0, 0)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (17, 6, 2, 77)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (18, 6, 2, 76)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (19, 6, 10, 75)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (20, 7, 8, 71)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (21, 7, 10, 70)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (22, 8, 10, 71)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (23, 8, 20, 70)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (24, 9, 3, 3)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (25, 9, 3, 2)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (26, 9, 3, 1)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (27, 9, 3, 0)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (28, 10, 6, 69)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (29, 10, 6, 68)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (30, 10, 10, 67)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (31, 10, 10, 66)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (32, 11, 10, 60)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (33, 11, 10, 59)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (34, 11, 10, 58)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (35, 12, 3, 52)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (36, 12, 3, 51)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (37, 12, 4, 50)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (38, 13, 1, 52)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (39, 13, 1, 51)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (40, 13, 2, 50)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (41, 14, 2, 62)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (42, 14, 3, 61)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (43, 14, 5, 60)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (44, 15, 5, 55)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (45, 15, 8, 54)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (46, 15, 2, 53)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (47, 16, 7, 44)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (48, 17, 6, 73)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (49, 17, 4, 72)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (50, 17, 6, 71)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (51, 17, 8, 70)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (52, 18, 10, 60)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (53, 18, 10, 59)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (54, 18, 10, 58)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (55, 19, 1, 28)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (56, 19, 1, 27)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (57, 19, 3, 26)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (58, 19, 2, 25)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (59, 19, 6, 26)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (60, 19, 6, 27)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (61, 20, 10, 78)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (62, 20, 10, 79)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (63, 20, 8, 80)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (64, 21, 2, 74)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (65, 21, 5, 75)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (66, 21, 10, 76)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (67, 21, 10, 77)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (68, 22, 6, 73)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (69, 22, 5, 72)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (70, 22, 10, 71)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (71, 22, 10, 70)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (72, 23, 5, 21)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (73, 23, 10, 22)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (74, 23, 6, 23)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (75, 23, 4, 24)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (76, 24, 10, 61)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (77, 24, 6, 60)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (78, 24, 3, 59)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (79, 24, 3, 58)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (80, 25, 6, 26)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (81, 25, 6, 27)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (82, 26, 1, 21)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (83, 26, 6, 22)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (84, 26, 6, 23)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (85, 26, 4, 24)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (86, 27, 15, 61)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (87, 28, 10, 78)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (88, 28, 10, 79)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (89, 28, 8, 80)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (90, 29, 13, 72)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (91, 30, 19, 0)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (92, 31, 10, 0)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (93, 31, 10, 1)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (94, 31, 10, 2)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (95, 31, 10, 3)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (96, 32, 6, 21)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (97, 32, 6, 22)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (98, 32, 5, 23)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (99, 32, 4, 24)
GO
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (100, 32, 3, 25)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (101, 33, 4, 0)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (102, 33, 4, 1)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (103, 33, 3, 2)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (104, 33, 6, 3)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (105, 34, 2, 0)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (106, 35, 10, 50)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (107, 35, 10, 51)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (108, 35, 10, 52)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (109, 35, 10, 53)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (110, 35, 10, 54)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (111, 36, 10, 21)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (112, 36, 6, 22)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (113, 36, 4, 23)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (114, 36, 6, 24)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (115, 37, 6, 85)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (116, 37, 3, 86)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (117, 38, 6, 24)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (118, 38, 9, 25)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (119, 38, 1, 26)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (120, 39, 1, 58)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (121, 39, 2, 59)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (122, 39, 6, 60)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (123, 39, 5, 61)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (124, 40, 4, 83)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (125, 40, 4, 84)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (126, 40, 6, 85)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (127, 40, 9, 86)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (128, 40, 1, 87)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (129, 41, 6, 0)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (130, 41, 8, 1)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (131, 41, 9, 2)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (132, 42, 3, 87)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (133, 42, 3, 86)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (134, 42, 6, 85)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (135, 42, 9, 84)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (136, 43, 6, 70)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (137, 43, 6, 71)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (138, 43, 4, 72)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (139, 43, 8, 73)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (140, 44, 6, 2)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (141, 45, 9, 43)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (142, 45, 6, 44)
INSERT [dbo].[detalleOrdenF] ([id], [numero], [cantidad], [idArticulo]) VALUES (143, 46, 3, 72)
SET IDENTITY_INSERT [dbo].[detalleOrdenF] OFF
GO
INSERT [dbo].[domicilio] ([numero], [calle], [nombreBarrio], [idLocalidad], [idProvincia]) VALUES (123, N'Av 9 de Julio', N'San Cristóbal', 3, 23)
INSERT [dbo].[domicilio] ([numero], [calle], [nombreBarrio], [idLocalidad], [idProvincia]) VALUES (456, N'San Martin', N'Centro', 4, 23)
INSERT [dbo].[domicilio] ([numero], [calle], [nombreBarrio], [idLocalidad], [idProvincia]) VALUES (690, N'San Juan', N'Centro', 5, 34)
INSERT [dbo].[domicilio] ([numero], [calle], [nombreBarrio], [idLocalidad], [idProvincia]) VALUES (206, N'Santa Maria', N'Armada', 7, 40)
INSERT [dbo].[domicilio] ([numero], [calle], [nombreBarrio], [idLocalidad], [idProvincia]) VALUES (4651, N'Juan Rosario', N'Microcentro', 6, 36)
INSERT [dbo].[domicilio] ([numero], [calle], [nombreBarrio], [idLocalidad], [idProvincia]) VALUES (196, N'Sarmiento', N'Centro', 8, 36)
INSERT [dbo].[domicilio] ([numero], [calle], [nombreBarrio], [idLocalidad], [idProvincia]) VALUES (220, N'padre darbon', N'Ituzaingo', 8, 36)
INSERT [dbo].[domicilio] ([numero], [calle], [nombreBarrio], [idLocalidad], [idProvincia]) VALUES (314, N'Rivadavia', N'Centro', 12, 27)
INSERT [dbo].[domicilio] ([numero], [calle], [nombreBarrio], [idLocalidad], [idProvincia]) VALUES (1456, N'Alem', N'Rivadavia', 3, 22)
GO
SET IDENTITY_INSERT [dbo].[estadoOrdenC] ON 

INSERT [dbo].[estadoOrdenC] ([id], [descripcion]) VALUES (1, N'Registrada')
INSERT [dbo].[estadoOrdenC] ([id], [descripcion]) VALUES (2, N'Enviada')
INSERT [dbo].[estadoOrdenC] ([id], [descripcion]) VALUES (3, N'Anulada')
INSERT [dbo].[estadoOrdenC] ([id], [descripcion]) VALUES (4, N'Recibida')
SET IDENTITY_INSERT [dbo].[estadoOrdenC] OFF
GO
SET IDENTITY_INSERT [dbo].[estadoOrdenF] ON 

INSERT [dbo].[estadoOrdenF] ([id], [descripcion]) VALUES (1, N'Registrada')
INSERT [dbo].[estadoOrdenF] ([id], [descripcion]) VALUES (2, N'En proceso')
INSERT [dbo].[estadoOrdenF] ([id], [descripcion]) VALUES (3, N'Atrasada')
INSERT [dbo].[estadoOrdenF] ([id], [descripcion]) VALUES (4, N'Anulada')
INSERT [dbo].[estadoOrdenF] ([id], [descripcion]) VALUES (5, N'Finalizada')
SET IDENTITY_INSERT [dbo].[estadoOrdenF] OFF
GO
SET IDENTITY_INSERT [dbo].[estadoPedido] ON 

INSERT [dbo].[estadoPedido] ([id], [descripcion]) VALUES (1, N'Registrado')
INSERT [dbo].[estadoPedido] ([id], [descripcion]) VALUES (2, N'En fabricacion')
INSERT [dbo].[estadoPedido] ([id], [descripcion]) VALUES (3, N'A facturar')
INSERT [dbo].[estadoPedido] ([id], [descripcion]) VALUES (4, N'Remitado')
INSERT [dbo].[estadoPedido] ([id], [descripcion]) VALUES (5, N'Facturado')
INSERT [dbo].[estadoPedido] ([id], [descripcion]) VALUES (6, N'Despachado')
INSERT [dbo].[estadoPedido] ([id], [descripcion]) VALUES (7, N'Anulado')
SET IDENTITY_INSERT [dbo].[estadoPedido] OFF
GO
SET IDENTITY_INSERT [dbo].[factura] ON 

INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (12, 265, CAST(N'2023-06-19' AS Date), CAST(111332.10 AS Decimal(18, 2)), N'A', 21, CAST(19322.10 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 26589456, 6, 265, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (13, 266, CAST(N'2023-06-20' AS Date), CAST(45820.98 AS Decimal(18, 2)), N'A', 21, CAST(7728.84 AS Decimal(18, 2)), CAST(1288.14 AS Decimal(18, 2)), 26589457, 5, 266, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (14, 267, CAST(N'2023-07-19' AS Date), CAST(97369.59 AS Decimal(18, 2)), N'A', 21, CAST(16423.79 AS Decimal(18, 2)), CAST(2737.30 AS Decimal(18, 2)), 26589459, 6, 267, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (15, 268, CAST(N'2023-07-19' AS Date), CAST(166998.15 AS Decimal(18, 2)), N'A', 21, CAST(28983.15 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 26589460, 5, 268, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (16, 269, CAST(N'2023-07-22' AS Date), CAST(6163.75 AS Decimal(18, 2)), N'A', 21, CAST(1039.67 AS Decimal(18, 2)), CAST(173.28 AS Decimal(18, 2)), 26589461, 5, 269, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (17, 270, CAST(N'2023-07-22' AS Date), CAST(137470.41 AS Decimal(18, 2)), N'A', 21, CAST(23187.78 AS Decimal(18, 2)), CAST(3864.63 AS Decimal(18, 2)), 26589462, 7, 270, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (18, 271, CAST(N'2023-07-24' AS Date), CAST(94632.29 AS Decimal(18, 2)), N'A', 21, CAST(16423.79 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 26589458, 5, 271, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (19, 272, CAST(N'2023-07-24' AS Date), CAST(11455.25 AS Decimal(18, 2)), N'A', 21, CAST(1932.21 AS Decimal(18, 2)), CAST(322.04 AS Decimal(18, 2)), 26589464, 5, 272, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (20, 273, CAST(N'2023-07-26' AS Date), CAST(66799.26 AS Decimal(18, 2)), N'A', 21, CAST(11593.26 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 26589463, 6, 273, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (21, 274, CAST(N'2023-07-26' AS Date), CAST(77932.47 AS Decimal(18, 2)), N'A', 21, CAST(13525.47 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 26589465, 6, 274, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (22, 275, CAST(N'2023-07-27' AS Date), CAST(65350.89 AS Decimal(18, 2)), N'A', 21, CAST(11341.89 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 26589470, 6, 275, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (23, 276, CAST(N'2023-07-28' AS Date), CAST(89065.68 AS Decimal(18, 2)), N'A', 21, CAST(15457.68 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 26589466, 6, 276, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (24, 277, CAST(N'2023-07-28' AS Date), CAST(33399.63 AS Decimal(18, 2)), N'A', 21, CAST(5796.63 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 26589471, 5, 277, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (25, 278, CAST(N'2023-07-29' AS Date), CAST(141766.15 AS Decimal(18, 2)), N'A', 21, CAST(23912.36 AS Decimal(18, 2)), CAST(3985.39 AS Decimal(18, 2)), 26589472, 6, 278, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (26, 279, CAST(N'2023-07-29' AS Date), CAST(68731.47 AS Decimal(18, 2)), N'A', 21, CAST(11593.26 AS Decimal(18, 2)), CAST(1932.21 AS Decimal(18, 2)), 26589467, 5, 279, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (27, 280, CAST(N'2023-07-29' AS Date), CAST(91641.96 AS Decimal(18, 2)), N'A', 21, CAST(15457.68 AS Decimal(18, 2)), CAST(2576.28 AS Decimal(18, 2)), 26589468, 5, 280, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (28, 281, CAST(N'2023-07-30' AS Date), CAST(178131.36 AS Decimal(18, 2)), N'A', 21, CAST(30915.36 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 26589469, 6, 281, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (29, 282, CAST(N'2023-07-30' AS Date), CAST(166998.15 AS Decimal(18, 2)), N'A', 21, CAST(28983.15 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 26589473, 6, 282, 1)
INSERT [dbo].[factura] ([id], [numero], [fecha], [montoTotal], [tipoFactura], [PorcentajeAlicuotaIva], [importeIva], [importeTributos], [idRemito], [idCliente], [idDetalleFactura], [idFormaDePago]) VALUES (30, 283, CAST(N'2023-07-31' AS Date), CAST(66799.26 AS Decimal(18, 2)), N'A', 21, CAST(11593.26 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 26589475, 5, 283, 1)
SET IDENTITY_INSERT [dbo].[factura] OFF
GO
SET IDENTITY_INSERT [dbo].[formaDePago] ON 

INSERT [dbo].[formaDePago] ([id], [descripcion]) VALUES (1, N'Contado - Clausula Dolar')
INSERT [dbo].[formaDePago] ([id], [descripcion]) VALUES (2, N'Tarjeta de credito')
INSERT [dbo].[formaDePago] ([id], [descripcion]) VALUES (3, N'Cuenta corriente')
INSERT [dbo].[formaDePago] ([id], [descripcion]) VALUES (4, N'Cheque')
INSERT [dbo].[formaDePago] ([id], [descripcion]) VALUES (5, N'Otro')
SET IDENTITY_INSERT [dbo].[formaDePago] OFF
GO
SET IDENTITY_INSERT [dbo].[iva] ON 

INSERT [dbo].[iva] ([id], [descripcion]) VALUES (1, N'0')
INSERT [dbo].[iva] ([id], [descripcion]) VALUES (2, N'10,5')
INSERT [dbo].[iva] ([id], [descripcion]) VALUES (3, N'21')
INSERT [dbo].[iva] ([id], [descripcion]) VALUES (4, N'27')
SET IDENTITY_INSERT [dbo].[iva] OFF
GO
SET IDENTITY_INSERT [dbo].[jurisdiccion] ON 

INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (1, N' ')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (2, N'901 - Ciudad autonoma de Buenos Aires')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (3, N'902 - Buenos Aires')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (4, N'903 - Catamarca')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (5, N'904 - Cordoba')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (6, N'905 - Corrientes')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (7, N'906 - Chaco')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (8, N'907 - Chubut')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (9, N'908 - Entre Rios')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (10, N'909 - Formosa')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (11, N'910 - Jujuy')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (12, N'911 - La Pampa')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (13, N'912 - La Rioja')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (14, N'913 - Mendoza')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (15, N'914 - Misiones')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (16, N'915 - Neuquen')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (17, N'916 - Rio Negro')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (18, N'917 - Salta')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (19, N'918 - San Juan')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (20, N'919 - San Luis')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (21, N'920 - Santa Cruz')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (22, N'921 - Santa Fe')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (23, N'922 - Santiago del Estero')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (24, N'923 - Tierra del Fuego')
INSERT [dbo].[jurisdiccion] ([idjurisdiccion], [jurisdiccion]) VALUES (25, N'924 - Tucuman')
SET IDENTITY_INSERT [dbo].[jurisdiccion] OFF
GO
SET IDENTITY_INSERT [dbo].[localidad] ON 

INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (1, N'La calera', N'5151', 27)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (2, N'Caballito', N'3031', 23)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (3, N'Ciudad Autónoma de Buenos Aires', N'1228', 22)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (4, N'Mar del plata', N'1406', 23)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (5, N'Ciudad de Mendoza', N'5500', 34)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (6, N'Cafayate', N'3262', 38)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (7, N'Merlo', N'1718', 40)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (8, N'Piedra del Aguila', N'8300', 36)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (11, N'Tanti', N'1234', 27)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (12, N'Carlos Paz', N'5050', 27)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (13, N'Alma Fuerte', N'5412', 27)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (18, N'villa costa', N'8989', 45)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (19, N'la falda', N'5050', 27)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (21, N'Abasto', N'851', 23)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (26, N'Dean funes', N'5200', 27)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (20, N'Villa Leon', N'1714', 35)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (28, N'Villa de soto ', N'8954', 27)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (31, N'colonia caroya', N'5453', 27)
INSERT [dbo].[localidad] ([idLocalidad], [nombreLocalidad], [codigoPostal], [idProvincia]) VALUES (32, N'Capilla del monte', N'5468', 27)
SET IDENTITY_INSERT [dbo].[localidad] OFF
GO
SET IDENTITY_INSERT [dbo].[marcaMP] ON 

INSERT [dbo].[marcaMP] ([id], [descripcion], [idProveedor]) VALUES (5, N'Novo Tex', 8)
INSERT [dbo].[marcaMP] ([id], [descripcion], [idProveedor]) VALUES (6, N'Fenodion', 5)
INSERT [dbo].[marcaMP] ([id], [descripcion], [idProveedor]) VALUES (7, N'Tupiplast', 7)
INSERT [dbo].[marcaMP] ([id], [descripcion], [idProveedor]) VALUES (8, N'Dorsa', 6)
INSERT [dbo].[marcaMP] ([id], [descripcion], [idProveedor]) VALUES (9, N'Crazy Hor', 4)
INSERT [dbo].[marcaMP] ([id], [descripcion], [idProveedor]) VALUES (10, N'Colwave', 13)
INSERT [dbo].[marcaMP] ([id], [descripcion], [idProveedor]) VALUES (11, N'Bibi', 7)
SET IDENTITY_INSERT [dbo].[marcaMP] OFF
GO
SET IDENTITY_INSERT [dbo].[materiaPrima] ON 

INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (1, N'1000', N'Hilo Aparado N40 negro', 3, 14, 4, CAST(6.00 AS Decimal(18, 2)), 7, 7, 4, 6, CAST(1850.00 AS Decimal(18, 2)))
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (2, N'1001', N'Hilo Aparado N40 marron', 3, 15, 4, CAST(6.00 AS Decimal(18, 2)), 7, 7, 4, 5, CAST(1980.10 AS Decimal(18, 2)))
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (3, N'1002', N'Adhesivo de contacto FenoDion', 3, 8, 4, CAST(6.00 AS Decimal(18, 2)), 8, 6, 3, 0, CAST(4600.20 AS Decimal(18, 2)))
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (4, N'1003', N'Cuero negro fulonado', 6, 16, 3, CAST(1.80 AS Decimal(18, 2)), 3, 5, 6, 9, CAST(12300.50 AS Decimal(18, 2)))
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (5, N'1004', N'Forro vacuno negro', 6, 14, 3, CAST(1.80 AS Decimal(18, 2)), 3, 5, 7, 22, CAST(6890.00 AS Decimal(18, 2)))
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (6, N'1005', N'Forro plastico mm1', 3, 4, 3, CAST(1.60 AS Decimal(18, 2)), 4, 5, 8, 6, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (7, N'1006', N'Ojalillo grabado bronce', 8, 8, 4, CAST(1000.00 AS Decimal(18, 2)), 9, 5, 9, 4, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (8, N'1007', N'Ojalillo grabado niquel', 8, 15, 4, CAST(1000.00 AS Decimal(18, 2)), 9, 5, 9, 4, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (9, N'1008', N'Cuero engrasado marron', 8, 9, 3, CAST(1.60 AS Decimal(18, 2)), 3, 5, 6, 4, CAST(11560.00 AS Decimal(18, 2)))
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (10, N'1009', N'Cuero engrasado Negro', 8, 11, 3, CAST(1.60 AS Decimal(18, 2)), 3, 5, 6, 21, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (11, N'1010', N'Caja roja dama GL5', 15, 90, 4, CAST(15.00 AS Decimal(18, 2)), 7, 8, 10, 10, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (12, N'1011', N'Cuero azul crazy Horse', 6, 0, 3, CAST(1.60 AS Decimal(18, 2)), 3, 9, 6, 5, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (13, N'1012', N'Forro vacuno negro liso', 3, 3, 3, CAST(1.60 AS Decimal(18, 2)), 3, 8, 7, 3, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (14, N'1013', N'Plantilla termo sublimada N5', 20, 88, 4, CAST(10.00 AS Decimal(18, 2)), 8, 5, 12, 12, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (15, N'1014', N'Elastico negro mm2', 6, 12, 3, CAST(6.00 AS Decimal(18, 2)), 3, 8, 5, 0, CAST(680.00 AS Decimal(18, 2)))
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (16, N'1015', N'Ojalillo grabado azul ', 10, 3, 4, CAST(900.00 AS Decimal(18, 2)), 9, 7, 9, 0, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (17, N'1016', N'Plantilla sublimada Hombre', 6, 17, 4, CAST(10.00 AS Decimal(18, 2)), 8, 6, 12, 9, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (18, N'1017', N'Elastico gris mm2', 2, 4, 4, CAST(3.00 AS Decimal(18, 2)), 3, 8, 5, 0, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (19, N'1018', N'Hilo Aparado N40 gris', 3, 6, 4, CAST(6.00 AS Decimal(18, 2)), 7, 7, 4, 0, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (20, N'1019', N'Hilo Aparado N40 beige', 3, 2, 4, CAST(6.00 AS Decimal(18, 2)), 7, 7, 4, 0, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (21, N'1020', N'Hilo Aparado N20 Gris', 3, 6, 4, CAST(6.00 AS Decimal(18, 2)), 7, 7, 4, 0, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (22, N'1021', N'Hilo Aparado N20 Beige', 3, 5, 4, CAST(6.00 AS Decimal(18, 2)), 7, 7, 4, 0, CAST(4600.50 AS Decimal(18, 2)))
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (23, N'1022', N'Cordon engrasado negro 1M', 3, 8, 4, CAST(100.00 AS Decimal(18, 2)), 8, 10, 11, 7, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (24, N'1023', N'Cordon engrasado marron 1M', 3, 8, 4, CAST(100.00 AS Decimal(18, 2)), 8, 10, 11, 4, NULL)
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (25, N'1024', N'Caja GL5', 5, 57, 4, CAST(50.00 AS Decimal(18, 2)), 7, 11, 10, 0, CAST(5680.00 AS Decimal(18, 2)))
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (26, N'1025', N'Caja Urban ', 3, 45, 4, CAST(50.00 AS Decimal(18, 2)), 7, 11, 10, 3, CAST(2600.50 AS Decimal(18, 2)))
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (27, N'1026', N'Elastico bicolor beigue 2mm', 3, 2, 4, CAST(3.00 AS Decimal(18, 2)), 3, 8, 5, 0, CAST(680.00 AS Decimal(18, 2)))
INSERT [dbo].[materiaPrima] ([id], [codigo], [descripcion], [stockMinimo], [cantidad], [IdUnidadDeMedida], [cantidadQueContiene], [IdSubUnidadDeMedidda], [idMarcaMP], [idtipoMP], [reservado], [ultimoPrecio]) VALUES (28, N'1027', N'Elastico bicolor azul 2mm', 3, 4, 4, CAST(3.00 AS Decimal(18, 2)), 3, 8, 5, 0, CAST(680.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[materiaPrima] OFF
GO
SET IDENTITY_INSERT [dbo].[materiaPrimaPorArticulo] ON 

INSERT [dbo].[materiaPrimaPorArticulo] ([id], [codigoArt], [cuero1], [cuero2], [forro1], [forro2], [refuerzo1], [refuerzo2], [refuerzo3], [hilo1], [hilo2], [costuron], [apliqueI], [apliqueII], [apliqueIII], [contrafuerte], [elastico], [ojalillos], [plantillaArmado], [caja], [plantilla], [cordon], [horma], [fondo], [comentario1], [comentario2], [comentario3]) VALUES (1, N'siena acordonado negro', N'Cuero engrasado Negro', N'', N'Azul marino', N'', N'', N'', N'', N'Hilo Aparado N40 gris', N'', N'Diente de perro', N'N20, 40 gris', N'Aplique- costuron gris', N'Forro azul copete', N'', N'', N'Ojalillo grabado niquel', N'', N'Caja Urban ', N'Plantilla termo sublimada N5', N'Cordon engrasado negro 1M', N'4560', N'', N'', N'', N'')
INSERT [dbo].[materiaPrimaPorArticulo] ([id], [codigoArt], [cuero1], [cuero2], [forro1], [forro2], [refuerzo1], [refuerzo2], [refuerzo3], [hilo1], [hilo2], [costuron], [apliqueI], [apliqueII], [apliqueIII], [contrafuerte], [elastico], [ojalillos], [plantillaArmado], [caja], [plantilla], [cordon], [horma], [fondo], [comentario1], [comentario2], [comentario3]) VALUES (2, N'confort office slack negro', N'Cuero negro fulonado', N'', N'Forro vacuno negro liso', N'', N'Puntera plastica', N'Plancha refuerzo copete - puntera', N'', N'Hilo Aparado N40 marron', N'', N'', N'N40 -20 negro', N'Sello copete scarpino', N'', N'Forro plastico mm1', N'', N'Ojalillo grabado bronce', N'', N'Caja Urban ', N'Plantilla sublimada Hombre', N'Cordon engrasado negro 1M', N'3260', N'', N'', N'', N'')
INSERT [dbo].[materiaPrimaPorArticulo] ([id], [codigoArt], [cuero1], [cuero2], [forro1], [forro2], [refuerzo1], [refuerzo2], [refuerzo3], [hilo1], [hilo2], [costuron], [apliqueI], [apliqueII], [apliqueIII], [contrafuerte], [elastico], [ojalillos], [plantillaArmado], [caja], [plantilla], [cordon], [horma], [fondo], [comentario1], [comentario2], [comentario3]) VALUES (3, N'verona slack negro', N'Cuero engrasado Negro', N'', N'', N'', N'', N'', N'', N'Hilo Aparado N40 negro', N'', N'Diente de perro', N'N20 - 40 Gris PAFF azul', N'Aplique gris - forro azul', N'', N'', N'Elastico gris mm2', N'', N'', N'Caja Urban ', N'Plantilla termo sublimada N5', N'', N'2256', N'', N'', N'', N'')
INSERT [dbo].[materiaPrimaPorArticulo] ([id], [codigoArt], [cuero1], [cuero2], [forro1], [forro2], [refuerzo1], [refuerzo2], [refuerzo3], [hilo1], [hilo2], [costuron], [apliqueI], [apliqueII], [apliqueIII], [contrafuerte], [elastico], [ojalillos], [plantillaArmado], [caja], [plantilla], [cordon], [horma], [fondo], [comentario1], [comentario2], [comentario3]) VALUES (4, N'verona acordonado marron', N'Cuero engrasado marron', N'', N'', N'', N'', N'', N'', N'Hilo Aparado N40 beige', N'', N'Diente de perro', N'N20 - 40 Beige', N'Elastico beigue bicol - ', N'Aplique beige', N'', N'', N'Ojalillo grabado bronce', N'', N'Caja Urban ', N'Plantilla termo sublimada N5', N'Cordon engrasado marron 1M', N'2216', N'', N'', N'', N'')
INSERT [dbo].[materiaPrimaPorArticulo] ([id], [codigoArt], [cuero1], [cuero2], [forro1], [forro2], [refuerzo1], [refuerzo2], [refuerzo3], [hilo1], [hilo2], [costuron], [apliqueI], [apliqueII], [apliqueIII], [contrafuerte], [elastico], [ojalillos], [plantillaArmado], [caja], [plantilla], [cordon], [horma], [fondo], [comentario1], [comentario2], [comentario3]) VALUES (5, N'siena slack azul', N'Cuero azul crazy Horse', N'', N'', N'', N'', N'', N'', N'Hilo Aparado N40 gris', N'', N'Diente de perro', N'N20 - 40 Gris', N'Elast - forro azul - costuron gris', N'', N'', N'Elastico bicolor azul 2mm', N'', N'', N'Caja Urban ', N'Plantilla sublimada Hombre', N'', N'2216', N'', N'', N'', N'')
SET IDENTITY_INSERT [dbo].[materiaPrimaPorArticulo] OFF
GO
SET IDENTITY_INSERT [dbo].[ordenDeCompra] ON 

INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (41, 0, CAST(N'2023-05-25' AS Date), CAST(13800.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(2898.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(16698.00 AS Decimal(18, 2)), CAST(N'2023-05-25' AS Date), N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 3, NULL, 4)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (42, 1, CAST(N'2023-05-24' AS Date), CAST(9201.60 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(1932.34 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(11133.94 AS Decimal(18, 2)), CAST(N'2023-05-25' AS Date), N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 4, NULL, 4)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (43, 2, CAST(N'2023-06-15' AS Date), CAST(196807.47 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(41329.57 AS Decimal(18, 2)), CAST(6200.00 AS Decimal(18, 2)), CAST(244337.04 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 4, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (44, 3, CAST(N'2023-07-21' AS Date), CAST(9003.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(1890.63 AS Decimal(18, 2)), CAST(3680.60 AS Decimal(18, 2)), CAST(14574.23 AS Decimal(18, 2)), CAST(N'2023-07-21' AS Date), N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 3, NULL, 4)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (45, 4, CAST(N'2023-07-21' AS Date), CAST(24120.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(5065.20 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(29185.20 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 6, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (46, 5, CAST(N'2023-07-22' AS Date), CAST(10900.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(2289.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(13189.00 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 5, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (47, 6, CAST(N'2023-07-22' AS Date), CAST(22374.40 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(4698.62 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(27073.02 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 6, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (48, 7, CAST(N'2023-07-22' AS Date), CAST(7503.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'0', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(7503.00 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 7, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (49, 8, CAST(N'2023-07-26' AS Date), CAST(130025.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(27305.25 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(157330.25 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 6, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (50, 9, CAST(N'2023-07-26' AS Date), CAST(85361.50 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(17925.92 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(103287.42 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 6, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (51, 10, CAST(N'2023-07-26' AS Date), CAST(30604.20 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(6426.88 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(37031.08 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 13, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (52, 11, CAST(N'2023-07-28' AS Date), CAST(8201.80 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(1722.38 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9924.18 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 5, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (53, 12, CAST(N'2023-07-28' AS Date), CAST(9200.40 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'0', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(9200.40 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 6, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (54, 13, CAST(N'2023-07-29' AS Date), CAST(22241.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(4670.61 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(26911.61 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 5, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (55, 14, CAST(N'2023-07-29' AS Date), CAST(5440.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(1142.40 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(6582.40 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 3, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (56, 15, CAST(N'2023-07-30' AS Date), CAST(8501.80 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(1785.38 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(10287.18 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'3543466606', 3, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (57, 16, CAST(N'2023-07-30' AS Date), CAST(4801.80 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(1008.38 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(5810.18 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'03543466606', 6, NULL, 1)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (58, 17, CAST(N'2023-07-30' AS Date), CAST(9101.80 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(1911.38 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(11013.18 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'03543466606', 13, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (59, 18, CAST(N'2023-07-30' AS Date), CAST(9551.50 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(2005.82 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(11557.32 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'03543466606', 13, NULL, 2)
INSERT [dbo].[ordenDeCompra] ([id], [numero], [fecha], [subtotal], [importeDescuento], [ivaPorcentaje], [importeIva], [importeEnvio], [importeTotal], [fechaDeIngreso], [enviarEmpresa], [enviarDomicilio], [enviarTelefono], [idProveedor], [idDetalleOrdenDeCompra], [idEstadoOrdenC]) VALUES (60, 19, CAST(N'2023-07-31' AS Date), CAST(11490.30 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), N'21', CAST(2412.96 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), CAST(13903.26 AS Decimal(18, 2)), NULL, N'Fabincal Cordoba SA', N'Juan Bautista Alberdi 450 La Calera Cordoba', N'03543466606', 13, NULL, 2)
SET IDENTITY_INSERT [dbo].[ordenDeCompra] OFF
GO
SET IDENTITY_INSERT [dbo].[ordenDeFabricacion] ON 

INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (4, 0, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-09' AS Date), 30, 4, 7, NULL, N'Amanda SA', 16, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-05' AS Date), CAST(N'2023-06-06' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (5, 1, CAST(N'2023-06-06' AS Date), CAST(N'2023-06-09' AS Date), 11, 5, 7, NULL, N'Amanda SA', 13, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-05' AS Date), CAST(N'2023-06-07' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (6, 2, CAST(N'2023-06-01' AS Date), CAST(N'2023-06-09' AS Date), 17, 5, 7, NULL, N'Amanda SA', 14, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-04' AS Date), CAST(N'2023-06-10' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (7, 3, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-04' AS Date), 17, 5, 7, NULL, N'Mary shoes', 15, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-05' AS Date), CAST(N'2023-06-07' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (8, 4, CAST(N'2023-06-07' AS Date), CAST(N'2023-06-13' AS Date), 19, 5, 7, NULL, N'Mary shoes', 17, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-05' AS Date), CAST(N'2023-06-07' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (9, 5, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-04' AS Date), 6, 4, 7, NULL, N'Mary shoes', 18, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-05' AS Date), CAST(N'2023-06-06' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (11, 7, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-04' AS Date), 18, 5, 7, NULL, N'Mary shoes', 10, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-04' AS Date), CAST(N'2023-06-11' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (12, 8, CAST(N'2023-06-05' AS Date), CAST(N'2023-06-09' AS Date), 30, 5, 7, NULL, N'Amanda SA', 16, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-10' AS Date), CAST(N'2023-06-19' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (13, 9, CAST(N'2023-06-09' AS Date), CAST(N'2023-06-14' AS Date), 12, 4, 7, NULL, N'Mary shoes', 18, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-06' AS Date), CAST(N'2023-06-10' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (14, 10, CAST(N'2023-06-12' AS Date), CAST(N'2023-06-16' AS Date), 32, 5, 7, NULL, N'Mary shoes', 23, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-10' AS Date), CAST(N'2023-06-10' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (15, 11, CAST(N'2023-06-13' AS Date), CAST(N'2023-06-19' AS Date), 30, 4, 7, NULL, N'Mary shoes', 24, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-10' AS Date), CAST(N'2023-06-10' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (16, 12, CAST(N'2023-06-13' AS Date), CAST(N'2023-06-16' AS Date), 10, 5, 7, NULL, N'Amanda SA', 25, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-15' AS Date), CAST(N'2023-06-19' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (17, 13, CAST(N'2023-06-13' AS Date), CAST(N'2023-06-16' AS Date), 4, 5, 7, NULL, N'Amanda SA', 26, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-10' AS Date), CAST(N'2023-06-11' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (18, 14, CAST(N'2023-06-06' AS Date), CAST(N'2023-06-09' AS Date), 10, 5, 7, NULL, N'Mary shoes', 27, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-04' AS Date), CAST(N'2023-06-10' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (19, 15, CAST(N'2023-06-07' AS Date), CAST(N'2023-06-13' AS Date), 15, 4, 7, NULL, N'Mary shoes', 28, CAST(N'2023-06-04' AS Date), CAST(N'2023-06-04' AS Date), CAST(N'2023-06-10' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (20, 16, CAST(N'2023-06-12' AS Date), CAST(N'2023-06-16' AS Date), 7, 5, 7, NULL, N'Amanda SA', 21, CAST(N'2023-06-11' AS Date), CAST(N'2023-06-15' AS Date), CAST(N'2023-06-19' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (21, 17, CAST(N'2023-06-11' AS Date), CAST(N'2023-06-16' AS Date), 24, 5, 7, NULL, N'Mary shoes', 31, CAST(N'2023-06-11' AS Date), CAST(N'2023-06-11' AS Date), CAST(N'2023-06-19' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (22, 18, CAST(N'2023-06-12' AS Date), CAST(N'2023-06-16' AS Date), 30, 5, 7, NULL, N'Mary shoes', 24, CAST(N'2023-06-12' AS Date), CAST(N'2023-06-15' AS Date), CAST(N'2023-07-14' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (24, 19, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 12, 4, 7, NULL, N'Mary shoes', 47, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (25, 20, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 28, 4, 7, NULL, N'Mary shoes', 46, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (26, 21, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 27, 5, 7, NULL, N'Mary shoes', 44, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (27, 22, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 31, 5, 7, NULL, N'Mary shoes', 43, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date), CAST(N'2023-07-29' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (28, 23, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 25, 4, 7, NULL, N'Mary shoes', 45, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (29, 24, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 22, 5, 7, NULL, N'Mary shoes', 41, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (30, 25, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 12, 5, 7, NULL, N'Mary shoes', 47, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (31, 26, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 17, 5, 7, NULL, N'Mary shoes', 45, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date), CAST(N'2023-07-29' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (32, 27, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 15, 5, 7, NULL, N'Mary shoes', 40, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (33, 28, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 28, 5, 7, NULL, N'Mary shoes', 46, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date), CAST(N'2023-07-29' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (34, 29, CAST(N'2023-07-23' AS Date), CAST(N'2023-07-27' AS Date), 13, 5, 7, NULL, N'Mary shoes', 38, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-24' AS Date), CAST(N'2023-07-29' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (35, 30, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 19, 5, 7, NULL, N'Mary shoes', 37, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date), CAST(N'2023-07-26' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (36, 31, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 40, 5, 7, NULL, N'Briganti calzados', 48, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (37, 32, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 24, 5, 7, NULL, N'Briganti calzados', 51, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date), CAST(N'2023-07-22' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (38, 33, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-27' AS Date), 17, 5, 7, NULL, N'Mary shoes', 50, CAST(N'2023-07-22' AS Date), CAST(N'2023-07-23' AS Date), CAST(N'2023-07-26' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (39, 34, CAST(N'2023-07-24' AS Date), CAST(N'2023-07-29' AS Date), 2, 5, 7, NULL, N'Amanda SA', 34, CAST(N'2023-07-24' AS Date), CAST(N'2023-07-26' AS Date), CAST(N'2023-07-30' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (40, 35, CAST(N'2023-07-28' AS Date), CAST(N'2023-08-02' AS Date), 50, 2, 2, NULL, N'Mary shoes', 54, CAST(N'2023-07-26' AS Date), CAST(N'2023-07-29' AS Date), NULL)
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (41, 36, CAST(N'2023-07-27' AS Date), CAST(N'2023-08-01' AS Date), 26, 2, 4, NULL, N'Briganti calzados', 53, CAST(N'2023-07-26' AS Date), CAST(N'2023-07-29' AS Date), NULL)
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (42, 37, CAST(N'2023-07-27' AS Date), CAST(N'2023-08-01' AS Date), 9, 5, 7, NULL, N'Mary shoes', 59, CAST(N'2023-07-27' AS Date), CAST(N'2023-07-27' AS Date), CAST(N'2023-07-27' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (43, 38, CAST(N'2023-07-27' AS Date), CAST(N'2023-08-01' AS Date), 16, 5, 7, NULL, N'Mary shoes', 60, CAST(N'2023-07-27' AS Date), CAST(N'2023-07-27' AS Date), CAST(N'2023-07-27' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (44, 39, CAST(N'2023-07-28' AS Date), CAST(N'2023-08-02' AS Date), 14, 5, 7, NULL, N'Mary shoes', 58, CAST(N'2023-07-28' AS Date), CAST(N'2023-07-29' AS Date), CAST(N'2023-07-30' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (45, 40, CAST(N'2023-07-31' AS Date), CAST(N'2023-09-05' AS Date), 24, 5, 7, NULL, N'Briganti calzados', 57, CAST(N'2023-07-29' AS Date), CAST(N'2023-07-29' AS Date), CAST(N'2023-07-29' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (46, 41, CAST(N'2023-07-31' AS Date), CAST(N'2023-09-06' AS Date), 23, 5, 7, NULL, N'Mary shoes', 61, CAST(N'2023-07-29' AS Date), CAST(N'2023-07-29' AS Date), CAST(N'2023-07-29' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (47, 42, CAST(N'2023-07-29' AS Date), CAST(N'2023-08-03' AS Date), 21, 2, 2, NULL, N'Mary shoes', 62, CAST(N'2023-07-29' AS Date), CAST(N'2023-07-29' AS Date), NULL)
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (48, 43, CAST(N'2023-07-30' AS Date), CAST(N'2023-08-04' AS Date), 24, 2, 2, NULL, N'Briganti calzados', 56, CAST(N'2023-07-30' AS Date), CAST(N'2023-07-31' AS Date), NULL)
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (49, 44, CAST(N'2023-07-30' AS Date), CAST(N'2023-08-04' AS Date), 6, 5, 7, NULL, N'Briganti calzados', 52, CAST(N'2023-07-30' AS Date), CAST(N'2023-07-30' AS Date), CAST(N'2023-07-30' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (50, 45, CAST(N'2023-07-30' AS Date), CAST(N'2023-08-04' AS Date), 15, 5, 7, NULL, N'Briganti calzados', 64, CAST(N'2023-07-30' AS Date), CAST(N'2023-07-30' AS Date), CAST(N'2023-07-30' AS Date))
INSERT [dbo].[ordenDeFabricacion] ([id], [numero], [fechaCreacion], [fechaPrevistaFin], [totalPares], [idEstadoOrdenF], [idPuntoDeControl], [idDetalleOrdenF], [cliente], [idPedido], [fechaEmision], [fechaRealInicio], [fechaRealFin]) VALUES (51, 46, CAST(N'2023-07-31' AS Date), CAST(N'2023-08-05' AS Date), 3, 5, 7, NULL, N'Briganti calzados', 70, CAST(N'2023-07-31' AS Date), CAST(N'2023-07-31' AS Date), CAST(N'2023-07-31' AS Date))
SET IDENTITY_INSERT [dbo].[ordenDeFabricacion] OFF
GO
SET IDENTITY_INSERT [dbo].[pedido] ON 

INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (10, CAST(N'2023-05-25' AS Date), CAST(92010.00 AS Decimal(18, 2)), CAST(N'2023-06-19' AS Date), 20, NULL, 6, 7, 1, 6, 81, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (11, CAST(N'2023-05-25' AS Date), CAST(36804.00 AS Decimal(18, 2)), CAST(N'2023-07-11' AS Date), 8, NULL, 5, NULL, 1, 6, 6, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (12, CAST(N'2023-05-25' AS Date), CAST(4950.80 AS Decimal(18, 2)), CAST(N'2023-07-26' AS Date), 1, NULL, 5, NULL, 1, 6, 6, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (13, CAST(N'2023-05-25' AS Date), CAST(50605.50 AS Decimal(18, 2)), NULL, 11, NULL, 5, 1, 1, 2, 6, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (14, CAST(N'2023-05-25' AS Date), CAST(78208.50 AS Decimal(18, 2)), CAST(N'2023-07-24' AS Date), 17, NULL, 5, 2, 1, 6, 81, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (15, CAST(N'2023-06-01' AS Date), CAST(78208.50 AS Decimal(18, 2)), CAST(N'2023-07-22' AS Date), 17, NULL, 6, 3, 1, 6, 56, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (16, CAST(N'2023-06-01' AS Date), CAST(138015.00 AS Decimal(18, 2)), CAST(N'2023-07-22' AS Date), 30, NULL, 5, 8, 1, 6, 81, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (17, CAST(N'2023-06-02' AS Date), CAST(87409.50 AS Decimal(18, 2)), NULL, 19, NULL, 6, 4, 1, 2, 48, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (18, CAST(N'2023-06-04' AS Date), CAST(55206.00 AS Decimal(18, 2)), NULL, 12, NULL, 6, 9, 1, 5, 6, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (19, CAST(N'2023-06-04' AS Date), CAST(64407.00 AS Decimal(18, 2)), NULL, 14, NULL, 6, 6, 1, 5, 86, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (20, CAST(N'2023-06-08' AS Date), CAST(73608.00 AS Decimal(18, 2)), CAST(N'2023-07-31' AS Date), 16, NULL, 6, NULL, 1, 6, 48, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (21, CAST(N'2023-06-08' AS Date), CAST(55206.00 AS Decimal(18, 2)), CAST(N'2023-07-29' AS Date), 12, NULL, 5, 16, 1, 6, 48, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (22, CAST(N'2023-06-08' AS Date), CAST(73608.00 AS Decimal(18, 2)), NULL, 16, NULL, 5, NULL, 1, 5, 81, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (23, CAST(N'2023-06-09' AS Date), CAST(147216.00 AS Decimal(18, 2)), CAST(N'2023-07-30' AS Date), 32, NULL, 6, 10, 1, 6, 77, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (24, CAST(N'2023-06-09' AS Date), CAST(138015.00 AS Decimal(18, 2)), CAST(N'2023-07-30' AS Date), 30, NULL, 6, 18, 1, 6, 69, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (25, CAST(N'2023-06-09' AS Date), CAST(55206.00 AS Decimal(18, 2)), NULL, 12, NULL, 5, 12, 1, 5, 56, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (26, CAST(N'2023-06-09' AS Date), CAST(27603.00 AS Decimal(18, 2)), CAST(N'2023-07-29' AS Date), 6, NULL, 5, 13, 1, 6, 56, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (27, CAST(N'2023-06-09' AS Date), CAST(64407.00 AS Decimal(18, 2)), NULL, 14, NULL, 6, 14, 1, 4, 71, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (28, CAST(N'2023-06-10' AS Date), CAST(82813.50 AS Decimal(18, 2)), NULL, 18, NULL, 6, 15, 1, 7, 70, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (29, CAST(N'2023-06-11' AS Date), CAST(92015.00 AS Decimal(18, 2)), NULL, 20, NULL, 6, NULL, 1, 4, 27, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (30, CAST(N'2023-06-11' AS Date), CAST(46007.50 AS Decimal(18, 2)), NULL, 10, NULL, 6, 19, 1, 4, 31, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (31, CAST(N'2023-06-11' AS Date), CAST(184020.00 AS Decimal(18, 2)), NULL, 40, NULL, 6, 17, 1, 3, 81, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (32, CAST(N'2023-06-12' AS Date), CAST(41404.50 AS Decimal(18, 2)), NULL, 9, NULL, 6, NULL, 1, 3, 6, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (33, CAST(N'2023-06-15' AS Date), CAST(9201.00 AS Decimal(18, 2)), CAST(N'2023-07-24' AS Date), 2, NULL, 5, NULL, 1, 6, 6, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (34, CAST(N'2023-06-15' AS Date), CAST(9201.00 AS Decimal(18, 2)), NULL, 2, NULL, 5, 34, 1, 3, 6, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (35, CAST(N'2023-06-15' AS Date), CAST(9201.00 AS Decimal(18, 2)), NULL, 2, NULL, 5, NULL, 1, 3, 87, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (36, CAST(N'2023-06-15' AS Date), CAST(55206.00 AS Decimal(18, 2)), NULL, 12, NULL, 6, NULL, 1, 7, 74, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (37, CAST(N'2023-06-19' AS Date), CAST(108917.60 AS Decimal(18, 2)), NULL, 22, NULL, 6, 30, 1, 3, 6, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (38, CAST(N'2023-06-19' AS Date), CAST(59800.00 AS Decimal(18, 2)), NULL, 13, NULL, 6, 29, 1, 3, 83, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (40, CAST(N'2023-07-18' AS Date), CAST(52501.50 AS Decimal(18, 2)), NULL, 15, NULL, 6, 27, 1, 3, 72, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (41, CAST(N'2023-07-18' AS Date), CAST(77002.20 AS Decimal(18, 2)), NULL, 22, NULL, 6, 24, 1, 3, 69, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (43, CAST(N'2023-07-18' AS Date), CAST(142600.00 AS Decimal(18, 2)), NULL, 31, NULL, 6, 22, 1, 3, 81, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (44, CAST(N'2023-07-18' AS Date), CAST(124200.00 AS Decimal(18, 2)), NULL, 27, NULL, 6, 21, 1, 3, 88, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (45, CAST(N'2023-07-18' AS Date), CAST(115018.75 AS Decimal(18, 2)), NULL, 25, NULL, 6, 26, 1, 3, 30, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (46, CAST(N'2023-07-18' AS Date), CAST(100800.00 AS Decimal(18, 2)), NULL, 28, NULL, 6, 28, 1, 4, 91, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (47, CAST(N'2023-07-19' AS Date), CAST(55209.00 AS Decimal(18, 2)), NULL, 12, NULL, 6, 25, 1, 3, 33, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (48, CAST(N'2023-07-22' AS Date), CAST(198032.00 AS Decimal(18, 2)), NULL, 40, NULL, 7, 31, 1, 3, 9, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (49, CAST(N'2023-07-22' AS Date), CAST(82809.00 AS Decimal(18, 2)), NULL, 18, NULL, 7, NULL, 1, 3, 51, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (50, CAST(N'2023-07-22' AS Date), CAST(84163.60 AS Decimal(18, 2)), NULL, 17, NULL, 6, 33, 1, 3, 9, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (51, CAST(N'2023-07-22' AS Date), CAST(110418.00 AS Decimal(18, 2)), CAST(N'2023-07-22' AS Date), 24, NULL, 7, 32, 1, 6, 31, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (52, CAST(N'2023-07-24' AS Date), CAST(59409.60 AS Decimal(18, 2)), NULL, 12, NULL, 7, 44, 3, 3, 8, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (53, CAST(N'2023-07-24' AS Date), CAST(119619.50 AS Decimal(18, 2)), NULL, 26, NULL, 7, 36, 1, 2, 30, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (54, CAST(N'2023-07-26' AS Date), CAST(230037.50 AS Decimal(18, 2)), NULL, 50, NULL, 6, 35, 1, 2, 60, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (55, CAST(N'2023-07-26' AS Date), CAST(144000.00 AS Decimal(18, 2)), NULL, 40, NULL, 7, NULL, 1, 3, 93, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (56, CAST(N'2023-07-26' AS Date), CAST(110400.00 AS Decimal(18, 2)), NULL, 24, NULL, 7, 43, 3, 2, 84, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (57, CAST(N'2023-07-26' AS Date), CAST(108018.00 AS Decimal(18, 2)), NULL, 24, NULL, 7, 40, 3, 3, 98, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (58, CAST(N'2023-07-27' AS Date), CAST(58800.00 AS Decimal(18, 2)), NULL, 14, NULL, 6, 39, 1, 3, 72, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (59, CAST(N'2023-07-27' AS Date), CAST(54009.00 AS Decimal(18, 2)), CAST(N'2023-07-27' AS Date), 12, NULL, 6, 37, 1, 6, 97, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (60, CAST(N'2023-07-27' AS Date), CAST(110418.00 AS Decimal(18, 2)), NULL, 24, NULL, 6, 38, 1, 3, 32, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (61, CAST(N'2023-07-29' AS Date), CAST(113868.40 AS Decimal(18, 2)), CAST(N'2023-07-29' AS Date), 23, NULL, 6, 41, 10, 6, 8, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (62, CAST(N'2023-07-29' AS Date), CAST(135022.50 AS Decimal(18, 2)), NULL, 30, NULL, 6, 42, 10, 2, 94, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (63, CAST(N'2023-07-29' AS Date), CAST(29704.80 AS Decimal(18, 2)), NULL, 6, NULL, 6, NULL, 10, 3, 6, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (64, CAST(N'2023-07-30' AS Date), CAST(110412.00 AS Decimal(18, 2)), NULL, 24, NULL, 7, 45, 3, 3, 50, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (65, CAST(N'2023-07-31' AS Date), CAST(99016.00 AS Decimal(18, 2)), NULL, 20, NULL, 7, NULL, 1, 1, 9, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (66, CAST(N'2023-07-31' AS Date), CAST(121000.00 AS Decimal(18, 2)), NULL, 22, NULL, 7, NULL, 1, 1, 30, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (67, CAST(N'2023-07-31' AS Date), CAST(78208.50 AS Decimal(18, 2)), NULL, 17, NULL, 6, NULL, 1, 1, 51, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (68, CAST(N'2023-07-31' AS Date), CAST(87414.25 AS Decimal(18, 2)), NULL, 19, NULL, 7, NULL, 1, 3, 59, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (69, CAST(N'2023-07-31' AS Date), CAST(78461.25 AS Decimal(18, 2)), NULL, 15, NULL, 6, NULL, 1, 1, 83, 1)
INSERT [dbo].[pedido] ([numero], [fechaDePedido], [importeTotal], [fechaSalidaDePedido], [totalPares], [idDetallePedido], [idCliente], [idOrdenDeFabricacion], [idUsuario], [idEstadoPedido], [idArt], [idFormaDePago]) VALUES (70, CAST(N'2023-07-31' AS Date), CAST(78461.25 AS Decimal(18, 2)), NULL, 15, NULL, 7, 46, 3, 3, 83, 1)
SET IDENTITY_INSERT [dbo].[pedido] OFF
GO
SET IDENTITY_INSERT [dbo].[proveedor] ON 

INSERT [dbo].[proveedor] ([id], [nombre], [cuit], [mail], [telefono], [condicionIva], [idDomicilio]) VALUES (3, N'Yaira', N'30-12345675-0', N'yairaComplementos@gmail.com', N'56412312', N'IVA Responsable Inscripto', 123)
INSERT [dbo].[proveedor] ([id], [nombre], [cuit], [mail], [telefono], [condicionIva], [idDomicilio]) VALUES (4, N'Grupo Santino', N'30234567890', N'SantinoGrupo@gmail.com', N'2234445678', N'IVA Responsable Inscripto', 456)
INSERT [dbo].[proveedor] ([id], [nombre], [cuit], [mail], [telefono], [condicionIva], [idDomicilio]) VALUES (5, N'Celiberti', N'30345678901', N'CelibertiProductos@outlook.com', N'2613337890', N'IVA Responsable Inscripto', 690)
INSERT [dbo].[proveedor] ([id], [nombre], [cuit], [mail], [telefono], [condicionIva], [idDomicilio]) VALUES (6, N'Taical', N'30456789012', N'taicalSRL@gmail.com', N'3872223456', N'IVA Responsable Inscripto', 206)
INSERT [dbo].[proveedor] ([id], [nombre], [cuit], [mail], [telefono], [condicionIva], [idDomicilio]) VALUES (7, N'Grupo zahonero', N'30789012345', N'ZahoneroCompania@gmail.com', N'114447890', N'IVA Responsable Inscripto', 4651)
INSERT [dbo].[proveedor] ([id], [nombre], [cuit], [mail], [telefono], [condicionIva], [idDomicilio]) VALUES (8, N'CintaTex SA', N'30711303401', N'CintaTexVentas@gmail.com', N'2993334567', N'IVA Responsable Inscripto', 196)
INSERT [dbo].[proveedor] ([id], [nombre], [cuit], [mail], [telefono], [condicionIva], [idDomicilio]) VALUES (13, N'Dorsa SL', N'30-70977356-8', N'Dorsa@gmail.com', N'1146214789', N'IVA Responsable Inscripto', 220)
SET IDENTITY_INSERT [dbo].[proveedor] OFF
GO
SET IDENTITY_INSERT [dbo].[provincia] ON 

INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (22, N'Ciudad autonoma de Buenos Aires')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (23, N'Buenos Aires')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (24, N'Catamarca')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (25, N'Chaco')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (26, N'Chubut')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (27, N'Cordoba')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (28, N'Corrientes')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (29, N'Entre Rios')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (30, N'Formosa')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (31, N'Jujuy')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (32, N'La Pampa')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (33, N'La Rioja')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (34, N'Mendoza')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (35, N'Misiones')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (36, N'Neuquen')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (37, N'Rio Negro')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (38, N'Salta')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (39, N'San Juan')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (40, N'San Luis')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (41, N'Santa Cruz')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (42, N'Santa Fe')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (43, N'Santiago del Estero')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (44, N'Tierra del Fuego')
INSERT [dbo].[provincia] ([idprovincia], [nombreProvincia]) VALUES (45, N'Tucuman')
SET IDENTITY_INSERT [dbo].[provincia] OFF
GO
SET IDENTITY_INSERT [dbo].[puntoDeControl] ON 

INSERT [dbo].[puntoDeControl] ([id], [descripcion]) VALUES (1, N'--')
INSERT [dbo].[puntoDeControl] ([id], [descripcion]) VALUES (2, N'Cortado')
INSERT [dbo].[puntoDeControl] ([id], [descripcion]) VALUES (3, N'Aparado')
INSERT [dbo].[puntoDeControl] ([id], [descripcion]) VALUES (4, N'Ingreso linea')
INSERT [dbo].[puntoDeControl] ([id], [descripcion]) VALUES (5, N'Linea')
INSERT [dbo].[puntoDeControl] ([id], [descripcion]) VALUES (6, N'Empaque')
INSERT [dbo].[puntoDeControl] ([id], [descripcion]) VALUES (7, N'Finalizado')
SET IDENTITY_INSERT [dbo].[puntoDeControl] OFF
GO
SET IDENTITY_INSERT [dbo].[remito] ON 

INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (18, 26589456, CAST(N'2023-06-15' AS Date), 1, 10)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (19, 26589457, CAST(N'2023-06-19' AS Date), 1, 11)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (20, 26589458, CAST(N'2023-07-19' AS Date), 1, 14)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (21, 26589459, CAST(N'2023-07-19' AS Date), 1, 15)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (22, 26589460, CAST(N'2023-07-19' AS Date), 1, 16)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (23, 26589461, CAST(N'2023-07-22' AS Date), 1, 12)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (24, 26589462, CAST(N'2023-07-22' AS Date), 1, 51)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (25, 26589463, CAST(N'2023-07-24' AS Date), 1, 18)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (26, 26589464, CAST(N'2023-07-24' AS Date), 1, 33)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (27, 26589465, CAST(N'2023-07-26' AS Date), 1, 19)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (28, 26589466, CAST(N'2023-07-26' AS Date), 1, 20)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (29, 26589467, CAST(N'2023-07-26' AS Date), 1, 21)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (30, 26589468, CAST(N'2023-07-26' AS Date), 1, 22)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (31, 26589469, CAST(N'2023-07-26' AS Date), 1, 23)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (32, 26589470, CAST(N'2023-07-27' AS Date), 1, 59)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (33, 26589471, CAST(N'2023-07-28' AS Date), 1, 26)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (34, 26589472, CAST(N'2023-07-29' AS Date), 1, 61)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (35, 26589473, CAST(N'2023-07-29' AS Date), 1, 24)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (36, 26589474, CAST(N'2023-07-29' AS Date), 1, 46)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (37, 26589475, CAST(N'2023-07-30' AS Date), 1, 25)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (38, 26589476, CAST(N'2023-07-30' AS Date), 1, 27)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (39, 26589477, CAST(N'2023-07-30' AS Date), 1, 29)
INSERT [dbo].[remito] ([id], [numero], [fecha], [bultos], [idPedido]) VALUES (40, 26589478, CAST(N'2023-07-31' AS Date), 1, 30)
SET IDENTITY_INSERT [dbo].[remito] OFF
GO
SET IDENTITY_INSERT [dbo].[SubUnidadDeMedida] ON 

INSERT [dbo].[SubUnidadDeMedida] ([id], [descripcion]) VALUES (1, N'Plancha')
INSERT [dbo].[SubUnidadDeMedida] ([id], [descripcion]) VALUES (2, N'Paquete')
INSERT [dbo].[SubUnidadDeMedida] ([id], [descripcion]) VALUES (3, N'Mts')
INSERT [dbo].[SubUnidadDeMedida] ([id], [descripcion]) VALUES (4, N'Mt2')
INSERT [dbo].[SubUnidadDeMedida] ([id], [descripcion]) VALUES (5, N'Chapa')
INSERT [dbo].[SubUnidadDeMedida] ([id], [descripcion]) VALUES (6, N'Unidad')
INSERT [dbo].[SubUnidadDeMedida] ([id], [descripcion]) VALUES (7, N'Unidades')
INSERT [dbo].[SubUnidadDeMedida] ([id], [descripcion]) VALUES (8, N'Ps')
INSERT [dbo].[SubUnidadDeMedida] ([id], [descripcion]) VALUES (9, N'Gramos')
INSERT [dbo].[SubUnidadDeMedida] ([id], [descripcion]) VALUES (10, N'Bolsa')
SET IDENTITY_INSERT [dbo].[SubUnidadDeMedida] OFF
GO
SET IDENTITY_INSERT [dbo].[tipoMP] ON 

INSERT [dbo].[tipoMP] ([id], [descripcion], [idProveedor]) VALUES (3, N'Adhesivos', 5)
INSERT [dbo].[tipoMP] ([id], [descripcion], [idProveedor]) VALUES (4, N'Hilo', 8)
INSERT [dbo].[tipoMP] ([id], [descripcion], [idProveedor]) VALUES (5, N'Elastico', 6)
INSERT [dbo].[tipoMP] ([id], [descripcion], [idProveedor]) VALUES (6, N'Cuero', 4)
INSERT [dbo].[tipoMP] ([id], [descripcion], [idProveedor]) VALUES (7, N'Forro', 3)
INSERT [dbo].[tipoMP] ([id], [descripcion], [idProveedor]) VALUES (8, N'Forro plastificado', 4)
INSERT [dbo].[tipoMP] ([id], [descripcion], [idProveedor]) VALUES (9, N'Ojalillos', 4)
INSERT [dbo].[tipoMP] ([id], [descripcion], [idProveedor]) VALUES (10, N'Caja', 6)
INSERT [dbo].[tipoMP] ([id], [descripcion], [idProveedor]) VALUES (11, N'Cordones', 4)
INSERT [dbo].[tipoMP] ([id], [descripcion], [idProveedor]) VALUES (12, N'Plantilla', 4)
INSERT [dbo].[tipoMP] ([id], [descripcion], [idProveedor]) VALUES (13, N'Agujas', 5)
SET IDENTITY_INSERT [dbo].[tipoMP] OFF
GO
SET IDENTITY_INSERT [dbo].[transportista] ON 

INSERT [dbo].[transportista] ([id], [cuit], [nombre], [mail], [telefono], [idDomicilio]) VALUES (3, N'30-20461688-9', N'Expreso Cargo', N'cargo@gmail.com', N'4563666089', 314)
INSERT [dbo].[transportista] ([id], [cuit], [nombre], [mail], [telefono], [idDomicilio]) VALUES (4, N'20-20381664-8', N'LA PERLA S.A.', N'laperlaTransporte@outlook.com', N'351 34-28419', 1456)
SET IDENTITY_INSERT [dbo].[transportista] OFF
GO
SET IDENTITY_INSERT [dbo].[unidadDeMedida] ON 

INSERT [dbo].[unidadDeMedida] ([id], [descripcion]) VALUES (3, N'Plancha')
INSERT [dbo].[unidadDeMedida] ([id], [descripcion]) VALUES (4, N'Paquete')
INSERT [dbo].[unidadDeMedida] ([id], [descripcion]) VALUES (5, N'Mts')
INSERT [dbo].[unidadDeMedida] ([id], [descripcion]) VALUES (6, N'Mt2')
INSERT [dbo].[unidadDeMedida] ([id], [descripcion]) VALUES (7, N'Chapa')
INSERT [dbo].[unidadDeMedida] ([id], [descripcion]) VALUES (8, N'Unidad')
INSERT [dbo].[unidadDeMedida] ([id], [descripcion]) VALUES (9, N'Unidades')
INSERT [dbo].[unidadDeMedida] ([id], [descripcion]) VALUES (10, N'Ps')
INSERT [dbo].[unidadDeMedida] ([id], [descripcion]) VALUES (11, N'Gramos')
INSERT [dbo].[unidadDeMedida] ([id], [descripcion]) VALUES (12, N'Bolsa')
SET IDENTITY_INSERT [dbo].[unidadDeMedida] OFF
GO
SET IDENTITY_INSERT [dbo].[usuarios] ON 

INSERT [dbo].[usuarios] ([idUsuario], [dni], [nombre], [clave], [fechaIngreso], [fechaEgreso], [preg1], [preg2], [preg3], [nivel], [area], [region]) VALUES (1, 42246163, N'martin lopez', N'12345678', CAST(N'2023-07-31T00:52:40.350' AS DateTime), CAST(N'2023-07-31T00:52:48.347' AS DateTime), N'Alejo', N'2018', N'', N'Administrador', N'Administración', N'Fabincal')
INSERT [dbo].[usuarios] ([idUsuario], [dni], [nombre], [clave], [fechaIngreso], [fechaEgreso], [preg1], [preg2], [preg3], [nivel], [area], [region]) VALUES (3, 42246160, N'Analia rodriguez', N'12345678', CAST(N'2023-07-31T00:43:45.107' AS DateTime), CAST(N'2023-07-31T00:43:56.580' AS DateTime), N'anna', N'2016', N'', N'Usuario Normal', N'Ventas', N'Fabincal')
INSERT [dbo].[usuarios] ([idUsuario], [dni], [nombre], [clave], [fechaIngreso], [fechaEgreso], [preg1], [preg2], [preg3], [nivel], [area], [region]) VALUES (7, 42246159, N'Andrea Rodriguez', N'12345678', CAST(N'2023-07-31T00:46:03.960' AS DateTime), CAST(N'2023-07-31T00:46:17.060' AS DateTime), N'maira', N'2018', N'', N'Usuario Normal', N'Depósito', N'Fabincal ')
INSERT [dbo].[usuarios] ([idUsuario], [dni], [nombre], [clave], [fechaIngreso], [fechaEgreso], [preg1], [preg2], [preg3], [nivel], [area], [region]) VALUES (8, 42246189, N'Mariana', N'12345678', CAST(N'2023-07-31T00:38:08.977' AS DateTime), CAST(N'2023-07-31T00:38:16.153' AS DateTime), N'marianela', N'2016', N'', N'Usuario Normal', N'Fabricación', N'Fabincal ')
INSERT [dbo].[usuarios] ([idUsuario], [dni], [nombre], [clave], [fechaIngreso], [fechaEgreso], [preg1], [preg2], [preg3], [nivel], [area], [region]) VALUES (10, 43674368, N'Belen Juarez', N'1', CAST(N'2023-07-30T14:42:19.103' AS DateTime), CAST(N'2023-07-30T14:42:21.837' AS DateTime), N'no', NULL, NULL, N'Administrador', N'Administración', N'Fabincal ')
SET IDENTITY_INSERT [dbo].[usuarios] OFF
GO
USE [master]
GO
ALTER DATABASE [sistemaVND] SET  READ_WRITE 
GO
