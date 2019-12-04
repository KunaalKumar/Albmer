# Artist Details
Get artist details based on given id

**URL** : `API/artistDetails`

**METHOD** : `GET`

**PARAMETERS**<br>
* `id=[string]`
	* Artist id to query

**RESULT**<br>
* success: bool
* result: object
	* name: string
	* image: string
	* begin_year: bool
	* end_year: string
	* type: string
	* origin: string
	* albums: array
		* id: string
		* title: string
	* official_website: string
	* allmusic: string
	* discogs: string
	* rate_your_music: string

## Success Response Example
```json
{
  "success": true,
  "result": {
    "name": "Green Day",
    "image": "https://commons.wikimedia.org/wiki/File:Greenday2010.jpg",
    "begin_year": null,
    "end_year": null,
    "origin": null,
    "type": "Group",
    "albums": [
      {
        "albumId": "c58228d1-05e9-3ce0-83f6-b0d33ffcaa90",
        "title": "39/Smooth"
      },
      {
        "albumId": "a0603694-2422-3a40-b946-d0bcea5e8254",
        "title": "Kerplunk!"
      }
    ],
    "official_website": "http://www.greenday.com/",
    "allmusic": "https://www.allmusic.com/artist/mn0000154544",
    "discogs": "https://www.discogs.com/artist/251593",
    "rate_your_music": "http://rateyourmusic.com/artist/green_day"
  }
```

## Failure Response Example
```json
{
	"success": false,
	"result": "Artist with given id not found"
}
```

## Notes 
* Data will be fetched from MusicBrainz
* [MusicBrainz Example API Call](https://musicbrainz.org/ws/2/artist/084308bd-1654-436f-ba03-df6697104e19?fmt=json&inc=url-rels+release-groups+artist-rels)
