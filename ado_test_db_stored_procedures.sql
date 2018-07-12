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
	
CREATE PROCEDURE sp_InsertTrack
	@album varchar,
	@artist varchar,
	@genre varchar,
	@title varchar,
	@year varchar,
	@duration varchar,
	@trackNumber int
AS
	DECLARE @genre_id int;
	DECLARE @artist_id int;
	DECLARE @album_id int;
	
	IF EXISTS (SELECT * FROM genres WHERE Name LIKE @genre)
		BEGIN
			SET @genre_id = (SELECT Id FROM genres WHERE Name LIKE @genre);
		END
	ELSE
		BEGIN
			SET @genre_id = 144; --uknown
		END

	IF EXISTS (SELECT * FROM artists WHERE Name LIKE @artist)
		BEGIN
			SET @artist_id = (SELECT Id FROM artists WHERE Name LIKE @artist);
		END
	ELSE
		BEGIN
			INSERT INTO artists(Name)
			SELECT @artist;
			SET @artist_id = (SELECT Id FROM artists WHERE Name LIKE @artist);
		END

	IF EXISTS (SELECT * FROM albums WHERE Name LIKE @album)
		BEGIN
			SET @album_id = (SELECT Id FROM albums WHERE Name LIKE @album);
		END
	ELSE
		BEGIN
			INSERT INTO albums(Name,Year,Artist_Id)
			VALUES (@album, @year, @artist_id);
			SET @album_id = (SELECT Id FROM albums WHERE Name LIKE @album);
		END

	INSERT INTO tracks (Name, Album_Id, Artist_Id, Genre_Id, Duration, TrackNumber)
	VALUES (@title, @album_id, @artist_id, @genre_id, @duration, @trackNumber);


/*
select t.id, t.TrackNumber, t.Name Track, al.Name Album, al.Year, g.Name Genre, t.Duration
from tracks t
join albums al on al.id = t.Album_Id
join artists ar on ar.id = t.Artist_Id
join genres g on g.id = t.Genre_Id
*/