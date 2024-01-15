////document.addEventListener('keydown', function () {
////    if (event.keyCode == 123) {
////        alert("This function has been disabled to prevent you from stealing my code!");
////        return false;
////    } else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) {
////        alert("This function has been disabled to prevent you from stealing my code!");
////        return false;
////    } else if (event.ctrlKey && event.keyCode == 85) {
////        alert("This function has been disabled to prevent you from stealing my code!");
////        return false;
////    }
////}, false);

////if (document.addEventListener) {
////    document.addEventListener('contextmenu', function (e) {
////        alert("This function has been disabled to prevent you from stealing my code!");
////        e.preventDefault();
////    }, false);
////} else {
////    document.attachEvent('oncontextmenu', function () {
////        alert("This function has been disabled to prevent you from stealing my code!");
////        window.event.returnValue = false;
////    });
////}
function SetMenu() {
    $('#navbar-top').html('');
    $.ajax({
        type: "GET",
        url: "../Home/CreateDynamicMenu",
        context: document.body,
        success: function (data) {
            $('#navbar-top').append(data);
        },
        error: function (xhr) {
            $('#navbar-top').append('');

            $.ajax({
                type: "GET",
                url: "../../Home/CreateDynamicMenu",
                context: document.body,
                success: function (data) {
                    $('#navbar-top').append(data);
                },
                error: function (xhr) {
                    $('#navbar-top').append('');
                }
            });


        }
    });
}

$(document).ready(function () {
    SetMenu();
});


