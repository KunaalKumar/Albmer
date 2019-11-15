# Album Details
Get album details based on given id

**URL** : `API/albumDetails`

**METHOD** : `GET`

**PARAMETERS** :
* `id=[string]`
	* Album id to query

## Success Response Example
```json
{
  "success": true,
  "title": "Kerplunk!",
  "release-date": "1992-01-07",
  "artist-id": "084308bd-1654-436f-ba03-df6697104e19",
  "tracks": [
    {
      "number": 1,
      "id": "1aa427e7-6ebe-31bd-90f8-6b544f484926",
      "length": 144440,
      "title": "2000 Light Years Away"
    },
    {
      "number": 2,
      "id": "3138e9eb-78b1-4dd0-80a4-bf6fb6a7e9a8",
      "length": 150000,
      "title": "One for the Razorbacks"
    }
  ]
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
* [MusicBrainz Example API Call](https://musicbrainz.org/ws/2/release/fd3c6333-9e3e-4360-aff7-05c0512e8b38?fmt=json&inc=recordings+artists)
