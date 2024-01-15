function matchPassword() {

    if ($('#Password').val() != '' && $('#ConfirmPassword').val() != '') {
        if ($('#Password').val() != $('#ConfirmPassword').val())
            // alert('Password and Confirm Password doesnot match');
            $("#spanMessage").addClass('text-danger').html('Password and Confirm Password not match.');
        else {
            var resultPassword = '';
            resultPassword = CheckPasswordFormat($('#Password').val())
            $('#spanWaringMessage').html(resultPassword);
            $('#warningModal').modal('show');
            $("#spanMessage").html('');
            $("#Sbtid").prop('disabled', false);
        }
    }
    else {
        $("#spanMessage").html('');
    }
    return false;
}