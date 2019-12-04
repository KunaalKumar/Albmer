# Search Artist

Get a list of results matching given query

**URL** : `/API/searchArtist`

**METHOD**: `GET`

**PARAMETERS**<br>
* `name=[string]`
	* Name of artist to query

**RETURN**<br>
* success: bool
* result: array 
	* id: string
	* name: string
	* origin: string
	* begin_year: string
	* end_year: string
	* genre: string

## Success Response Example
```json
{
  "success": true,
  "result": [
      {
        "id": "084308bd-1654-436f-ba03-df6697104e19",
        "name": "Green Day",
        "origin": "Berkeley",
		"begin_year": "1982",
		"end_year": null,
		"genre": "punk, pop, rock, reggae"
      }
    ]
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
* [MusicBrainz Example API Call](https://musicbrainz.org/ws/2/artist/?query=green%20day&fmt=json)

