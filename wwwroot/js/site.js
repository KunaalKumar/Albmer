// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#search-form').submit(function (e)
{
    e.preventDefault();
    let type = "&search=" + $('#search-type').val();
    let input = "name= " + $('#search').val();
    //$.ajax({
    //    url: "/API/search" + type,
    //    method: "GET",
    //    data: { name: input}
    //}).done(function (response) {
    //    console.log(response);
    //    alert("test");
    //})
    window.location.href = '/Home/SearchResults/?'+input + type;

    //e.preventDefault();
    //$.ajax({
    //    url: "/API/search" + $('#search-type').val(),
    //    data: { name: $('#search').val() },
    //    method: "GET"
    //})
    //    .then(function (response) {
    //        if (!response.success)
    //            return;
    //        if ($('#search-type').val() === "Artist") {
    //            console.log(response.result[0].name);
    //            console.log(response.result[0].tags[0].name);
    //            console.log(response.result[0].begin_area.name);
    //            console.log(response.result[0].life_span.begin);
    //        } else {
    //            console.log(response.result[0].title);
    //            console.log(response.result[0].score);
    //            console.log(response.result[0].artist_credit[0].artist.name);
    //            console.log(response.result[0].tags[0].name);
    //        }
    //    });
});

function navigateToSearchPage() {
    window.location.href = '/Home/SearchResults';
}

/**
 * Function to test api calls via console
 * **/
function testArtistDetails() {
    var id = "084308bd-1654-436f-ba03-df6697104e19";
    $.ajax({
        url: "/API/artistDetails",
        data: { id: id },
        method: "GET"
    })
        .then(function (response) {
            if (!response.success)
                return;
            console.log(response.result)
        });
}