USE [Test]
GO

/****** Object:  UserDefinedFunction [dbo].[GetRate]    Script Date: 24.06.2021 13:41:56 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Function [dbo].[GetRate] (@name varchar(100), @dt date) 
	returns real 
	begin
		declare @result real
		select @result = VALUE from ExchangeRates where name = @name and DATE = @dt
		return @result
	end;
GO

