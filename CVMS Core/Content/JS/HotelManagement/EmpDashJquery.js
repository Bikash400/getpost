$(document).ready(function () {

});

$('#loginBtn').click(function () {
    
    var Name = $('#employeeName').val(); // Correct the id to match the input id
    var Password = $('#password').val();

    $.ajax({
        type: "GET",
        url: "/HotelManagement/EmployeeDashLogin",
        data: { Name: Name, Password: Password },
        dataType: 'json',
        context: document.body,
        success: function (data) {
            
            if (data.success) {
                
                $('#loginForm').hide();
                $('#dashboard').show();
               
                $('#shiftTiming').text(data.shiftTiming);
                $('#EmpName').text(data.employeeName);
               
            } else {
                $('#errorMessage').text(data.message);
            }
        }

    });
});