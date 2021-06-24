USE [Test]
GO

/****** Object:  Table [dbo].[DirectoryCurrency]    Script Date: 24.06.2021 13:39:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[DirectoryCurrency](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDITEM] [varchar](50) NOT NULL,
	[NAME] [varchar](100) NOT NULL,
	[ENGNAME] [varchar](100) NOT NULL,
	[NOMINAL] [int] NOT NULL,
	[PARENTCODE] [varchar](50) NOT NULL,
	[ISONUMCODE] [int] NOT NULL,
	[ISOCHARCODE] [varchar](50) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

