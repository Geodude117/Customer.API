CREATE PROCEDURE [dbo].[Customer_Insert]
	@Forename nvarchar(50),
	@Surename nvarchar(50),
	@DateOfBirth nvarchar(50),
	@HouseNo int,
	@Street nvarchar(100),
	@City nvarchar(100),
	@PostCode nvarchar(100)
AS
DECLARE @Uniqueidentifier uniqueidentifier  = NEWID();
DECLARE @Address_Id int

	INSERT INTO [dbo].[Customer]
           ([ID],
		   [Forename]
           ,[Surename]
           ,[DateOfBirth])
     VALUES
           (@Uniqueidentifier,
		    @Forename
           ,@Surename
           ,@DateOfBirth)


	INSERT INTO [dbo].[Address]
           ([HouseNo]
           ,[Street]
           ,[City ]
           ,[Postcode]
           ,[CustomerId])
     VALUES
           (@HouseNo
           ,@Street
           ,@City
           ,@PostCode
           ,@Uniqueidentifier)

	SET @Address_Id = SCOPE_IDENTITY();

	UPDATE [dbo].[Customer]
	SET [AddressId] = @Address_Id
	WHERE Customer.AddressId = @Address_Id

RETURN
GO


