USE [master]
GO
/****** Object:  Database [facture_profesores]    Script Date: 07/02/2022 01:46:17 ******/
CREATE DATABASE [facture_profesores]
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [facture_profesores] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [facture_profesores].[dbo].[sp_fulltext_database] @action = 'enable'
end

GO
EXEC sys.sp_db_vardecimal_storage_format N'facture_profesores', N'ON'
GO
ALTER DATABASE [facture_profesores] SET QUERY_STORE = OFF
GO
USE [facture_profesores]
GO
/****** Object:  Table [dbo].[Lessons]    Script Date: 07/02/2022 01:46:17 ******/
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
/****** Object:  Table [dbo].[Teachers]    Script Date: 07/02/2022 01:46:17 ******/
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
 CONSTRAINT [PK_Teachers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lessons]  WITH CHECK ADD  CONSTRAINT [FK_Teacher_Id] FOREIGN KEY([idTeacher])
REFERENCES [dbo].[Teachers] ([Id])
GO
ALTER TABLE [dbo].[Lessons] CHECK CONSTRAINT [FK_Teacher_Id]
GO
USE [master]
GO
ALTER DATABASE [facture_profesores] SET  READ_WRITE 
GO
