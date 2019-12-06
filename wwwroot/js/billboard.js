
/*
 * Gets top billboard albums and their ratings, builds a table row by row with the results. 
 */
function getBillboardResults() {
    $('#billboard-top-album').ready(function () {
        $.ajax({
            url: "/Scraper/ScrapeAlbumChart",
            method: "GET",
        }).done(function (response) {
            for (let i = 0; i < 100; i++) {
                let art = response.albums[i].artist;
                let tit = response.albums[i].title;
                //getLinks(response.albums[i].artist, response.albums[i].title);
                //$('#billboard-top-album > tbody:last-child').append('<tr><td>'+(i+1)+'</td><td>'+tit+'</td><td>'+art+'</td><td><button onclick="getReviews(\''+art+'\',\''+tit+'\')"> Get Reviews </button></td></tr>');
                //$('#billboard-top-album > tbody:last-child').append('<tr><td>' + (i + 1) + '</td><td>' + tit + '</td><td>' + art + '</td><td><button id="'+tit+'" class="'+art+'"onclick="getReviews(this.class, this.id)"> Get Reviews </button></td></tr>');
                $('#billboard-top-album > tbody:last-child').append('<tr><td>' + (i + 1) + '</td><td>' + tit + '</td><td>' + art + '</td><td><button id="row_' + i + '" class="' + art + '" data-title="'+tit+'"onClick="getReviews(this)"> Get Reviews </button></td></tr>');


            }
        })
    });
    
}



function getLinks(artist, album) {
    $.ajax({
        url: "/API/searchAlbum",
        method: "GET",
        data: {
            //artistName: artist,
            name: album
        }
    }).done(function (response) {
        console.log(response);
    })
}

function getReviews(e) {
    let art = e.className;
    let tit = e.getAttribute("data-title");
    let row = e.id;

    console.log(art + tit);
    $.ajax({
        url: "/API/searchAlbum",
        method: "GET",
        data: {
            name: art
        }
    }).done(function (response) {
        for (let i = 0; i < response.result.length; i++) {
            if ( response.result[i].artist_credit[0].name == art) {
                getAlbumDetails(response.result[i].id, tit, row);
                break;
            }
            if (i = response.result.length) {
                unavailable(row);
            }
        }
    })
}

function getAlbumDetails(mbid,tit, row) {
    $.ajax({
        url: "/API/albumDetails",
        method: "GET",
        data: { id: mbid }
    }).done(function (response) {
        if (!response.success) {
            unavailable();
        }
        console.log(response);
        if (response.result.allmusic) {
            allMusicRating(response.result.allmusic);
        }
    })
}

function unavailable(row) {
    console.log('unavailable');
    $("#"+row).html("Unavailable");
}

function allMusicRating(_url) {
    $.ajax({
        url: "/Scraper/AllMusicRatings",
        method: "GET",
        data: {
            url: _url
        }
    }).done(function (response) {
        console.log(response);
    })
}