getController("/Student/FetchStudentsResults").then((response) => {

    var _studentList = JSON.parse(response);


    for (const i in _studentList) {
        $('table').append('<tr><td>' + _studentList[i]["StudentId"] + '</td><td>' 
            + _studentList[i]["FirstName"] + '</td><td>'
            + _studentList[i]["LastName"] + '</td><td>' 
            + _studentList[i]["GuardianName"] + '</td><td>'
            + _studentList[i]["TotalMarks"] + '</td><td>' 
            + _studentList[i]["EnrollmentStatusName"] + '</td><td>'
            + '<button href = "" >Edit ' + _studentList[i]["StudentId"] + '</button></td ></tr > ');
    }


});

function DownloadCSV() {

    var csv_data = [];

    var rows = document.getElementsByTagName('tr');
    for (var i = 0; i < rows.length; i++) {

        var cols = rows[i].querySelectorAll('td,th');

        var csvrow = [];
        for (var j = 0; j < cols.length; j++) {

            csvrow.push(cols[j].innerHTML);
        }

        csv_data.push(csvrow.join(","));
    }

    csv_data = csv_data.join('\n');

    downloadCSVFile(csv_data);

}

function downloadCSVFile(csv_data) {

    CSVFile = new Blob([csv_data], {
        type: "text/csv"
    });

    var temp_link = document.createElement('a');

    temp_link.download = "StudentList.csv";
    var url = window.URL.createObjectURL(CSVFile);
    temp_link.href = url;

    temp_link.style.display = "none";
    document.body.appendChild(temp_link);

    temp_link.click();
    document.body.removeChild(temp_link);
}