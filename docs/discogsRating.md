# Discogs
Get discogs album ratings from given id

**URL** : `scraper/discogsRatings`

**METHOD** : `GET`

**Parameters** :
* `id=[string]`
        * Album ratings will be fetched from https://www.discogs.com/master/[id]

## Success Response Example
```json
{
  "success": true,
  "rating": 4.22,
  "max_rating": 5,
  "number_of_ratings": 671 
}
```

## Failure Response Example
```json
{
        "success": false,
        "response": "Given id is invalid"
}
```
