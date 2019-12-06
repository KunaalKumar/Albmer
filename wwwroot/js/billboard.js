
/*
 * Gets top billboard albums and their ratings, builds a table row by row with the results. 
 */
function getBillboardResults() {
    $('#billboard-top-album').ready(function () {
        $.ajax({
            url: "/Scraper/ScrapeAlbumChart",
            method: "GET",
        }).done(function (response) {
            for (let i = 0; i < 10; i++) {
                //getLinks(response.albums[i].artist, response.albums[i].title, i);
                $('#billboard-top-album > tbody:last-child').append('<tr><td>' + (i + 1) + '</td><td>' + response.albums[i].title + '</td><td>' + response.albums[i].artist + '</td></tr>');
            }
        })
    });
    
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
        console.log(num + ") " + response.result);
    })
}
