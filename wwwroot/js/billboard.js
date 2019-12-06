
/*
 * Gets top billboard albums and their ratings, builds a table row by row with the results. 
 */
function getBillboardResults() {
    $('#billboard-top-album').ready(function () {
        $.ajax({
            url: "/Scraper/ScrapeAlbumChart",
            method: "GET",
        }).done(function (response) {
            for (let i = 0; i < 50; i++) {
                //getLinks(response.albums[i].artist, response.albums[i].title, i);
                let art = response.albums[i].artist;
                let tit = response.albums[i].title;
                //getLinks(response.albums[i].artist, response.albums[i].title);
                //$('#billboard-top-album > tbody:last-child').append('<tr><td>'+(i+1)+'</td><td>'+tit+'</td><td>'+art+'</td><td><button onclick="getReviews(\''+art+'\',\''+tit+'\')"> Get Reviews </button></td></tr>');
                //$('#billboard-top-album > tbody:last-child').append('<tr><td>' + (i + 1) + '</td><td>' + tit + '</td><td>' + art + '</td><td><button id="'+tit+'" class="'+art+'"onclick="getReviews(this.class, this.id)"> Get Reviews </button></td></tr>');
                $('#billboard-top-album > tbody:last-child').append('<tr><td>' + (i + 1) + '</td><td>' + tit + '</td><td>' + art + '</td><td><button id="row_' + i + '" class="' + art + '" data-title="' + tit + '"onClick="searchAlbum(this)"> Search Album </button></td></tr>');
            }
        })
    });
    
}

function searchAlbum(e) {
    let type = "&search=" + "Album";
    let input = "name= " + e.getAttribute("data-title");

    window.location.href = '/Home/SearchResults/?' + input + type;
}

function getLinks(artist, album, num) {
    $.ajax({
        url: "/API/matchAlbum",
        method: "GET",
        data: {
            artistName: artist,
            albumName: album
        }
    }).done(function (response) {
        console.log(num + ") " + response.success);
    })
}
