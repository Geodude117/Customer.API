CREATE PROCEDURE [dbo].[Customer_Search]
    @Forename nvarchar(50),
	@Surename nvarchar(50),
	@PostCode nvarchar(10),
	@EmailAddress nvarchar(50)
AS
	
DECLARE @results TABLE (Id uniqueidentifier not null)

	--SEARCH INSIDE CUSTOMER TABLE

	IF (@Forename is not null or @Surename is not null )
	BEGIN
		INSERT INTO @results
			SELECT TOP 100
				CU.Id
			FROM
				dbo.Customer CU
			WHERE
				(@Forename = CU.Forename)
			or (@Surename = CU.Surename)
	END

	--SEARCH INSIDE ADDRESS TABLE

	IF (@PostCode is not null)
	BEGIN
		INSERT INTO @results
			SELECT TOP 100
				AD.CustomerId
			FROM
				dbo.[Address] AD
			WHERE
				(@PostCode = AD.Postcode)
	END

	--SEARCH INSIDE CONTACT INformation table - (EMAIL ADDRESS)

	INSERT INTO @results
			SELECT DISTINCT
				CI.CustomerId
			FROM 
				dbo.ContactInformation CI
			WHERE
				CI.[Type] = 'emailaddress' and @EmailAddress = @EmailAddress

SELECT * FROM @results

RETURN 0

GO
