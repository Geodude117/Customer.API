CREATE PROCEDURE [dbo].[Address_Delete_By_CustomerId]
	@CustomerId uniqueidentifier  
AS
	DELETE FROM [dbo].[Address] 
      WHERE CustomerId = @CustomerId
RETURN 0
