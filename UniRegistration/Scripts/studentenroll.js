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

    var FirstName = document.getElementById("fname").value;
    var LastName = document.getElementById("lname").value;
    var NID = document.getElementById("NID").value;
    var DoB = document.getElementById("date").value;
    var GuardianName = document.getElementById("guardian").value;
    var Phone = document.getElementById("phone").value;
    var Address = document.getElementById("address").value;
    var _resultvalues = {};
   
    $(".result_select").each(function () {
        _resultvalues[this.id] = this.value;
    });

    var Results = buildResultJSON(_resultvalues);

    var studentObj = { FirstName, LastName, NID, DoB, Address, GuardianName, Phone, Results };



    sendController(studentObj, "/Student/Register").then((response) => {

        if (!response.error) {

            toastr.success("Registration Succeed. Redirecting to relevent page.....");
            window.location = response.url;

        }
        else {
            toastr.error(response.error);
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




