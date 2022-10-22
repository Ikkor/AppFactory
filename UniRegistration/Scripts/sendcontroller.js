function sendController(dataObject, url) {
    return new Promise((resolve, reject) => {
        $.ajax({
            type: "POST",
            url: url,
            data: dataObject,
            dataType: "json",
            success: function (data) {
                resolve(data)
            },
            error: function (error) {
                reject(error)
            }
        })
    });
}