
    $(function () {
        let form = document.querySelector('form');
        form.addEventListener('submit', (e) => {
        e.preventDefault();
    return false;
        });
    });


    function logIn() {
        var Email = $("#email").val(); 
        var Password = $("#password").val();
        var RememberMe = $("#rememberMe").val();
   
    var authObj = {Email,Password,RememberMe };

        sendController(authObj, "/User/Login").then((response) => {
            
            if (!response.error) {

        toastr.success("Authentication Succeed. Redirecting to relevent page.....");
                //window.location = response.url;
            }
    else {
        toastr.error('Unable to Authenticate user, ' + response.error);
    return false;
            }
        })
            .catch((error) => {
        toastr.error('Unable to make request!!');
            });

    }


