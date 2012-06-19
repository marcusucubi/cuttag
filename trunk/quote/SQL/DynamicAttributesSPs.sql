USE [cuttagSKE]
GO

/****** Object:  StoredProcedure [dbo].[HQ_GetPropertyAttributeMasterJoined]    Script Date: 05/28/2012 23:52:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- ==========================================================================================
-- Author:		DCS
-- Create date: 3/18/12
-- Description:	Get Available PropertyAttribute for us with Dynamic Properties
-- ==========================================================================================
CREATE PROCEDURE [dbo].[HQ_GetPropertyAttributeMasterJoined]
AS
BEGIN
	SET NOCOUNT ON;
SELECT m.PropertyAttributeMasterID, m.AttributeName,
 t.AttributeType FROM _PropertyAttributeMaster m
  INNER JOIN _PropertyAttributeType t
    ON t.PropertyAttributeTypeID = m.PropertyAttributeTypeID
END






GO


USE [cuttagSKE]
GO

/****** Object:  StoredProcedure [dbo].[HQ_GetDynamicProperties4Container]    Script Date: 05/28/2012 23:51:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- ==========================================================================================
-- Author:		DCS
-- Create date: 3/4/12
-- Description:	Get Dynamic Properties  for one Container - e.g. Computation Properies
-- ==========================================================================================
CREATE PROCEDURE [dbo].[HQ_GetDynamicProperties4Container]
(
  @PropertyContainerID int 
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  p.DynamicPropertyID, p.PropertyContainerID, p.Name, p.PropertyTypeID FROM dbo._DynamicProperties p
		WHERE p.PropertyContainerID = @PropertyContainerID 

END






GO


USE [cuttagSKE]
GO

/****** Object:  StoredProcedure [dbo].[HQ_GetDynamicAttributes4Property]    Script Date: 05/28/2012 23:51:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- ==========================================================================================
-- Author:		DCS
-- Create date: 3/4/12
-- Description:	Get Attributes Properties  for one Property 
-- ==========================================================================================
CREATE PROCEDURE [dbo].[HQ_GetDynamicAttributes4Property]
(
  @DynamicPropertyID int 
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT a.DynamicPropertyID, a.PropertyAttributeMasterID, a.Value FROM  dbo._DynamicPropertyAttributes a 
		WHERE a.DynamicPropertyID = @DynamicPropertyID  

END






GO


