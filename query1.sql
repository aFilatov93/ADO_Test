select t.id, t.TrackNumber, t.Name Track, al.Name Album, al.Year, g.Name Genre, t.Duration
from tracks t
join albums al on al.id = t.Album_Id
join artists ar on ar.id = t.Artist_Id
join genres g on g.id = t.Genre_Id