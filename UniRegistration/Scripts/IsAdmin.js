getController("/Admin/IsAdmin").then((response) => {

    if (response.IsAdmin=="True") {
        $('body').prepend('<a  class="navItem" style="float:right" href="/Admin/Index"> Admin </a>')
    }
    

})
