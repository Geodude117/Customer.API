CREATE PROCEDURE [dbo].[ContactInformation_Delete_By_Id]
	@CustomerId uniqueidentifier  
AS
DELETE FROM [dbo].[ContactInformation]
      WHERE [ContactInformation].CustomerId = @CustomerId
RETURN 1
