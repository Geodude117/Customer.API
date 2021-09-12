CREATE PROCEDURE [dbo].[Address_Insert]
	@HouseNo int,
	@Street nvarchar(100),
	@City nvarchar(100),
	@PostCode nvarchar(100),
	@CustomerId uniqueidentifier  
AS
	INSERT INTO [dbo].[Address]
           ([HouseNo]
           ,[Street]
           ,[City]
           ,[Postcode]
           ,[CustomerId])
     VALUES
           (@HouseNo
           ,@Street
           ,@City
           ,@PostCode
           ,@CustomerId)
RETURN
GO
