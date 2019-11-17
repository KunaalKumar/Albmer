# Search Artist

Get a list of results matching given query

**URL** : `/API/searchArtist`

**METHOD**: `GET`

**PARAMETERS**<br>
* `name=[string]`
	* Name of artist to query

**RETURN**<br>
* success: bool
* artist: array
	* id: string
	* score: int
	* name: string
	* country: string
	* begin_area: object
		* name: string
	* life_span: object
		* begin: string
		* ended: string
	* tags: array
		* name: string

## Success Response Example
```json
{
  "success": true,
  "artists": [
    {
      "id": "084308bd-1654-436f-ba03-df6697104e19",
      "score": 100,
      "name": "Green Day",
      "country": "US",
      "begin_area": {
        "name": "Berkeley"
      },
      "life_span": {
        "begin": "1989",
        "ended": null
      },
      "tags": [
        {
          "name": "rock"
        },
        {
          "name": "alternative rock"
        },
        {
          "name": "punk"
        }
      ]
    }
  ]
}
```

## Failure Response Example
```json
{
	"success": false,
	"response": "No result found matching query"
}
```

## Notes
* Data will be fetched from MusicBrainz
* [MusicBrainz Example API Call](https://musicbrainz.org/ws/2/artist/?query=green%20day&fmt=json)

