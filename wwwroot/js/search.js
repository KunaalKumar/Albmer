$('#results').ready(function () {
    $.ajax({
        url: "/API/search" + $('#type').text(),
        method: "GET",
        data: { name: $('#name').text()}
    }).done(function (response) {
        console.log(response);
        console.log(response.result[0]);
        console.log(response.result[0].length);
        if ($('#type').text() === 'Artist') {
            for (let i = 0; i < response.result[0].length; i++) {
                $('#results > tbody:last-child').append('<tr><td><a href=#>' + response.result[0][i].name + '</a></td><tr>');
            }
        } else {
            for (let i = 0; i < response.result.length; i++) {
                $('#results > tbody:last-child').append('<tr><td><a href=#>' + response.result[i].title + '</a></td> <td><a href=#>' + response.result[i].artists[0].name + '</a></td><tr>');
            }
        }
    })
});