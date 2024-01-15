$(document).ready(function () {
   
    getCity();
    getRoom();
    
    //GetEditHotel();
})



function getCity() {
    
    $.ajax({
        type: 'GET',
        url: "../HotelManagement/GetCityList",
        data: {},
        dataType: 'json',
        context: document.body,
        success: function (data) {
            


            $.each(data.city, function (item, value) {
                
                $("#city").append($("<option></option>").val(value.cid).html(value.cityname));
            });

        },
        error: function (error) {

            //Handle errors here
            alert("Not Found");
        }
    });
}

//-------------------------------------------------------------------------------------------
$("#city").change(function () {
    GetEditHotel();


});

function GetEditHotel() {
    
    $("#hotel").empty();
    $("#hotel").append($('<option>Select</option>'));

    var HotelSelector = $("#city").val();
    
    $.ajax({
        type: 'GET',
        url: "../HotelManagement/BindHotel",
        data: { cid: HotelSelector },
        dataType: "JSON",
        context: document.body,
        success: function (data) {
            
           
            $.each(data.hotelbinding, function (item, value) {
                
                $("#hotel").append($("<option></option>").val(value.hotelid).html(value.hotelname));
            });

        },

        error: function (error) {

            alert("Not Found");
        }
    });
}
//----------------------------------------------------------------------------------------------------

function getRoom() {
    
    $.ajax({
        type: 'GET',
        url: "../HotelManagement/GetRoomtype",
        data: {},
        dataType: 'json',
        context: document.body,
        success: function (data) {

            

            $.each(data.room, function (item, value) {
                
                $("#roomPreference").append($("<option></option>").val(value.roomid).html(value.roomtype));
            });

        },
        error: function (error) {

            //Handle errors here
            alert("Not Found");
        }
    });
}


$("#submit").click(function () {
    var FName = $("#firstName").val();
    var LName = $("#lastName").val();
    var aadharnumber = $("#adharNumber").val();
    var Address = $("#address").val();
    var City = $("#city").val();
    var Hotels = $("#hotel").val();
    var phonenumber = $("#phone").val();
    var Emailid = $("#email").val();
    var RoomPreference = $("#roomPreference").val();
    var NumberofAdults = $("#adults").val();
    var Payment = $("#payment").val();
    var checkin = $("#checkin").val();
    var checkout = $("#checkout").val();
    var userid = $("#Hiddenid").val();
    var aadharPattern = /^[0-9]{12}$/;
    var phonePattern = /^[0-9]{10}$/; // Assuming 10-digit phone number
    

    function isValidInput(input) {
        return /^[A-Z][a-z]*$/.test(input);
    }
    function isValidName(input) {
        return /^[A-Z][a-z]* [A-Z][a-z]*$/.test(input);
    }



    if (!isValidInput(FName)) {
        $('#firstName').addClass('border-danger');
        $('#fname-error').text('Name should start with a capital letter and only contain alphabets.');
        isValid = false;
    } else {
        $('#firstName').removeClass('border-danger');
    }

    if (!isValidInput(LName)) {
        $('#lastName').addClass('border-danger');
        $('#lname-error').text('LastName should start with a capital letter and only contain alphabets.');
        isValid = false;
    } else {
        $('#lastName').removeClass('border-danger');
    }

    if (!aadharPattern.test(aadharnumber) || (aadharnumber == '')) {
        isValid = false;
        $('#adharNumber').addClass('border-danger');
        $('#aadhar-error').text('Enter your 12 digits aadhar no.');

    }
    else {
        $('#adharNumber').removeClass('border-danger');
    }

    if (Address == '') {
        isValid = false;
        $('#address').addClass('border-danger');
    } else {
        $('#address').removeClass('border-danger');
    }

    if (City == 'Select') {
        isValid = false;
        $('#city').addClass('border-danger');
        $('#city-error').text('Please select a city.');

    } else {
        $('#city').removeClass('border-danger');
    }

    if (Hotels == 'Select') {
        isValid = false;
        $('#hotel').addClass('border-danger');
        $('#hotel-error').text('Please select a Hotel.');

    } else {
        $('#hotel').removeClass('border-danger');
    }

    if (!phonePattern.test(phonenumber) || (phonenumber == '')) {
        $('#phone').addClass('border-danger');
        $('#phone-error').text('please fill the correct Phone number.');

        isValid = false;
    }
    else {
        $('#phone').removeClass('border-danger');
    }

    var DocemailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!Emailid || !DocemailRegex.test(Emailid) || (Emailid.match(/@/g) || []).length !== 1) {
        $("#Email").addClass("border-danger");
        $('#email-error').text('please fill the correct Emailid.');
    }

    if (RoomPreference == 'Select') {
        isValid = false;
        $('#roomPreference').addClass('border-danger');

    } else {
        $('#roomPreference').removeClass('border-danger');
    }

    if (NumberofAdults == '') {
        isValid = false;
        $('#adults').addClass('border-danger');
    } else {
        $('#adults').removeClass('border-danger');
    }
    if (Payment == '') {
        isValid = false;
        $('#payment').addClass('border-danger');
    } else {
        $('#payment').removeClass('border-danger');
    }

    if (checkin == '') {
        isValid = false;
        $('#checkin').addClass('border-danger');
    }
    else {
        $('#checkin').removeClass('border-danger');
    }

    if (checkout == '') {
        isValid = false;
        $('#checkout').addClass('border-danger');
    }
    else {
        $('#checkout').removeClass('border-danger');
    }
    
    var formData = new FormData();
    formData.append("FirstName", FName);
    formData.append("LastName", LName);
    formData.append("AadharNumber", aadharnumber);
    formData.append("Address", Address);
    formData.append("City", City);
    formData.append("Hotels", Hotels);
    formData.append("phonenumber", phonenumber);
    formData.append("Emailid", Emailid);
    formData.append("RoomPreference", RoomPreference);
    formData.append("NumberofAdults", NumberofAdults);
    formData.append("Payment", Payment);
    formData.append("Checkin", checkin);
    formData.append("checkout", checkout);
    formData.append("Userid", userid);

  

    $.ajax({
        type: "POST",
        url: "/HotelManagement/Savecustomerhotelbooking",
        data: formData,
        contentType: false, 
        processData: false,
        context: document.body,
        
        success: function (data) {
            alert("Your Reservation is Successfull");

        },
        error: function (error) {

            console.log(error);
        }
    });
});

