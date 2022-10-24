$(function () {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});

let subjectList;


function buildSubjectsDropdown() {
    subjectList.forEach(subject => {
        $('.selectpicker').append('<option name="' + subject.SubjectName + '"value=' + subject.Id + '>' + subject.SubjectName + '</option>');
    })
    $('.selectpicker').selectpicker('refresh');

}

function buildGradesDropdown(selected,container) {
    var innerHTML = "";
    selected.forEach(element => {
        console.log(element);
        var subIndex = subjectList.findIndex(subject => subject.Id == element);
        innerHTML += '<label for="subject_result">' + subjectList[subIndex]["SubjectName"] + '</label>' +

            '<select required id=' + subjectList[subIndex]["Id"] + ' class = "subject_result">' +
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

getController("/Subject/GetSubjects").then((response) => {

    subjectList = JSON.parse(response);
    buildSubjectsDropdown();


    $(".selectpicker").on("changed.bs.select", function (e, clickedIndex, newValue, oldValue) {
        var sel = $(this).find('option').eq(clickedIndex).val();
        var selected = $(this).val();
        

        if (selected) {
            buildGradesDropdown(selected,$('.result_input'));
        }
    });
})
 

