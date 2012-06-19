USE [cuttagSKE]
GO

/****** Object:  UserDefinedFunction [dbo].[HC_Fct_GetMultiConductorCable4BOM]    Script Date: 05/28/2012 23:15:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[HC_Fct_GetMultiConductorCable4BOM]
(
@OrganizationID int,
@PartID uniqueidentifier
) 
RETURNS TABLE 
AS
RETURN 
(
SELECT OrganizationID, PartNumber, PartVersion, PartRevision, WireSourceID, WireDescription,
 MAX(CableID) AS CableNumber, MAX(WirePartNumber) AS WirePartNumber, MAX(RefLength) AS MaxRefLength
FROM HC_WireTableUnfiltered4BOM(@OrganizationID,@PartID)
WHERE CableID<>0
GROUP BY OrganizationID, PartNumber, PartVersion, PartRevision, WireSourceID , WireDescription, CableID
 )



GO
USE [cuttagSKE]
GO

/****** Object:  UserDefinedFunction [dbo].[HC_Fct_GetPartComponents4BOM]    Script Date: 05/28/2012 23:15:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[HC_Fct_GetPartComponents4BOM]
(
@OrganizationID int,
@PartID uniqueidentifier
) 
RETURNS TABLE 
AS
RETURN 
(
SELECT p.OrganizationID, p.PartNumber, p.PartVersion, p.PartRevision, pc.ComponentSourceID,
 s.PartNumber AS ComponentPartNumber, 'NA' AS UserName, Count(*) AS Qty,
 s.UOM, s.Description
FROM Part p 
 INNER JOIN PartComponent pc ON pc.PartID = p.PartID
  LEFT JOIN WireComponentSource s ON s.WireComponentSourceID = pc.ComponentSourceID
 WHERE p.OrganizationID = @OrganizationID AND p.PartID = @PartID
 GROUP BY p.OrganizationID, p.PartVersion, p.PartRevision,
  pc.ComponentSourceID, p.PartNumber, s.PartNumber,s.UOM, s.Description
)


GO


USE [cuttagSKE]
GO

/****** Object:  UserDefinedFunction [dbo].[HC_Fct_GetSingleConductors4BOM]    Script Date: 05/28/2012 23:15:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[HC_Fct_GetSingleConductors4BOM]
(
@OrganizationID int,
@PartID uniqueidentifier
) 
RETURNS TABLE 
AS
RETURN 
(
	SELECT OrganizationID, PartNumber, PartVersion, PartRevision, WireSourceID, WireNumber, LNode, RNode,
	 WirePartNumber, WireDescription ,RefLength
	FROM HC_WireTableUnfiltered4BOM(@OrganizationID, @PartID)
	WHERE CableID=0
	GROUP BY OrganizationID, PartNumber, PartVersion, PartRevision, WireSourceID, WireNumber, LNode, RNode,
	 WirePartNumber, WireDescription, RefLength
 )



GO
USE [cuttagSKE]
GO

/****** Object:  UserDefinedFunction [dbo].[HC_Fct_GetWireComponents4BOM]    Script Date: 05/28/2012 23:16:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[HC_Fct_GetWireComponents4BOM]
(
@OrganizationID int,
@PartID uniqueidentifier
) 
RETURNS TABLE 
AS
RETURN 
(
SELECT t.*, s.Description, s.UOM FROM
 (  
  SELECT d.OrganizationID, d.PartID, d.PartNumber, d.PartRevision, d.PartVersion, d.ComponentSourceID,
   d.ComponentPartNumber, d.UserName, d.CoTerm, s.UniqueCount FROM
   (
    (SELECT * FROM HC_WireComponents4BOM(@OrganizationID,@PartID)) d
    LEFT JOIN 
    (SELECT c.ComponentSourceID,  COUNT(*) AS UniqueCount FROM HC_WireComponents4BOM(@OrganizationID,@PartID) c 
    GROUP BY c.ComponentSourceID) s
    ON s.ComponentSourceID = d.ComponentSourceID
   ) 
 ) t
 LEFT JOIN WireComponentSource s ON s.WireComponentSourceID = t.ComponentSourceID
)


GO



