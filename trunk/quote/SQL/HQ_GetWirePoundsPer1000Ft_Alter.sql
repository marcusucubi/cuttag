USE [cuttagSKE]
GO

/****** Object:  StoredProcedure [dbo].[HQ_GetWirePoundsPer1000Ft]    Script Date: 05/28/2012 22:34:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		DCS
-- Create date: 12/27/11
-- Description:	
-- =============================================
ALTER PROCEDURE [dbo].[HQ_GetWirePoundsPer1000Ft]
@WireSourceID uniqueidentifier,
@ErrorMessage nvarchar(100) OUT 
AS
BEGIN
SELECT dbo.HQ_Fct_GetWirePoundsPer1000Ft(@WireSourceID)
SET @ErrorMessage = 'TestMessage'
END



GO


