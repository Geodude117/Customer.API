CREATE PROCEDURE [dbo].[Customer_Insert]
	@CustomerId uniqueidentifier,
    @Forename nvarchar(50),
	@Surename nvarchar(50),
	@DateOfBirth nvarchar(50)
AS

	INSERT INTO [dbo].[Customer]
           ([ID],
		   [Forename]
           ,[Surename]
           ,[DateOfBirth])
     VALUES
           (@CustomerId,
		    @Forename
           ,@Surename
           ,@DateOfBirth)

RETURN 1
GO


