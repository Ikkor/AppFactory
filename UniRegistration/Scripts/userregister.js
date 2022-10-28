$(function () {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});






function register() {

    var Email = document.getElementById("email").value;
    var Password = document.getElementById("password").value;
    var _confirmPassword = document.getElementById("confirmPassword").value;

    if (Password!=_confirmPassword) {
        toastr.error('Password does not match.');
        return false;
    }
    


    var authObj = {Email,Password };

    sendController(authObj, "/User/Register").then((response) => {

        if (!response.error) {

            toastr.success("Registration Succeed. Redirecting to relevent page.....");
            window.location = response.url;
        }
        else {
            toastr.error('Unable to register user, ' + response.error);
            return false;
        }
    })
        .catch((error) => {
            toastr.error('Unable to make request!!');
        });




}




