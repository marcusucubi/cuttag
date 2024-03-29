USE [cuttagSKE]
GO
/****** Object:  UserDefinedFunction [dbo].[HC_WireComponents4BOM]    Script Date: 05/28/2012 23:41:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [dbo].[HC_WireComponents4BOM]
(
@OrganizationID int,
@PartID uniqueidentifier
) 
RETURNS TABLE 
AS
RETURN 
(
Select OrganizationID, PartID, PartNumber, PartVersion, PartRevision, ComponentSourceID, ComponentPartNumber, UserName, Count(UserName) AS CoTerm
 from HC_WireTableUnfiltered4BOM(@OrganizationID ,@PartID)
 WHERE UserName IS NOT NULL
 GROUP BY OrganizationID, PartID, PartNumber, PartVersion, PartRevision, ComponentSourceID, ComponentPartNumber, UserName
UNION ALL
SELECT OrganizationID, partID, PartNumber, PartVersion, PartRevision, ComponentSourceID, ComponentPartNumber, 'NoName', 1 AS CoTerm
 FROM HC_WireTableUnfiltered4BOM(@OrganizationID ,@PartID)
 WHERE UserName IS NULL AND ComponentPartNumber IS NOT NULL
)
