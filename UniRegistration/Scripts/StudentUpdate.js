
getController("/Student/GetStudent").then((student) => {
    var student = JSON.parse(student);

    $("#FirstName").val(student.FirstName);
    $("#LastName").val(student.LastName);
    $("#NationalIdentity").val(student.NationalIdentity);
    $("#DateOfBirth").val(student.DateOfBirth);
    $("#GuardianName").val(student.GuardianName);
    $("#Phone").val(student.Phone);
    $("#Address").val(student.Address);

})



function update() {

    var FirstName = $("#FirstName").val();
    var LastName = $("#LastName").val();
    var NationalIdentity = $("#NationalIdentity").val();
    var DateOfBirth = new Date($("#DateOfBirth").val()).toISOString();
    var GuardianName = $("#GuardianName").val();
    var Phone = $("#Phone").val();
    var Address = $("#Address").val();
    var _resultvalues = {};

    $(".result_select").each(function () {
        _resultvalues[this.id] = this.value;
    });

    var Results = buildResultJSON(_resultvalues);

    var studentObj = { FirstName, LastName, NationalIdentity, DateOfBirth, Address, GuardianName, Phone, Results };



    sendController(studentObj, "/Student/Update").then((response) => {

        if (response.url != null) {

            toastr.success("Registration Succeed. Redirecting to relevent page.....");
            window.location = response.url;

        }
        else {
            toastr.error("An error occured");
            return false;
        }
    })
        .catch((error) => {
            toastr.error('Unable to make request!!');
        });



}