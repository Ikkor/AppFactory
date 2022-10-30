getController("/Student/GetEnrollmentStatus").then((response) => {

    var _status = JSON.parse(response.status).toString();
    $('#status').html('- Enrollment status: '+_status);
});
