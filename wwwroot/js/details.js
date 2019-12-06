
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

                that.setImage(response.result.image);
                that.setTitle(response.result.name);
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

                that.setImage(response.result.image);
                that.setTitle(response.result.title);
            },
            error: function() {
                console.log('[getAlbumDetails] Error occured');
            }
        });
    }

    /* image */
    setImage(imageUrl) {
        //console.log(imageUrl);
        $("#detailsImage").attr("src", imageUrl);
    }

    /* text */
    setTitle(titleStr) {
        $("#detailsPageTitle").text(titleStr);
    }

    /* rating */

}