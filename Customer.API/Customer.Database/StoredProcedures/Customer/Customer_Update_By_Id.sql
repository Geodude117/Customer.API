CREATE PROCEDURE [dbo].[Customer_Update_By_Id]
	@CustomerId uniqueidentifier,
	@Forename nvarchar(50),
	@Surename nvarchar(50),
	@DateOfBirth nvarchar(50),
	@HouseNo int,
	@Street nvarchar(100),
	@City nvarchar(100),
	@Postcode nvarchar(10)
AS
	UPDATE [dbo].[Customer]
	SET [Forename] = @Forename
      ,[Surename] = @Surename
      ,[DateOfBirth] = @DateOfBirth
	WHERE Customer.Id = @CustomerId

	UPDATE [dbo].[Address]
	SET [HouseNo] = @HouseNo
      ,[Street] = @Street
      ,[City ] = @City
      ,[Postcode] = @Postcode
	WHERE Address.CustomerId = @CustomerId

RETURN 1