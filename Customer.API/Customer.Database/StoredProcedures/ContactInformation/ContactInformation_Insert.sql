CREATE PROCEDURE [dbo].[ContactInformation_Insert]
	@Type nvarchar(50),
	@Value nvarchar(50),
	@CustomerId uniqueidentifier  
AS
	INSERT INTO [dbo].[ContactInformation]
           ([Type]
           ,[Value]
           ,[CustomerId])
     VALUES
           (@Type
           ,@Value
           ,@CustomerId)
RETURN 0
