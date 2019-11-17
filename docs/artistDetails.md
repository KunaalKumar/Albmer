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
	* life_span: object
		* ended: bool
		* begin: string
		* end: string
	* type: string
	* band_members: array
		* name: string
		* id:" string
		* start_year: string
		* end_year: string
	* albums: array
		* id: string
		* title: string
		* release_date: string
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
    "life_span": {
      "ended": false,
      "begin": "1989",
      "end": null
    },
    "type": "Group",
    "band_members": [
      {
        "name": "Mike Dirnt",
        "id": "f332a312-e95b-4413-b6cc-1762a5a6a083",
        "start_year": "1987",
        "end_year": null
      }
    ],
    "albums": [
      {
        "id": "c58228d1-05e9-3ce0-83f6-b0d33ffcaa90",
        "title": "39/Smooth",
        "release_date": "1990"
      }
    ],
    "offical_website": "http://www.greenday.com/",
    "allmusic": "https://www.allmusic.com/artist/mn0000154544",
    "discogs": "https://www.discogs.com/artist/251593",
    "rate_your_music": "http://rateyourmusic.com/artist/green_day"
  }
}
```

## Failure Response Example
```json
{
	"success": false,
	"response": "Artist with given id not found"
}
```

## Notes 
* Data will be fetched from MusicBrainz
* [MusicBrainz Example API Call](https://musicbrainz.org/ws/2/artist/084308bd-1654-436f-ba03-df6697104e19?fmt=json&inc=url-rels+release-groups+artist-rels)
