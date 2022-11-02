getController("/Student/GetEnrollmentStatus").then((response) => {

    var _status = JSON.parse(response.status).toString();
    $('#status').html('- Enrollment status: ' + _status);
    if (_status != 'Approved') { 
        $('#update').append('<a href="/Student/Update">- Edit my enrollment information </a>');
}
});
