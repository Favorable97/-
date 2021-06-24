USE [Test]
GO

/****** Object:  Table [dbo].[ExchangeRates]    Script Date: 24.06.2021 13:40:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ExchangeRates](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDVAL] [varchar](50) NOT NULL,
	[NUMCODE] [varchar](50) NOT NULL,
	[CHARCODE] [varchar](50) NOT NULL,
	[NOMINAL] [int] NOT NULL,
	[NAME] [varchar](100) NOT NULL,
	[VALUE] [real] NOT NULL,
	[DATE] [date] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

