
/*
 * Get details page by given id
 */

class detailsPage {

    constructor(id, detailsSwitch) {

        if (detailsSwitch === "album") {
            this.getAlbumDetails(id);
            console.log("album: id=" + id);
        } else if (detailsSwitch === "artist") {
            this.getArtistDetails(id);
            console.log("artist: id=" + id);
        }
    }

    getArtistDetails(id_input) {
        let that = this;
        $.ajax({
            url: "/API/artistDetails",
            method: "GET",
            data: {
                id: id_input
            },
            success: function(response) {
                console.log("getArtistDetails");
                console.log(response);

                that.setArtistImage(response.result.image);
                that.setArtistTitle(response.result.name);
            },
            error: function() {
                console.log('[getArtistDetails] Error occured');
            }
        });
    }

    getAlbumDetails(id_input) {
        let that = this;
        $.ajax({
            url: "/API/albumDetails",
            method: "GET",
            data: {
                id: id_input
            },
            success: function(response) {
                console.log("getArtistDetails");
                console.log(response);

                /* album settings */
                that.setAlbumImage(response.result.image);
                that.setAlbumTitle(response.result.title);
                that.setAlbumArtist(response.result.artists[0].name, response.result.artists[0].id);
            },
            error: function() {
                console.log('[getAlbumDetails] Error occured');
            }
        });
    }

    /*********/
    /* image */
    /*********/

    /* Artist */
    setArtistImage(imageUrl) {
        //console.log(imageUrl);
        $("#detailsImage").attr("src", imageUrl);
    }

    /* Album */
    setAlbumImage(imageUrl) {
        //console.log(imageUrl);
        $("#detailsPageAlbum_image").attr("src", imageUrl);
    }


    /*********/
    /* text */
    /*********/

    /* Artist */
    setArtistTitle(titleStr) {
        $("#detailsPageTitle").text(titleStr);
    }

    /* Album */
    setAlbumTitle(titleStr) {
        $("#detailsPageAlbum_title").text("Title: " + titleStr);
    }

    setAlbumArtist(artistStr, artistId) {
        $("#detailsPageAlbum_artist a").text("Artist: " + artistStr);
        $("#detailsPageAlbum_artist a").attr("href", "/Details/Artist?id=" + artistId);
    }


    /*********/
    /* rating */
    /*********/

    /* Artist */


    /* Album */
}