$(document).ready(function () {
    $('#eventFilter').change(function () {
        var eventId = $(this).val();
        if (eventId) {
            $.ajax({
                url: '/DashBoard/GetVolunteersByEvent', // Use a relative URL instead of Razor syntax
                type: 'GET',
                data: { eventId: eventId },
                success: function (data) {
                    var volunteerTableBody = $('#volunteerTable tbody');
                    volunteerTableBody.empty();
                    $.each(data, function (index, volunteer) {
                        var row = '<tr>' +
                            '<td>' + volunteer.name + '</td>' +
                            '<td>' + volunteer.city + '</td>' +
                            '<td>' + volunteer.phoneNumber + '</td>' +
                            '<td>' + new Date(volunteer.joinedAt).toLocaleDateString() + '</td>' +
                            '<td>' + volunteer.email + '</td>' +
                            '</tr>';
                        volunteerTableBody.append(row);
                    });
                }
            });
        } else {
            $('#volunteerTable tbody').empty();
            // You need to fetch the initial data again or store it in a variable
            var initialVolunteers = @Html.Raw(Json.Serialize(Model));
            $.each(initialVolunteers, function (index, volunteer) {
                var row = '<tr>' +
                    '<td>' + volunteer.Name + '</td>' +
                    '<td>' + volunteer.City + '</td>' +
                    '<td>' + volunteer.PhoneNumber + '</td>' +
                    '<td>' + volunteer.Email + '</td>' +
                    '</tr>';
                $('#volunteerTable tbody').append(row);
            });
        }
    });
});
