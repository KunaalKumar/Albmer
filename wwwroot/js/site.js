// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#search-form').submit(function (e)
{
    e.preventDefault();
    $.ajax({
        url: "/API/search" + $('#search-type').val(),
        data: { name: $('#search').val() },
        method: "GET"
    })
        .then(function (result) {
            if (!result.success)
                return;
            if ($('#search-type').val() === "Artist") {
                console.log(result.result[0].name);
                console.log(result.result[0].tags[0].name);
                console.log(result.result[0].begin_area.name);
                console.log(result.result[0].life_span.begin);
            } else {
                console.log(result.result[0].title);
                console.log(result.result[0].score);
                console.log(result.result[0].artist_credit[0].artist.name);
                console.log(result.result[0].tags[0].name);
            }
        });
});
