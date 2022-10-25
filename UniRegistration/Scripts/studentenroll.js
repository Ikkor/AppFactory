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


function buildSubjectsDropdown(subjectList) {
    subjectList.forEach(subject => {
        $('.selectpicker').append('<option name="' + subject.SubjectName + '"value=' + subject.Id + '>' + subject.SubjectName + '</option>');
    })
    $('.selectpicker').selectpicker('refresh');

}

function buildGradesDropdown(selected, container, subjectList) {
    var innerHTML = "";
    selected.forEach(element => {
        console.log(element);
        var subIndex = subjectList.findIndex(subject => subject.Id == element);
        innerHTML += '<label for="result_select">' + subjectList[subIndex]["SubjectName"] + '</label>' +

            '<select required id=' + subjectList[subIndex]["Id"] + ' class = "result_select">' +
            '<option value="" selected disabled hidden>Grade</option>' +
            '<option value="A">A</option>' +
            '<option value="B">B</option>' +
            '<option value="C">C</option>' +
            '<option value="D">D</option>' +
            '<option value="E">E</option>' +
            '<option value="E">F</option>' +
            '</select>'

    });
    
    container.html(innerHTML);
}



function register() {

    var FirstName = $("#fname").val();
    var LastName = $("#lname").val();
    var NID = $('#NID').val();
    var DoB = $('#date').val();
    var GuardianName = $('#guardian').val();
    var Phone = $('#phone').val();
    var Address = $('#address').val();
    var _resultvalues = {};

    $(".result_select").each(function () {
        result_values[this.id] = this.value;
    });
    var resultObj = buildResultJSON(_resultvalues);

    var studentObj = {FirstName,LastName,NID,DoB,Address,GuardianName,Phone};

    sendController(studentObj, "/Student/Register").then((response) => {

        if (!response.error) {

            toastr.success("Registration Succeed. Redirecting to relevent page.....");
        }
        else {
            toastr.error('Please provide the correct information, ');
            return false;
        }
    })
        .catch((error) => {
            toastr.error('Unable to make request!!');
        });



}


function buildResultJSON(result_list) {
    jsonObj = [];


    for (var resultid = 0; i < result_list.length; i++) {
        var id = i;
        var marks = result_list[resultid];
        item = {}
        item["SubjectId"] = id;
        item["Marks"] = marks;
        
        jsonObj.push(item);

    }
    return JSON.stringify(jsonObj);
}




