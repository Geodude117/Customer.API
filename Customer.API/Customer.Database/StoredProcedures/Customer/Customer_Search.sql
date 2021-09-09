CREATE PROCEDURE [dbo].[Customer_Search]
    @Forename nvarchar(50),
	@Surename nvarchar(50),
	@DateOfBirth nvarchar(50),
	@EmailAddress nvarchar(50)
AS
	
DECLARE @results TABLE (Id uniqueidentifier not null)

	--SEARCH INSIDE CUSTOMER TABLE

	IF (@Forename is not null or @Surename is not null or @DateOfBirth is not null )
	BEGIN
		INSERT INTO @results
			SELECT
				rd.Id
			FROM
				dbo.Customer rd
			WHERE
				((@Forename is null) or (@Forename is not null and @Forename = rd.Forename))
			and ((@Surename is null) or (@Surename is not null and @Surename = rd.Surename))
			and ((@DateOfBirth is null) or (@DateOfBirth is not null and @DateOfBirth = rd.DateOfBirth))
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
