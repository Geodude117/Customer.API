CREATE PROCEDURE [dbo].[Customer_Get_All]
AS
	SELECT 
       [Id]
      ,[Forename]
      ,[Surename]
      ,[DateOfBirth]
      ,[AddressId]
      FROM [Customer].[dbo].[Customer] 
RETURN 0
