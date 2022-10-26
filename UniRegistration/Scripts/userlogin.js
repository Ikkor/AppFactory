document.addEventListener("DOMContentLoaded", () => {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});




function logIn() {
   var Email = document.getElementById("email").value;
   var Password = document.getElementById("password").value;
   var RememberMe = document.getElementById("rememberMe").value;

    var authObj = { Email, Password, RememberMe };

    sendController(authObj, "/User/Login").then((response) => {

        if (response.url != null) {
            toastr.success("Redirecting..");
            window.location = response.url;
        }
        else {
            toastr.error('Failed to login');
            return false;
        }
    })
        .catch((error) => {
            toastr.error('Please try again later');
        });

}


