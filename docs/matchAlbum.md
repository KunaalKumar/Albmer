# Match Album
Get review websites' urls based on given album and artist names

**URL** : `API/matchAlbum`

**METHOD** : `GET`

**PARAMETERS** :
* `artistName=[string]`
	* Artist name to query
* `albumName=[string]`
	* Album name to query

**RESULT** :
* success: bool
* title: string
* artist_name: string
* allmusic: string
* discogs: string
* rate_your_music: string

## Success Response Example
```json
{
  "success": true,
  "title": "American Idiot",
  "artist_name": "Green Day",
  "allMusic": "https://www.allmusic.com/album/mw0001987250",
  "discogs": "https://www.discogs.com/master/406622",
  "rate_your_music": null
}
```

## Failure Response Example
```json
{
	"success": false,
	"response": "Failed to find a match"
}
```

## Notes 
* The API/searchAlbum and API/albumDetails endpoints are utilized in this endpoint 
