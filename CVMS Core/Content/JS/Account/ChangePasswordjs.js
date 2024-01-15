
$('#btnSendCode').on('click', function () {
    $('#pMessage').html('');
    debugger
    var email = $('#email').val();
    if (email != '') {
        if (isValidEmail(email)) {
            $.ajax({
                type: "POST",
                url: "../Account/SendChangePasswordCode",
                data: JSON.stringify({ 'email': email }),
                contentType: "application/json; charset=utf-8",
                context: document.body,
                success: function (data) {
                    debugger
                    if (data == 'success') {
                        $('#pMessage').removeClass('text-danger');
                        //$('#pMessage').addClass('text-success');
                        $('#pMessage').html('Link sent to your email address.');
                        $('#email').val('');
                    }
                    else
                        $('#pMessage').html(data);
                },
                error: function (xhr) {
                    debugger
                    $('#pMessage').html('Some error occured.');
                }
            });
        }
        else
            $('#pMessage').html('Email is not valid format');
    }
    else {
        $('#pMessage').html('Email is required');
    }

});

