CREATE PROCEDURE [dbo].[sp_InsertArtist]
    @Name nvarchar(50),
    @Country nvarchar(50)
AS
    INSERT INTO artists(Name, Country_Id)
	IF @Country IN (SELECT Name FROM coutries)
    VALUES (@name, @Country_Id)
  
    SELECT SCOPE_IDENTITY()
GO

CREATE PROCEDURE sp_GetArtists
AS
    SELECT * FROM artists
	
CREATE PROCEDURE sp_GetCountries
AS
    SELECT * FROM countries
	
CREATE PROCEDURE sp_GetArtistsWithCountries
AS
    SELECT a.Id, a.Name, c.Name
	FROM artists a
	JOIN countries c on c.Id = a.Country_Id	
	
	
CREATE PROCEDURE sp_InsertArtist
    @Name nvarchar(50),
    @Country nvarchar(50)
AS
	DECLARE @C_Id INT

	IF EXISTS (SELECT Id FROM countries WHERE Name like @Country)
	BEGIN
		SET @C_Id = (SELECT Id FROM countries WHERE Name like @Country)
	    INSERT INTO artists(Name, Country_Id) VALUES (@Name, @C_Id)
	END