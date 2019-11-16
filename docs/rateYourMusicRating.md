# RateYourMuisc
Get RateYourMuisc album ratings from given partial

**URL** : `scraper/rateYourMusicRatings`

**METHOD** : `GET`

**Parameters** :
* `partial=[string]`
        * Album ratings will be fetched from https://rateyourmusic.com/release/album/[partial]

## Success Response Example
```json
{
  "success": true,
  "rating": 3.14,
  "max-rating": 5,
  "number-of-ratings": 3051
}
```

## Failure Response Example
```json
{
        "success": false,
        "response": "Given id is invalid"
}
```
