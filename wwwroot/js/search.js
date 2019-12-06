//$('#search-results').ready(function () {
//    $.ajax({
//        url: "/API/search" + $('#type').text(),
//        method: "GET",
//        data: { name: $('#name').text()}
//    }).done(function (response) {
//        console.log(response);
//        console.log(response.result[0]);
//        console.log(response.result[0].length);
//        if ($('#type').text() === 'Artist') {
//            for (let i = 0; i < response.result[0].length; i++) {
//                $('#results > tbody:last-child').append('<tr><td><a href=#>' + response.result[0][i].name + '</a></td><tr>');
//            }
//        } else {
//            for (let i = 0; i < response.result.length; i++) {
//                $('#results > tbody:last-child').append('<tr><td><a href=#>' + response.result[i].title + '</a></td> <td><a href=#>' + response.result[i].artists[0].name + '</a></td><tr>');
//            }
//        }
//    })
//});

function getSearchResults() {
    $('#search-results').ready(function () {
        $.ajax({
            url: "/API/search" + $('#type').text(),
            method: "GET",
            data: { name: $('#name').text() }
        }).done(function (response) {
            console.log(response);
            console.log(response.result[0]);
            console.log(response.result[0].length);
            if (!response.success) {
                $('#search-results > tbody:last-child').append('<tr><td>Sorry, no search results for given search fields</td><tr>');
            } else {
                if ($('#type').text() === 'Artist') {
                    for (let i = 0; i < response.result[0].length; i++) {
                        let artistRef = "/Details/Artist/?name=" + response.result[0][i].id;
                        $('#search-results > tbody:last-child').append('<tr><td><a href=' + artistRef + '>' + response.result[0][i].name + '</a></td><tr>');
                    }
                } else {
                    for (let i = 0; i < response.result.length; i++) {
                        let albumRef = "/Details/Album/?id=" + response.result[i].id;
                        $('#search-results > tbody:last-child').append('<tr><td><a href=' + albumRef + '>' + response.result[i].title + '</a></td> <td>' + response.result[i].artist_credit[0].name + '<tr>');
                    }
                }
            }
            //getDetails(response.result[17].id);

        })
    });
}

function getDetails(mbid) {
   
    $.ajax({
        url: "/API/albumDetails",
        method: "GET",
        data: { id: mbid }
    }).done(function (response) {
        console.log(response);
        
    })
    
}