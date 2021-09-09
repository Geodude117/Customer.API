CREATE PROCEDURE [dbo].[CustomerInformation_Delete_By_CustomerId]
	@CustomerId uniqueidentifier  
AS
	DELETE FROM [dbo].[ContactInformation] 
      WHERE CustomerId = @CustomerId
RETURN 0
