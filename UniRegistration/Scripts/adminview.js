getController("/Student/FetchStudentsResults").then((response) => {

    var _studentList = JSON.parse(response);


    for (const i in _studentList) {
        $('table').append('<tr><td>' + _studentList[i]["StudentId"] + '</td><td>' + _studentList[i]["FirstName"] + '</td><td>'
            + _studentList[i]["LastName"] + '</td><td>' +
            _studentList[i]["GuardianName"] + '</td><td>'
            + _studentList[i]["Status"] + '</td><td>' +
            _studentList[i]["TotalMarks"] + '</td><td>Dom</td></tr>');
    }


});
