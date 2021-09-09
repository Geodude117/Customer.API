CREATE PROCEDURE [dbo].[Customer_Get_By_Id]
	@Id uniqueidentifier  
AS
	SELECT 
       [Id]
      ,[Forename]
      ,[Surename]
      ,[DateOfBirth]
      ,[AddressId]
      FROM [Customer].[dbo].[Customer] CU
      WHERE CU.[Id] = @Id
  RETURN 0