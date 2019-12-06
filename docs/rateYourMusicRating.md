# RateYourMuisc
Get RateYourMuisc album ratings from given partial

**URL** : `scraper/rateYourMusicRatings`

**METHOD** : `GET`

**Parameters** :
* `url=[string]`
        * Album ratings will be fetched from the given url

## Success Response Example
```json
{
  "success": true,
  "rating": 3.14,
  "max_rating": 5,
  "number_of_ratings": 3051
}
```

## Failure Response Example
```json
{
        "success": false,
        "response": "Given id is invalid"
}
```
