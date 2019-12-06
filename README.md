# Albmer
Albmer will scrape various music related sites for album reviews and ratings. This data will be displayed to users for Billboard top music charts and for individual artists or albums available through custom searches.

## API Endpoints

### Search Related
* [Search artist](docs/searchArtist.md) : `GET /API/searchArtist`
* [Search album](docs/searchAlbum.md) : `GET /API/searchAlbum`

### Details
* [Artist Details](docs/artistDetails.md) : `GET /API/artistDetails`
* [Album Details](docs/albumDetails.md) : `GET /API/albumDetails`

### Scraper Related
> This is what the api will return after scraping the websites.
* [Get AllMusic Album Rating](docs/allMusicRating.md): `GET /scraper/allMusicRatings`
* [Get Discogs Album Rating](docs/discogsRating.md): `GET /scraper/discogsRatings`
* [Get RateYourMusic Album Rating](docs/rateYourMusicRating.md): `GET /scraper/rateYourMuiscRatings`
* [Get Pitchfork Album Rating](docs/pitchforkRating.md): `GET /scraper/pitchforkRatings`
* [Get Metacritic Album Rating](docs/metacriticRating.md): `GET /scraper/metacriticRatings`

### Billboard Album Matcher
> This endpoint will be used to get review urls for albums in the Billboard charts
* [Match Album](docs/matchAlbum.md): `GET /API/matchAlbum`

### Details Page
> This is how user get details page for album and artist
* [Details Page for Artist](docs/detailsArtist.md): `GET /Details/Artist`
* [Details Page for Album](docs/detailsAlbum.md): `GET /Details/Album`
