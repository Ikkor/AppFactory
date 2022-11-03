$(function () {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});




getController("/Subject/GetSubjects").then((response) => {
    var subjectList = JSON.parse(response);
    buildSubjectsDropdown(subjectList);
    $(".selectpicker").on("changed.bs.select", function (e, clickedIndex, newValue, oldValue) {
        var sel = $(this).find('option').eq(clickedIndex).val();
        var selected = $(this).val();


        if (selected) {
            buildGradesDropdown(selected, $('.result_input'), subjectList);
        }
    });
})


getController("/User/CurrentEmailSession").then((response) => {
    $('#Email').val(response.email);
    });




function buildSubjectsDropdown(subjectList) {
    subjectList.forEach(subject => {
        $('.selectpicker').append('<option name="' + subject.SubjectName + '"value=' + subject.SubjectId + '>' + subject.SubjectName + '</option>');
    })
    $('.selectpicker').selectpicker('refresh');
}

function buildGradesDropdown(selected, container, subjectList) {
    var innerHTML = "";
    selected.forEach(element => {
        console.log(element);
        var subIndex = subjectList.findIndex(subject => subject.SubjectId == element);
        innerHTML += '<label for="result_select">' + subjectList[subIndex]["SubjectName"] + '</label>' +

            '<select required id=' + subjectList[subIndex]["SubjectId"] + ' class = "result_select">' +
            '<option value="" selected disabled hidden>Grade</option>' +
            '<option value="10">A[10]</option>' +
            '<option value="8">B[8]</option>' +
            '<option value="6">C[6]</option>' +
            '<option value="4">D[4]</option>' +
            '<option value="2">E[2]</option>' +
            '<option value="0">F[0]</option>' +
            '</select>'
    });
    container.html(innerHTML);
}



function register() {

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



    sendController(studentObj, "/Student/Register").then((response) => {

        if (response.url!=null) {

            toastr.success("Registration Succeed. Redirecting to relevent page.....");
            window.location = response.url;

        }
        else {
            toastr.error("An error occured"+response.error);
            return false;
        }
    })
        .catch((error) => {
            toastr.error('Unable to make request!!');
        });



}


function buildResultJSON(result_list) {
    jsonObj = [];

    for (const i in result_list) {
        var resultId = i;
        console.log(i);
        var marks = result_list[resultId];
        item = {}
        item["SubjectId"] = resultId;
        item["Marks"] = marks;
        jsonObj.push(item);
        

    }
    return jsonObj;
}




