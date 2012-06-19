USE [cuttagSKE]
GO

/****** Object:  UserDefinedFunction [dbo].[HC_WireTableUnfiltered4BOM]    Script Date: 05/28/2012 23:31:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER FUNCTION [dbo].[HC_WireTableUnfiltered4BOM]
(
@OrganizationID int,
@PartID uniqueidentifier
) 
RETURNS TABLE 
AS
RETURN 
(
SELECT p.partID, @OrganizationID As OrganizationID, p.PartNumber, p.PartVersion, p.PartRevision, w.WireSourceID,
w.CableID, w.ConductorNumber, w.WireNumber, w.LNode, w.RNode, ws.PartNumber AS WirePartNumber, ws.Description as WireDescription, 
w.RefLength, ws.Color, g.Gage, wt.WireType, wc.WireComponentSourceID AS ComponentSourceID, wcs.PartNumber AS ComponentPartNumber, 
wc.UserName, wc.Position, wcs.ClassID 
FROM part AS p 
LEFT JOIN Wire AS w ON w.PartID = p.PartID
 LEFT JOIN WireSource AS ws ON w.WireSourceID = ws.WireSourceID
  LEFT JOIN Gage AS g ON ws.GageID = g.GageID
   LEFT JOIN WireType AS wt ON ws.WireTypeID = wt.WireTypeID
    LEFT JOIN WireComponent AS wc ON wc.WireID = w.WireID
     LEFT JOIN WireComponentSource AS wcs ON wcs.WireComponentSourceID = wc.WireComponentSourceID
 WHERE p.PartID = @PartID
    AND p.OrganizationID = @OrganizationID
 )


GO


