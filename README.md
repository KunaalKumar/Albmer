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
* [Get AllMusic Album Rating](docs/allMusicRating.md): `GET /AllMusic/getAlbumRating`
* [Get Discogs Album Rating](docs/discogsRating.md): `GET /Discogs/getAlbumRating`
* [Get RateYourMusic Album Rating](docs/rateYourMusicRating.md): `GET /RateYourMusic/getAlbumRating`
* [Get Pitchfork Album Rating](docs/pitchforkRating.md): `GET /Pitchfork/getAlbumRating`
* [Get Metacritic Album Rating](docs/metacriticRating.md): `GET /Metacritic/getAlbumRating`
