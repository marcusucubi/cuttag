USE [cuttagSKE]
GO

/****** Object:  UserDefinedFunction [dbo].[DCS_RoundUp]    Script Date: 05/28/2012 23:11:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ============================================================================
-- Author:		DCS
-- Create date: 5/10/2012
-- Description: Round decimal arguement up to next digit
--              @RoundDigit=Rounding Precision:2=hundredthe,-2=hundreds,0=units
-- ============================================================================
CREATE FUNCTION [dbo].[DCS_RoundUp]
(
	@Number2Round decimal(18,6),
	@RoundDigits int
)
RETURNS decimal(18, 6)
AS
BEGIN
	DECLARE @Base decimal(18,6) = 10
	DECLARE @RetValue decimal(18,6)
	SET @RetValue = CEILING(@Number2Round*Power(@Base,@RoundDigits))/(Power(@Base,@RoundDigits))

	RETURN @RetValue 
END





GO


