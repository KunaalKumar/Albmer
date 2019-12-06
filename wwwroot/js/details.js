
/*
 * Get details page by given id
 */

class detailsPage {

    constructor(str, detailsSwitch) {

        if (detailsSwitch === "album") {
            this.getAlbumDetails(str);
            console.log("album: id=" + str);
        } else if (detailsSwitch === "artist") {
            this.getArtistDetails(str);
            console.log("artist: name=" + str);
        }
    }

    /* get all details */
    getArtistDetails(name_input) {
        let that = this;
        let tmpName = encodeURIComponent(name_input);
        $.ajax({
            url: "/API/artistDetails",
            method: "GET",
            data: {
                name: tmpName
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
                let listOfArtist = response.result.artists;
                listOfArtist.forEach(function (item) {
                    that.setAlbumArtist(item.name, item.name);
                });

                /* get rate */
                let allmusic_url = response.result.allmusic;
                let allmusic_id = allmusic_url.replace("https://www.allmusic.com/album/", "");
                that.getAllMusicRate(allmusic_id);
                let discogs_url = response.result.discogs;
                let discogs_id = discogs_url.replace("https://www.discogs.com/master/", "");
                that.getDiscogsRate(discogs_id);
                let rym_url = response.result.rate_your_music;
                //let rym_id = rym_url.replace("", "");
                that.getRYMRate(rym_url);
            },
            error: function() {
                console.log('[getAlbumDetails] Error occured');
            }
        });
    }

    /* get rating details */
    getAllMusicRate(id_input) {
        let that = this;
        $.ajax({
            url: "/scraper/allMusicRatings",
            method: "GET",
            data: {
                id: id_input
            },
            success: function (r) {
                console.log("allMusicRatings");
                console.log(r);

                /* all music rate settings */
                that.setAlbumRate_allmusic(r.site_rating, r.user_rating, r.max_rating);

            },
            error: function () {
                console.log('[getAlbumDetails] Error occured');
            }
        });
    }
    getDiscogsRate(id_input) {
        let that = this;
        $.ajax({
            url: "/scraper/discogsRatings",
            method: "GET",
            data: {
                id: id_input
            },
            success: function (r) {
                console.log("discogsRatings");
                console.log(r);

                /* all music rate settings */
                that.setAlbumRate_discogs(r.rating, r.max_rating);

            },
            error: function () {
                console.log('[getAlbumDetails] Error occured');
            }
        });
    }
    getRYMRate(url_input) {
        let that = this;
        $.ajax({
            url: "/scraper/rateYourMusicRatings",
            method: "GET",
            data: {
                url: url_input
            },
            success: function (r) {
                console.log("rateYourMusicRatings");
                console.log(r);

                /* all music rate settings */
                that.setAlbumRate_rym(r.rating, r.max_rating);
            },
            error: function () {
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
        $("#detailsPageAlbum_title").empty();
        $("#detailsPageAlbum_title").append("<i class=\"fas fa-record-vinyl\"></i> " + "Title: " + titleStr);
    }

    setAlbumArtist(artistStr, artistName) {
        $("#detailsPageAlbum_artist").append("<a class=\"badge badge-primary mx-1\" href=/Details/Artist?name=" + encodeURIComponent(artistName) + " target=\"_blank\">" + artistStr + " </a>");
    }


    /*********/
    /* rating */
    /*********/

    /* Artist */


    /* Album */
    setAlbumRate_allmusic(site_rate, user_rate, max_rate) {
        $("#allmusic_rating").text(site_rate + "/" + max_rate);
    }
    setAlbumRate_discogs(site_rate, max_rate) {
        $("#discogs_rating").text(site_rate + "/" + max_rate);
    }
    setAlbumRate_rym(site_rate, max_rate) {
        $("#rym_rating").text(site_rate + "/" + max_rate);
    }
}