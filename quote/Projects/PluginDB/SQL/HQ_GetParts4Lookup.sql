USE [cuttagSKE]
GO

/****** Object:  StoredProcedure [dbo].[HQ_GetParts4Lookup]    Script Date: 05/28/2012 22:35:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- ==========================================================================================
-- Author:		DCS
-- Create date: 5/17/12
-- Description:	Get Parts Lookup list for imported quote from WHC
-- ==========================================================================================
CREATE PROCEDURE [dbo].[HQ_GetParts4Lookup]
AS
BEGIN
	SET NOCOUNT ON;
SELECT p.PartID,
 (p.PartNumber + ' | ' + p.PartVersion + ' | ' + p.PartRevision) + 
	 CASE ISNULL(p.PartNumberAlt,'')
	   WHEN ''  THEN ''
	   ELSE ' (' + p.PartNumberAlt + ')'
	 END
 AS Display,
 p.PartNumber, p.PartVersion, p.PartRevision, p.PartNumberAlt, p.CustomerID, p.StatusID
 FROM Part p
 ORDER BY p.PartNumber, p.PartVersion, p.PartRevision, p.PartNumberAlt
END






GO


