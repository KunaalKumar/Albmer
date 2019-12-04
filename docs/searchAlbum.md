# Search Album
Get a list of album releases matching given query

**URL** : `API/searchAlbum`

**METHOD** : `GET`

**PARAMETERS**<br>
* `name=[string]`
	* Name of albumto query

**RESULT**<br>
* success: bool
* result: object
	* id: string
	* track_count: int
	* title: string
	* artist_credit: array
		* id: string
		* name: string
	* genre: string 

## Success Response Example
```json
{
  "success": true,
  "result": {
    "albums": [
      {
        "id": "37608ff2-3168-3e7c-a77e-04a4b7300c1b",
        "track_count": 13,
        "title": "21st Century Breakdown",
        "artist_credit": [
          {
            "id": "084308bd-1654-436f-ba03-df6697104e19",
            "name": "Green Day"
          }
        ],
        "tags": "rock, punk"
      }
    ]
  }
}
```

## Failure Response Example
```json
{
	"success": false,
	"result": "No result found matching query"
}
```

## Notes
* Data will be fetched from MusicBrainz
* [MusicBrainz Example API Call](http://musicbrainz.org/ws/2/release-group/?query=%2221st%20century%20breakdown%22%20AND%20type:album&fmt=json)
