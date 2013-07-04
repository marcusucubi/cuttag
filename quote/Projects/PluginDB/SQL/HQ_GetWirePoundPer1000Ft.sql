USE [cuttagSKE]
GO

/****** Object:  StoredProcedure [dbo].[HQ_GetWirePoundsPer1000Ft]    Script Date: 02/17/2012 22:37:47 ******/
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
DECLARE @WireSourceConductorCount int
DECLARE @ConductorSourceConductorCount int
DECLARE @ConductorNumber int
DECLARE @CopperWtPerFt Decimal(18,6)
DECLARE @ConductorWeight Decimal(18,6)
DECLARE @TotalWeight Decimal(18,6) = 0
SELECT @WireSourceConductorCount=s.ConductorCount, @CopperWtPerFt=s.CopperWtPerFt
	 FROM WIRESOURCE s
	WHERE WireSourceID = @WireSourceID
IF @CopperWtPerFt IS NULL
BEGIN --WireSource	has no Weight
	IF @WireSourceConductorCount > 1 
	BEGIN --Has Multi-Conductor
		SELECT @ConductorSourceConductorCount=COUNT(*) FROM ConductorSource s
			WHERE s.WireSourceID = @WireSourceID
		IF @ConductorSourceConductorCount = @WireSourceConductorCount
		BEGIN --Has correct number of rows in ConductorSource
			DECLARE c CURSOR FOR
			SELECT s.WireSourceID, c.ConductorNumber FROM WireSource s
				LEFT JOIN ConductorSource c ON c.WireSourceID = s.WireSourceID
						WHERE NOT c.WireSourceID IS NULL AND s.WireSourceID = @WireSourceID
			OPEN c
			FETCH NEXT FROM c INTO
				@WireSourceID, @ConductorNumber 
			WHILE @@FETCH_STATUS = 0
			BEGIN
        SET @ConductorWeight = dbo.HQ_GetConductorPoundsPer1000Ft(@WireSourceID,@ConductorNumber) 
			  SET @TotalWeight = @TotalWeight + @ConductorWeight
				FETCH NEXT FROM c INTO
					@WireSourceID, @ConductorNumber
			END
			CLOSE c
			DEALLOCATE c
		END
		ELSE --Has correct number of rows in ConductorSource
		BEGIN
		  SET @TotalWeight = dbo.HQ_GetConductorPoundsPer1000Ft(@WireSourceID,0)
		END
	END
	ELSE
	BEGIN --Is Single Conductor
    SET @TotalWeight = dbo.HQ_GetConductorPoundsPer1000Ft(@WireSourceID,0) 
	END
END
ELSE
BEGIN	--WireSource	has Weight
	SET @TotalWeight=@CopperWtPerFt*1000
END
SET @ErrorMessage = 'Test'
SELECT @TotalWeight
END



GO

