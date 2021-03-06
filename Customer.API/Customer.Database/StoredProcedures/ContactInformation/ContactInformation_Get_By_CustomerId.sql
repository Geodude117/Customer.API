CREATE PROCEDURE [dbo].[ContactInformation_Get_By_CustomerId]
	@CustomerId uniqueidentifier
AS
	SELECT [Id]
      ,[Type]
      ,[Value]
      ,[CustomerId]
  FROM [dbo].[ContactInformation] CI
  WHERE CI.CustomerId = @CustomerId
RETURN 0
