﻿getController("/Student/GetStatus").then((response) => {

    var _status = JSON.parse(response.status).toString();
    $('h2').html(_status);
});