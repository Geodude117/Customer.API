CREATE PROCEDURE [dbo].[Customer_Update_By_Id]
	@Id uniqueidentifier,
	@Forename nvarchar(50),
	@Surename nvarchar(50),
	@DateOfBirth nvarchar(50),
	@AddressId nvarchar(50) 
AS
	UPDATE [dbo].[Customer]
	SET [Forename] = @Forename
      ,[Surename] = @Surename
      ,[DateOfBirth] = @DateOfBirth
      ,[AddressId] = @AddressId
	WHERE Customer.Id = @Id
RETURN SCOPE_IDENTITY()
GO
