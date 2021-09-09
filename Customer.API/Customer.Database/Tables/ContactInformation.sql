CREATE TABLE [dbo].[ContactInformation]
(
	[Id] INT  PRIMARY KEY NOT NULL IDENTITY, 
    [Type] NVARCHAR(50), 
    [Value] NVARCHAR(50), 
    [CustomerId] UNIQUEIDENTIFIER NULL, 
)
