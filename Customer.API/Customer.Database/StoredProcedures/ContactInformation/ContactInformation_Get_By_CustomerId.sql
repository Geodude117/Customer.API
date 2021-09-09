CREATE PROCEDURE [dbo].[ContactInformation_Get_By_CustomerId]
	@CustomerId uniqueidentifier,
	@param2 int
AS
	SELECT [Id]
      ,[Type]
      ,[Value]
      ,[CustomerId]
  FROM [dbo].[ContactInformation] CI
  WHERE CI.CustomerId = @CustomerId
RETURN 0
