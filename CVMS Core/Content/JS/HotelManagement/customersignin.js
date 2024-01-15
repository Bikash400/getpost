$(document).ready(function () {
});

$("#signbtn").click(function () {
    
    var CustName = $("#name").val();
    var CustAddress = $("#address").val();
    var CustEmail = $("#email").val();
    var CustPhone = $("#phone").val();
    var CustPassword = $("#password").val();
    var CustConfirmPassword = $("#confirm-password").val();
    
    var formData = new FormData();
    formData.append("Name", CustName);
    formData.append("Address", CustAddress);
    formData.append("Email", CustEmail);
    formData.append("Phone", CustPhone);
    formData.append("Password", CustPassword);
    formData.append("ConfirmPassword", CustConfirmPassword);

    $.ajax({
        type: "POST",
        url: "/HotelManagement/Savecustomerdetails",
        data: formData,
        contentType: false,
        processData: false,
        context: document.body,
        success: function (data) {
            
            alert("Successful");
        },
        error: function (error) {

            console.log(error);
        }
    });
});

$("#loginButton").click(function () {
    debugger
    var isValid = true;
    var Name = $("#name").val();
    var Address = $("#address").val();
    var Email = $("#email").val();
    var Phone = $("#phone").val();
    var Password = $("#password").val();
    var ConfirmPassword = $("#confirm-password").val();
    var phonePattern = /^[0-9]{10}$/; // Assuming 10-digit phone number

    
    function isValidInput(input) {
        return /^[A-Z][a-z]*$/.test(input);
    }
    function isValidName(input) {
        return /^[A-Z][a-z]* [A-Z][a-z]*$/.test(input);
    }

    if (!isValidInput(Name)) {
        isValid = false;
        $('#name').addClass('border-danger');
        $('#fname-error').text('Name should start with a capital letter and only contain alphabets.');
        
    } else {
        $('#name').removeClass('border-danger');
    }

    if (Address == '') {
        isValid = false;
        $('#address').addClass('border-danger');
    } else {
        $('#address').removeClass('border-danger');
    }

    var DocemailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!Email || !DocemailRegex.test(Email) || (Email.match(/@/g) || []).length !== 1) {
        isValid = false;
        $("#email").addClass("border-danger");
        $('#email-error').text('please fill the correct Emailid.');
    }

    if (!phonePattern.test(Phone) || (Phone == '')) {
        isValid = false;
        $('#phone').addClass('border-danger');
        $('#phone-error').text('please fill the correct Phone number.');

        
    }
    else {
        $('#phone').removeClass('border-danger');
    }

    if (Password == '') {
        isValid = false;
        $('#password').addClass('border-danger');
    } else {
        $('#password').removeClass('border-danger');
    }

    if (ConfirmPassword == '') {
        isValid = false;
        $('#confirm-password').addClass('border-danger');
    } else {
        $('#confirm-password').removeClass('border-danger');
    }

    if (!isValid) {
        alert('Form is Not valid. Please fill out all Details!!!!');
        return;
    }

    var formData = new FormData();
    
    formData.append("Name", Name);
    formData.append("Address", Address);
    formData.append("Email", Email);
    formData.append("Phone", Phone);
    formData.append("Password", Password);
    formData.append("ConfirmPassword", ConfirmPassword);
    
    $.ajax({
        type: "POST",
        url: "/HotelManagement/Savecustomerdetails",
        data: formData,
        contentType: false,
        processData: false,
        context: document.body,
        success: function (data) {
            
            alert("Successful");

        },
        error: function (error) {

            console.log(error);
        }
    });
});