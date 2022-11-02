$(function () {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});





function logIn() {
   var Email = document.getElementById("email").value;
   var Password = document.getElementById("password").value;

    var authObj = { Email, Password };

    sendController(authObj, "/User/Login").then((response) => {

        if (response.url != null) {
            toastr.success("Redirecting..");
            window.location = response.url;
        }
        else {
            toastr.error('Failed to login' + response.error);
            return false;
        }
    })
        .catch((error) => {
            toastr.error('Please try again later');
        });

}


