// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#search-form').submit(function (e)
{
    e.preventDefault();
    let type = "&search=" + $('#search-type').val();
    let input = "name= " + $('#search').val();

    window.location.href = '/Home/SearchResults/?'+input + type;
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