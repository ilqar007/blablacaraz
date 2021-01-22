// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function SetFieldAutocompletePlacesApi(element) {
    var url = '/Ride/GetEventVenuesList';
    element.autocomplete({
        source: function (request, response) {
            $.ajax({
                url: url,
                data: { SearchText: request.term },
                dataType: "json",
                type: "GET",
                success: function (data) {
                    if (data.length == 0) {
                        // $('#EventVenueId').val("");
                        //   $('#VenueLocationMesssage').show();
                        return false;
                    }
                    else {
                        response($.map(data, function (item) {
                            return {
                                label: item.addressCombined,
                                value: item.locationId
                            }
                        }));
                    }
                },
                error: function (x, y, z) {
                    alert('error');
                }
            });
        },

        select: function (event, ui) {
            element.val(ui.item.label);
            // $('#EventVenueId').val(ui.item.value);
            return false;
        }
    }).autocomplete("widget").addClass("CitiesAutocomplete");
}