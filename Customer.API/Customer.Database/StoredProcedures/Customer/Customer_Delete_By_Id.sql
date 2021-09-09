CREATE PROCEDURE [dbo].[Customer_Delete_By_Id]
	@CustomerId uniqueidentifier
AS
	DELETE FROM [Address]
	WHERE [Address].CustomerId = @CustomerId

	DELETE FROM [Customer]
	WHERE [Customer].Id = @CustomerId

	DELETE FROM [ContactInformation]
	WHERE [ContactInformation].CustomerId = @CustomerId

RETURN 1
