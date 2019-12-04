# Album Details
Get album details based on given id

**URL** : `API/albumDetails`

**METHOD** : `GET`

**PARAMETERS** :
* `id=[string]`
	* Album id to query

**RESULT** :
* success: bool
* result: object
	* title: string
	* id: string
	* artist: object
		* id: string
		* name: string
	* track_count: int
	* allmusic: string
	* discogs: string
	* rate_your_music: string

## Success Response Example
```json
{
  "success": true,
  "result": {
    "title": "Kerplunk!",
    "image": null,
    "id": "a0603694-2422-3a40-b946-d0bcea5e8254",
    "artists": [
      {
        "artistId": "084308bd-1654-436f-ba03-df6697104e19",
        "name": "Green Day"
      }
    ],
    "track_count": 0,
    "allmusic": "https://www.allmusic.com/album/mw0000096356",
    "discogs": "https://www.discogs.com/master/33172",
    "rate_your_music": "https://rateyourmusic.com/release/album/green_day/kerplunk/"
  }
}
```

## Failure Response Example
```json
{
	"success": false,
	"response": "Album with given id not found"
}
```

## Notes 
* Data will be fetched from MusicBrainz
* The release will be fetched first and then the release group will be searched to get the album urls
* [MusicBrainz Example API Call to get the release](https://musicbrainz.org/ws/2/release/fd3c6333-9e3e-4360-aff7-05c0512e8b38?fmt=json&inc=release-groups%20recordings%20artists)
* [MusicBrainz Example API Call to get release-group](https://musicbrainz.org/ws/2/release-group/a0603694-2422-3a40-b946-d0bcea5e8254?fmt=json&inc=url-rels)
