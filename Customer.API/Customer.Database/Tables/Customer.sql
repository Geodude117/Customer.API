CREATE TABLE [dbo].[Customer]
(
	[Id] UNIQUEIDENTIFIER  NOT NULL PRIMARY KEY,
    [Forename] NVARCHAR(50)  , 
    [Surename] NVARCHAR(50) , 
    [DateOfBirth] NVARCHAR(50)  , 
    [AddressId] INT NULL , 
    CONSTRAINT [FK_Customer_Address] FOREIGN KEY ([AddressId]) REFERENCES [Address]([Id])
)
