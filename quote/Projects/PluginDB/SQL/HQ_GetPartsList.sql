USE [cuttagSKE]
GO

/****** Object:  StoredProcedure [dbo].[HQ_GetPartsList]    Script Date: 05/28/2012 22:35:02 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- ==========================================================================================
-- Author:		DCS
-- Create date: 5/17/12
-- Description:	Get PartsList for imported quote from WHC
-- ==========================================================================================
CREATE PROCEDURE [dbo].[HQ_GetPartsList]
(
	@PartID uniqueidentifier,
	@IsComputed bit
) 
AS
BEGIN
	SET NOCOUNT ON;
SELECT l.PartID, l.SourceID, l.PartNumber, l.Qty from PartsList l
  WHERE l.PartID =  @PartID AND l.IsComputed = @IsComputed
END






GO


