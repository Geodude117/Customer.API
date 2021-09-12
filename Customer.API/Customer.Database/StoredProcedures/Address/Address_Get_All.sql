CREATE PROCEDURE [dbo].[Address_Get_All]
AS
	SELECT [Id]
      ,[HouseNo]
      ,[Street]
      ,[City]
      ,[Postcode]
      ,[CustomerId]
  FROM [dbo].[Address]
RETURN 0
