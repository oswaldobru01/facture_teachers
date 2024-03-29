USE [master]
GO
/****** Object:  Database [facture_profesores]    Script Date: 07/02/2022 20:46:33 ******/
CREATE DATABASE [facture_profesores]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'facture_profesores', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.OSWALDOBRU\MSSQL\DATA\facture_profesores.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'facture_profesores_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.OSWALDOBRU\MSSQL\DATA\facture_profesores_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [facture_profesores] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [facture_profesores].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [facture_profesores] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [facture_profesores] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [facture_profesores] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [facture_profesores] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [facture_profesores] SET ARITHABORT OFF 
GO
ALTER DATABASE [facture_profesores] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [facture_profesores] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [facture_profesores] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [facture_profesores] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [facture_profesores] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [facture_profesores] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [facture_profesores] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [facture_profesores] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [facture_profesores] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [facture_profesores] SET  DISABLE_BROKER 
GO
ALTER DATABASE [facture_profesores] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [facture_profesores] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [facture_profesores] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [facture_profesores] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [facture_profesores] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [facture_profesores] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [facture_profesores] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [facture_profesores] SET RECOVERY FULL 
GO
ALTER DATABASE [facture_profesores] SET  MULTI_USER 
GO
ALTER DATABASE [facture_profesores] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [facture_profesores] SET DB_CHAINING OFF 
GO
ALTER DATABASE [facture_profesores] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [facture_profesores] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [facture_profesores] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [facture_profesores] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'facture_profesores', N'ON'
GO
ALTER DATABASE [facture_profesores] SET QUERY_STORE = OFF
GO
USE [facture_profesores]
GO
/****** Object:  Table [dbo].[Lessons]    Script Date: 07/02/2022 20:46:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lessons](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idTeacher] [int] NOT NULL,
	[date] [datetime] NOT NULL,
	[course] [varchar](10) NOT NULL,
	[dictatedHours] [int] NOT NULL,
	[Value] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_Lessons] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 07/02/2022 20:46:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[Name] [varchar](200) NOT NULL,
	[Identification] [varchar](20) NOT NULL,
	[Type] [varchar](10) NOT NULL,
	[PaymentCurrent] [varchar](3) NOT NULL,
	[HourlyRate] [numeric](18, 2) NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Equivalence] [numeric](18, 2) NOT NULL,
 CONSTRAINT [PK_Teachers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Teachers] ADD  CONSTRAINT [DF_Teachers_Equivalence]  DEFAULT ((0)) FOR [Equivalence]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_Id] FOREIGN KEY([idTeacher])
REFERENCES [dbo].[Teachers] ([Id])
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Teacher_Id]
GO
/****** Object:  StoredProcedure [dbo].[GetNominaInPeriod]    Script Date: 07/02/2022 20:46:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[GetNominaInPeriod]
(
	@_year INT = 0,
	@_month INT = 0
)
AS 
BEGIN 

	SELECT	T.Id,T.Identification,
			T.Name,
			T.PaymentCurrent, 
			T.HourlyRate, 
			T.Type, 
			SUM(L.Value)  AS Value
	FROM dbo.Lessons L
		LEFT JOIN dbo.Teachers T on L.idTeacher = T.Id
	WHERE YEAR(L.date) = @_year AND MONTH(L.date) = @_month
	GROUP BY T.Id,T.Identification,t.Name,t.PaymentCurrent, t.HourlyRate, t.Type

END
GO
USE [master]
GO
ALTER DATABASE [facture_profesores] SET  READ_WRITE 
GO
