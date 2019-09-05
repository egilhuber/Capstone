// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.









//function initMap() {
//    // The location of Uluru
//    var milwaukee = { lat: 43.0389, lng: -87.9064 };

//    // The map, centered at Uluru
//    var map = new google.maps.Map(
//        document.getElementById('map'), { zoom: 8, center: milwaukee });
//    // The marker, positioned at Uluru
//    var marker = new google.maps.Marker({ position: milwaukee, map: map });
//}








function searchAddress() {
    var addressInput = document.getElementById('location').value;
    var geocoder = new google.maps.Geocoder();
    geocoder.geocode({ address: location }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            var myResult = results[0].geometry.location;
            createMarker(myResult);
            map.setCenter(myResult);
            mapsetZoom(10);
        }
        else {
            alert("geocode unsuccessful because " + status);
        }
    })
}

function createMarker(latlng) {
    if (marker != undefined && marker != '') {
        marker.setMap(null);
        marker = '';
    }

    marker = new google.maps.Marker({
        map: map,
        position: latlng
    })
}