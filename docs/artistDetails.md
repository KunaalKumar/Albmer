# Artist Details
Get artist details based on given id

**URL** : `API/artistDetails`

**METHOD** : `GET`

**PARAMETERS** :
* `id=[string]`
	* Artist id to query

## Success Response Example
```json
{
  "success": true,
  "name": "Green Day",
  "life-span": {
    "ended": false,
    "start-year": "1989",
    "end-year": null
  },
  "type": "Group",
  "band-members": [
    {
      "name": "Mike Dirnt",
      "id": "f332a312-e95b-4413-b6cc-1762a5a6a083",
      "start-year": "1987",
      "end-year": null
    },
    {
      "name": "Tré Cool",
      "id": "0dcee02c-5d2c-4f5c-9d60-d58a4df32d9e",
      "start-year": "1990",
      "end-year": null
    }
  ],
  "albums": [
    {
      "id": "c58228d1-05e9-3ce0-83f6-b0d33ffcaa90",
      "title": "39/Smooth"
    },
    {
      "id": "a0603694-2422-3a40-b946-d0bcea5e8254",
      "title": "Kerplunk!"
    }
  ],
  "urls": [
    {
      "type": "allmusic",
      "url": "https://www.allmusic.com/artist/mn0000154544"
    },
    {
      "type": "discogs",
      "url": "https://www.discogs.com/artist/251593"
    }
  ]
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
* [MusicBrainz Example API Call](https://musicbrainz.org/ws/2/artist/084308bd-1654-436f-ba03-df6697104e19?fmt=json&inc=url-rels%20release-groups%20artist-rels)
