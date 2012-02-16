USE [cuttagSKE]
GO

/****** Object:  StoredProcedure [dbo].[HC_Inf_InsertCustomer]    Script Date: 02/15/2012 18:17:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- ==============================================
-- Author:		DCS
-- Create date: 1/12/12
-- Description: Add New Customers from ERP System	
-- ==============================================
ALTER PROCEDURE [dbo].[HC_Inf_InsertCustomer]
(
@Rows int OUT
)
AS
BEGIN
DECLARE @CustomerNumber_inf int
DECLARE @CustomerName_inf nvarchar(50)
DECLARE @NewCustomerID int
SET @Rows = 0
BEGIN TRANSACTION
	DECLARE c CURSOR FOR
	SELECT o1.No_, o1.Name FROM [Navision].dbo.[SK Express Inc_$Customer]o1
	  left join Customer_Inf i ON  o1.No_ COLLATE DATABASE_DEFAULT = i.CustomerID_Inf
	  left join Customer c ON c.CustomerID = i.CustomerID 
    WHERE  i.CustomerID is null
	OPEN c
	FETCH NEXT FROM c INTO
		@CustomerNumber_inf, @CustomerName_inf 
	WHILE @@FETCH_STATUS = 0
	BEGIN
		INSERT Customer (CustomerName, Active)
		  VALUES (@CustomerName_inf, 1)
		SET @NewCustomerID = @@IDENTITY 
		INSERT Customer_Inf VALUES (@NewCustomerID, @CustomerNumber_inf) 
		SET @Rows = @Rows + @@ROWCOUNT
		FETCH NEXT FROM c INTO
		@CustomerNumber_inf, @CustomerName_inf 
	END
	CLOSE c
	DEALLOCATE c
COMMIT TRANSACTION
END


GO

