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
            alert("Result = " + result);
        });
});
