# Search Artist

Get a list of results matching given query

**URL** : `/API/searchArtist`

**METHOD**: `GET`

**PARAMETERS**: `name=[string]`

## Success Response Example
```json
{
  "success": true,
  "artists": [
    {
      "name": "Green Day",
      "country": "US",
      "begin-area": {
        "type": "City",
        "name": "Berkeley"
      },
      "life-span": {
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

