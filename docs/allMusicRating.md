# AllMuisc
Get allmusic album ratings from given id

**URL** : `scraper/allMusicRatings`

**METHOD** : `GET`

**Parameters** :
* `id=[string]`
	* Album ratings will be fetched from https://www.allmusic.com/album/[id]

## Success Response Example
```json
{
  "success": true,
  "site_rating": 8,
  "user_rating": 4,
  "max_rating": 10
}
```

## Failure Response Example
```json
{
	"success": false,
	"response": "Given id is invalid"
}
```
