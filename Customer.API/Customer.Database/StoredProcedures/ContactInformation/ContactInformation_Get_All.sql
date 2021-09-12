CREATE PROCEDURE [dbo].[ContactInformation_Get_All]
AS
	SELECT [Id]
      ,[Type]
      ,[Value]
      ,[CustomerId]
  FROM [dbo].[ContactInformation]
RETURN 0
