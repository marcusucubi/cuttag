USE [cuttagSKE]
GO

/****** Object:  UserDefinedFunction [dbo].[HQ_GetNextLow_QuoteID]    Script Date: 01/03/2012 10:01:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		DCS
-- Create date: 1/2/2012
-- Description:
-- =============================================
CREATE FUNCTION [dbo].[HQ_GetNextLow_QuoteID] 
()
RETURNS int
AS
BEGIN
DECLARE @MaxLowID Int = 0
SELECT @MaxLowID=(ID) FROM _Quote 
  WHERE ID < 10000
RETURN @MaxLowID
END



GO

