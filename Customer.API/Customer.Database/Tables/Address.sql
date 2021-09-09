CREATE TABLE [dbo].[Address]
(
	[Id] INT  PRIMARY KEY NOT NULL IDENTITY, 
    [HouseNo] INT , 
    [Street] NVARCHAR(100) , 
    [City ] NVARCHAR(100) , 
    [Postcode] NVARCHAR(10) NULL  , 
    [CustomerId] UNIQUEIDENTIFIER NULL  , 
    CONSTRAINT [FK_Address_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id])
)
