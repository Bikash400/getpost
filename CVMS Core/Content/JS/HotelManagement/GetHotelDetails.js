
$(document).ready(function () {
    
    alert("ready!");
});
debugger
var HotelImages = "";

$('#hotelimages').on('change', function () {
    debugger
    var file = document.querySelector('input[type=file]')['files'][0];
    var fileReader = new FileReader();

    fileReader.onload = function () {
        debugger
        var data = fileReader.result;
        baseString = fileReader.result;
        HotelImages = baseString;
    };
    fileReader.readAsDataURL($('#hotelimages').prop('files')[0]);

});

getHotelDetail();
$("#submit").click(function () {
    debugger
    var HName = $("#hotelname").val();
    var HAddress = $("#hoteladdress").val();
    var HPhone = $("#hotelphone").val();
    var HEmail = $("#hotelemail").val();
    var Hnoofrooms = $("#numberOfRooms").val();
    //var file = $("#file")[0].files[0];
    var fileInput1 = HotelImages;
    var phonePattern = /^[0-9]{10}$/; // Assuming 10-digit phone number
    var phonePattern = /^[0-9]{10}$/; // Assuming 10-digit phone number
    var isValid = true;

  
   
    function isValidInput(input) {
        return /^[A-Z][a-z]*$/.test(input);
    }
    function isValidName(input) {
        return /^[A-Z][a-z]* [A-Z][a-z]*$/.test(input);
    }



    if (!isValidInput(HName)) {
        $('#hotelname').addClass('border-danger');
        $('#name-error').text('Name should start with a capital letter and only contain alphabets.');
        isValid = false;
    } else {
        $('#hotelname').removeClass('border-danger');
    }

    if (HAddress == '') {
        isValid = false;
        $('#hoteladdress').addClass('border-danger');
    } else {
        $('#hoteladdress').removeClass('border-danger');
    }

    if (!phonePattern.test(HPhone) || (HPhone == '')) {
        isValid = false;
        $('#hotelphone').addClass('border-danger');
        $('#phone-error').text('Enter your 10 digits mobile no.');

    }
    else {
        $('#hotelphone').removeClass('border-danger');
    }

    var DocemailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!HEmail || !DocemailRegex.test(HEmail) || (HEmail.match(/@/g) || []).length !== 1) {
        $("#hotelemail").addClass("border-danger");
        $('#email-error').text('Please Fill the correct Emailid.');

    }


    if (Hnoofrooms == '') {
        isValid = false;
        $('#numberOfRooms').addClass('border-danger');
    } else {
        $('#numberOfRooms').removeClass('border-danger');
    } 

    if (!isValid) {
        alert('Form is Not valid. Please fill out all Details!!!!');
        return;
    }
    debugger
    var formData = new FormData();
    if (fileInput1) {
    formData.append("HotelName", HName);
    formData.append("HotelAddress", HAddress);
    formData.append("HotelPhoneno", HPhone);
    formData.append("HotelEmailid", HEmail);
    formData.append("Numberofrooms", Hnoofrooms);
    formData.append("hotelImages", fileInput1);
    
    

        //formData.append("file", file);

        $.ajax({
            type: "POST",
            url: "/HotelManagement/Savehoteldetails",
            data: formData,
            contentType: false,
            processData: false,
            context: document.body,
            success: function (data) {
                debugger
                alert("Successful");
                window.location.reload();


            },
            error: function (error) {

                console.log(error);
            }

        });
    }
    });

    

function getHotelDetail() {
    
    $.ajax({
        type: 'GET',
        url: "/HotelManagement/GetHotelDetails",
        data: {},
        dataType: 'json',
        context: document.body,
        success: function (data) {

            var row = "";
            var rowcount = 1;
            //  console.log(employees);

            
            $.each(data.employees, function (item, value) {


                row += "<tr>";
                row += "<td>" + rowcount + "</td>";
                row += "<td>" + value.hotelName + "</td>";
                row += "<td>" + value.hotelAddress + "</td>";
                row += "<td>" + value.hotelPhoneno + "</td>";
                row += "<td>" + value.hotelEmailid + "</td>";
                row += "<td>" + value.numberofrooms + "</td>";



                row += "<td>" +
                    " <button type='button' class='btn btn-sm btn-success' data-id='' onclick='EditHotel(" + value.hotelid + ", event)' ><i class='fa fa-pencil-square-o' aria-hidden='true'></i></button>" +
                    "<button type='button' class='btn btn-sm btn-danger' data-id='' onclick='DeleteHotelById(" + value.hotelid + ", event)'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                    " <button type='button' class='btn btn-sm btn-info' data-id='' onclick='ViewMaster(" + value.file+ ", event)' ><i class='fa fa-eye' aria-hidden='true'></i></button>" +

                    "</td>";

                row += "</tr>";
                //   $("#hiddenId").val(value.operatorId);
                rowcount += 1;
            });


            $("#hotel").append(row);
            //operatorId = $("#hiddenId").val();

        },
        error: function (error) {

            // Handle errors here
            alert("Not Found");
        }
    });
}

function DeleteHotelById(control, e) {

    var Id = control;


    $.ajax({
        type: "POST",
        url: "../HotelManagement/DeleteHotel/" + Id,
        //data: (id: Id ), //use id here
        //dataType: "json",
        // contentType: "application/json; charset=utf-8",
        //dataType: "JSON",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
            alert("Your record is Deleted Successfully")
            //getoperator();
            window.location.reload();

        },
        error: function (error) {


            alert('Some error occured.');
        }
    });

}

function EditHotel(Hotelid, e) {
    $.ajax({
        type: "GET",
        url: "../HotelManagement/EditHotels",
        data: { Hotelid: Hotelid },
        dataType: "json",
        context: document.body,
        success: function (data) {
            
            $("#Edithotelname").val(data.list[0].hotelName);
            $("#Edithoteladdress").val(data.list[0].hotelAddress);
            $("#Edithotelphone").val(data.list[0].hotelPhoneno);
            $("#Edithotelemail").val(data.list[0].hotelEmailid);
            $("#Edithotelroom").val(data.list[0].numberofrooms);
            $("#hiddenId").val(data.list[0].hotelid)
            $("#Editedmaster-toggle").modal('show');

        },
        error: function (error) {


            alert('Some error occured.');
        }
    });

}

$('#BtnUpdate').on('click', function () {

    var ehotelName = $('#Edithotelname').val();
    var ehotelAddress = $('#Edithoteladdress').val();
    var ehotelPhone = $('#Edithotelphone').val();
    var ehotelEmail = $('#Edithotelemail').val();
    var ehotelRoom = $('#Edithotelroom').val();
    var ehotelId = $('#hiddenId').val();
    var phonePattern = /^[0-9]{10}$/; // Assuming 10-digit phone number
    var emailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var isValid = true;


    function isValidInput(input) {
        return /^[A-Z][a-z]*$/.test(input);
    }
    function isValidName(input) {
        return /^[A-Z][a-z]* [A-Z][a-z]*$/.test(input);
    }

    //-----------------------------------------------------------------------------------------------------------------------

    if (!isValidInput(ehotelName)) {
        $('#Edithotelname').addClass('border-danger');
        $('#editname-error').text('Name should start with a capital letter and only contain alphabets.');
        isValid = false;
    } else {
        $('#Edithotelname').removeClass('border-danger');
    }

    if (ehotelAddress == '') {
        isValid = false;
        $('#Edithoteladdress').addClass('border-danger');
    } else {
        $('#Edithoteladdress').removeClass('border-danger');
    }

    if (!phonePattern.test(ehotelPhone) || (ehotelPhone == '')) {
        $('#Edithotelphone').addClass('border-danger');
        $('#editphone-error').text('please fill the correct Addres.');

        isValid = false;
    }
    else {
        $('#Edithotelphone').removeClass('border-danger');
    }

    var DocemailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!ehotelEmail || !DocemailRegex.test(ehotelEmail) || (ehotelEmail.match(/@/g) || []).length !== 1) {
        $("#Edithotelemail").addClass("border-danger");
        $('#editemail-error').text('Please Fill the correct Emailid.');

    }


    if (ehotelRoom == '') {
        isValid = false;
        $('#Edithotelroom').addClass('border-danger');
    } else {
        $('#Edithotelroom').removeClass('border-danger');
    } 
    if (!isValid) {
        alert('Form is Not valid. Please fill out all Details!!!!');
        return;
    }


    var formData = new FormData();
    formData.append('HotelName', ehotelName);
    formData.append('HotelAddress', ehotelAddress);
    formData.append('HotelPhoneno', ehotelPhone);
    formData.append('HotelEmailid', ehotelEmail);
    formData.append('Numberofrooms', ehotelRoom);
    formData.append('Hotelid', ehotelId);

    $.ajax({
        type: "POST",
        url: "../HotelManagement/UpdateHotel",
        data: formData,
        contentType: false,
        processData: false,

        context: document.body,
        success: function (data) {

            alert('data has been stored to database')

            window.location.reload();

            getUserDetail();
        }
    });
});
//function gethotelimage(control, e) {
//    debugger
//    // ClearSubmitForm()
//    e.preventDefault();
//    //Id = cid;

//    var Hotelid = control;
//    // $('#btnupdate').prop('disabled', false);
//    debugger
//    if (Hotelid > 0) {
//        $.ajax({
//            type: "GET",
//            url: "../HotelManagement/Hotelimage/" + hotelid,
//            dataType: "jSON",
//            contentType: "application/json; charset=utf-8",
//            success: function (data) {
//                debugger
//                $("#imageModal").modal('show');
//                var imgurl = data.image[0].file;
//                $("#file").attr("src", imgurl);
//                $("#file").show();

//            },
//            error: function (xhr) {

//                debugger;
//                alert('Some error occured.');
//            }
//        });
//    }
//    else {
//        alert('Some error occured. Please try again.');
//    }
//};

function ViewMaster(file, event) {

    console.log("file Path2:", file);

    $("#imageModal").find("img").attr("src", file);
    $("#imageModal").modal("show");
}

$('#search').on('keyup', function () {
    var searchTerm = $(this).val().toLowerCase();
    $('#userTbl tbody tr').each(function () {
        var lineStr = $(this).text().toLowerCase();
        if (lineStr.indexOf(searchTerm) === -1) {
            $(this).hide();
        } else {
            $(this).show();
        }
    });
});
