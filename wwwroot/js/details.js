
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

    /* get all details */
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

                /* artist settings */
                //that.setArtistImage(response.result.image);
                that.setArtistTitle(response.result.name);
                let listOfAlbum = response.result.albums;
                listOfAlbum.forEach(function (item, index) {
                    that.setArtistAlbum(index, item.title, item.albumId);
                });
                

                /* get info */
                that.setArtistInfo_begin_year(response.result.begin_year);
                that.setArtistInfo_end_year(response.result.end_year);
                that.setArtistInfo_origin(response.result.origin);
                that.setArtistInfo_type(response.result.type);
                that.setArtistInfo_official_website(response.result.official_website);
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
                let listOfArtist_noDuplicate = Array.from(new Set(listOfArtist.map(a => a.name)))
                    .map(name => {
                        return listOfArtist.find(a => a.name === name)
                    })
                console.log(listOfArtist_noDuplicate);
                listOfArtist_noDuplicate.forEach(function (item) {
                    that.setAlbumArtist(item.name, item.id);
                });

                /* get rate */
                let allmusic_url = response.result.allmusic;
                if (allmusic_url != null) {
                    let allmusic_id = allmusic_url.replace("https://www.allmusic.com/album/", "");
                    that.getAllMusicRate(allmusic_id);
                } else {
                    $("#allmusic_rating").text("Unavailable");
                }
                let discogs_url = response.result.discogs;
                if (discogs_url != null) {
                    let discogs_id = discogs_url.replace("https://www.discogs.com/master/", "");
                    that.getDiscogsRate(discogs_id);
                } else {
                    $("#discogs_rating").text("Unavailable");
                }

                let rym_url = response.result.rate_your_music;
                if (rym_url != null) {
                    //let rym_id = rym_url.replace("", "");
                    that.getRYMRate(rym_url);
                } else {
                    $("#rym_rating").text("Unavailable");
                }
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
                if (!r.success) {
                    $("#allmusic_rating").text("Unavailable");

                } else {

                    /* all music rate settings */
                    that.setAlbumRate_allmusic(r.site_rating, r.user_rating, r.max_rating);
                }

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
        if (imageUrl != null) {
            $("#detailsPageArtist_image").attr("src", imageUrl);
        }
    }

    /* Album */
    setAlbumImage(imageUrl) {
        //console.log(imageUrl);
        if (imageUrl != null) {
            $("#detailsPageAlbum_image").attr("src", imageUrl);
        }
    }


    /*********/
    /* main */
    /*********/

    /* Artist */
    setArtistTitle(titleStr) {
        $("#detailsPageArtist_title").empty();
        $("#detailsPageArtist_title").append("<i class=\"fas fa-user\"></i> " + titleStr);
    }
    setArtistAlbum(count, name, id) {
        $(detailsPageArtist_playlist_content).append("<tr><th scope=\"row\">" + (count+1) + "</th><td><a href=\"/Details/Album/?id=" + id + "\">" + name + "</a></td></tr>")
    }

    /* Album */
    setAlbumTitle(titleStr) {
        $("#detailsPageAlbum_title").empty();
        $("#detailsPageAlbum_title").append("<i class=\"fas fa-record-vinyl\"></i> " + "Title: " + titleStr);
    }

    setAlbumArtist(artistStr, artistId) {
        $("#detailsPageAlbum_artist").append("<a class=\"badge badge-primary mx-1\" href=/Details/Artist?id=" + artistId + " target=\"_blank\">" + artistStr + " </a>");
    }



    /*********/
    /* others */
    /*********/

    /* Artist */
    setArtistInfo_begin_year(year) {
        if (year != null) {
            $("#begin_year").text(year);
        } else {
            $("#begin_year").text("Unavailable");
        }
    }
    setArtistInfo_end_year(year) {
        if (year != null) {
            $("#end_year").text(year);
        } else {
            $("#end_year").text("Unavailable");
        }
    }
    setArtistInfo_origin(origin) {
        if (origin != null) {
            $("#origin").text(origin);
        } else {
            $("#origin").text("Unavailable");
        }
    }
    setArtistInfo_type(type) {
        if (type != null) {
            $("#type").text(type);
        } else {
            $("#type").text("Unavailable");
        }
    }
    setArtistInfo_official_website(url) {
        if (url != null) {
            $("#official_website").empty();
            $("#official_website").append("<a href=\"" + url + "\" target=\"_blank\">" + url + "</a>");
        } else {
            $("#official_website").text("Unavailable");
        }
    }


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