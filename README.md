# Albmer
Albmer will scrape various music related sites for album reviews and ratings. This data will be displayed to users for Billboard top music charts and for individual artists or albums available through custom searches.

## API Endpoints

### Search Related
* [Search artist](docs/searchArtist) : `GET /API/searchArtist/`
* [Search album](docs/searchAlbum) : `GET /API/searchAlbum/`

### Scraper Related
* [Get AllMusic Album Rating](docs/allMusicRating): `GET /AllMusic/getAlbumRating`
* [Get Discogs Album Rating](docs/discogsRating): `GET /Discogs/getAlbumRating`
* [Get RateYourMusic Album Rating](docs/rateYourMusicRating): `GET /RateYourMusic/getAlbumRating`
* [Get Pitchfork Album Rating](docs/pitchforkRating): `GET /Pitchfork/getAlbumRating`
* [Get Metacritic Album Rating](docs/metacriticRating): `GET /Metacritic/getAlbumRating`
