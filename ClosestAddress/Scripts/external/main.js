

function initAutocomplete() {
    //19.1019014,72.7562848
    var latitude, longitude;
    
    var map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 19.0823998, lng: 72.8111467 },
        zoom: 11,
        mapTypeId: 'roadmap'
    });
    debugger;
    // Create the search box and link it to the UI element.
    var input = document.getElementById('pac-input');
    var searchBox = new google.maps.places.SearchBox(input);

    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener('places_changed', function () {
        var places = searchBox.getPlaces();
        debugger;
        $(".error").hide();
        $('#search-button').attr("disabled", false);
        if (places.length == 0) {
            return;
        }
        if (!places[0].formatted_address.includes("Australia")) {
            $(".error").show();
            $(".error").html("Entered address not present in Australia");
            $(".error").css("color", "red");
            $('#search-button').attr("disabled", true);
        }
        var geocoder = new google.maps.Geocoder();
        var address = places[0].formatted_address;

        geocoder.geocode({ 'address': address }, function (results, status) {

            if (status == google.maps.GeocoderStatus.OK) {
                latitude = results[0].geometry.location.lat();
                longitude = results[0].geometry.location.lng();
            }
        });
    });

    $('#search-button').click(function (event) {
        event.preventDefault();
        latitude = latitude;
        var searchText = $("#pac-input").val();
        if (searchText == "") {
            $(".error").show();
            $(".error").html("please enter the address");
            $(".error").css("color", "red");
        } else {
            $.ajax({
                type: "GET",
                url: "api/SearchAPI/GetSearchResult",
                data: { latitude: latitude, longitude: longitude },
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                beforeSend: function () {
                    $(".loader").css("display", "block");
                },
                success: function (result, status, xhr) {
                    $('#records_table').html('');
                    var trHTML = '';
                    trHTML += '<tr><th>' + 'Address '+ '</th><th>' + 'Distance' + '</th></tr>';

                    $.each(JSON.parse(result), function (i, item) {
                        trHTML += '<tr><td>' + item.Key + '</td><td>' + item.Value + '</td></tr>';
                    });
                    $('#records_table').append(trHTML);

                    $(".loader").css("display", "none");
                    console.log(result);
                },
                error: function (xhr, status, error) {
                    console.log("error occured");
                    $(".loader").css("display", "none");
                   
                },
            });
        }
       
    });
}
