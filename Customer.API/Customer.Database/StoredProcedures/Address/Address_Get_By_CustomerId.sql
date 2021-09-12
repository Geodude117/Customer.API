CREATE PROCEDURE [dbo].[Address_Get_By_CustomerId]
	@CustomerId uniqueidentifier  
AS
	SELECT [Id]
      ,[HouseNo]
      ,[Street]
      ,[City]
      ,[Postcode]
      ,[CustomerId]
  FROM [dbo].[Address] A
  WHERE A.CustomerId = @CustomerId
RETURN 0
