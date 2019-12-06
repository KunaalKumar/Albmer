
/*
 * Gets top billboard albums and their ratings, builds a table row by row with the results. 
 */

window.onload = function(id, detailsSwitch) {

    this.getArtistDetails();
    //this.getAlbumDetails();

}

function getArtistDetails() {
    $.ajax({
        url: "/API/artistDetails",
        method: "GET",
        data: {
            id: "084308bd-1654-436f-ba03-df6697104e19"
        },
        success: function(response) {
            console.log("getArtistDetails");
            console.log(response);

            setImage(response.result.image);
            setTitle(response.result.name);
        },
        error: function() {
            console.log('[getArtistDetails] Error occured');
        }
    });
}

function getAlbumDetails() {
    $.ajax({
        url: "API/albumDetails",
        method: "GET",
        data: {
            id: "a0603694-2422-3a40-b946-d0bcea5e8254"
        }
    }).done(function(response) {
        console.log("getAlbumDetails");
        console.log(response);
        
        setImage(response.result.image);
        setTitle(response.result.title);
    })
}

/* image */
function setImage(imageUrl) {
    //console.log(imageUrl);
    $("#detailsImage").attr("src", imageUrl);
}

/* text */
function setTitle(titleStr) {
    $("#detailsPageTitle").text(titleStr);
}

/* rating */


