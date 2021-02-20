// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function SetFieldAutocompletePlacesApi(element, submit) {
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
                                label: item.label,
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
            if (submit)
                element.closest("form").submit();
            return false;
                
        }
    }).autocomplete("widget").addClass("CitiesAutocomplete");
}


    function RefreshMessageCount() {
            var url = '/Message/GetNewMessageCount';
            $.ajax({
        url: url,
                dataType: "json",
                type: "GET",
                success: function (data) {
                    if (data.messageCount > 0) {
                        $('#unreadMessagesCountMessage').html('<span class="text-right"><i class="fa fa-bell"></i>' + data.messageCount + '</span>');
                    }
                    else {
                        $('#unreadMessagesCountMessage').html('');
                    }
                    if (data.bookRequestCount > 0) {
                        $('#unconfirmedBooksCount').html('<span class="text-right"><i class="fa fa-bell"></i>' + data.bookRequestCount + '</span>');
                    }
                    else {
                        $('#unconfirmedBooksCount').html('');
                    }
                    if ((data.messageCount + data.bookRequestCount) > 0) {
                        $('#notificationsCountTitle').html('<span class="text-right"><i class="fa fa-bell"></i>' + (data.messageCount + data.bookRequestCount) + '</span>');
                    }
                    else {
                        $('#notificationsCountTitle').html('');
                    }
                },
                error: function (x, y, z) {
                }
            });
        }

        setInterval(function () {
            if ($('#isLoggedIn').val()=="1") {
                RefreshMessageCount();
            }
        }, 5000);
$("#selectLanguage select").change(function () {
    $(this).parent().validate().settings.ignore = "*";
    $(this).parent().submit();
});

$('#picture-button').on('click', function () {
        $('#file-input').trigger('click');
        });

        $("#file-input").change(function () {
            //readURL(this);
            var formData = new FormData();
            formData.append('thefile', this.files[0]);
            $.ajax(
                {
        url: "/Profile/UploadFile",
                    data: formData,
                    processData: false,
                    contentType: false,
                    type: "POST",
                    success: function (data) {
        $('form').submit();
                    }
                }
            );
        });

    