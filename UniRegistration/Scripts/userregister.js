$(function () {
    let form = document.querySelector('form');
    form.addEventListener('submit', (e) => {
        e.preventDefault();
        return false;
    });
});






function register() {

    var Email = $("#email").val();
    var Password = $("#password").val();
    var ConfirmPassword = $("#confirmPassword").val();

    if (Password!=ConfirmPassword) {
        toastr.error('Password does not match.');
        return false;
    }
    


    var authObj = {Email,Password };

    sendController(authObj, "/User/Register").then((response) => {

        if (response.url!=null) {

            toastr.success(
                'Registration success.',
                'Redirecting',
                {
                    timeOut: 20,
                    fadeOut: 10,
                    onHidden: function () {
                        window.location = response.url;
                    }
                }
            );
            
        }
        else {
            toastr.error('Unable to register ');
            return false;
        }
    })
        .catch((error) => {
            toastr.error('Unable to make request!');
        });




}




