# Search Album
Get a list of album releases matching given query

**URL** : `API/searchAlbum`

**METHOD** : `GET`

**PARAMETERS** : 
* `name=[string]`
	* Name of album to query

## Success Response Example
```json
{
  "success": true,
  "albums": [
    {
      "id": "37608ff2-3168-3e7c-a77e-04a4b7300c1b",
      "score": 100,
      "count": 13,
      "title": "21st Century Breakdown",
      "artist-credit": [
        {
          "artist": {
            "id": "084308bd-1654-436f-ba03-df6697104e19",
            "name": "Green Day"
          }
        }
      ],
      "tags": [
        {
          "count": 1,
          "name": "rock"
        },
        {
          "count": 2,
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
* Data will be gotten from MusicBrainz
* [MusicBrainz Example API Call](http://musicbrainz.org/ws/2/release-group/?query=%2221st%20century%20breakdown%22%20AND%20type:album&fmt=json)
