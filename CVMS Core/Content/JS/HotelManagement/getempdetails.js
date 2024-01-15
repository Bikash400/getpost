$(document).ready(function () {
    alert('Welcome')
    getEmpDetail();
});

$("#submit").click(function () {
    var isValid = true;

    var Name = $("#empname").val();
    var Address = $("#empaddress").val();
    var Email = $("#empemail").val();
    var Phone = $("#empphone").val();
    var ShiftFrom = $("#EmpShiftFrom").val();
    var ShiftTo = $("#EmpShiftTo").val();
    var Password = $("#Emppassword").val();
    var phonePattern = /^[0-9]{10}$/; // Assuming 10-digit phone number
    //var DocemailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    //var atCount = (emailId.match(/@/g) || []).length;
    var phoneRegex = /^[0-9]{10}$/;

    function isValidInput(input) {
        return /^[A-Z][a-z]*$/.test(input);
    }
    function isValidName(input) {
        return /^[A-Z][a-z]* [A-Z][a-z]*$/.test(input);
    }
    
    //**************************************all condition for validation***************************************

    if (!isValidInput(Name)) {
        $('#empname').addClass('border-danger');
        $('#name-error').text('Name should start with a capital letter and only contain alphabets.');
        isValid = false;
    } else {
        $('#empname').removeClass('border-danger');
    }

    if (Address == '') {
        isValid = false;
        $('#empaddress').addClass('border-danger');
    } else {
        $('#empaddress').removeClass('border-danger');
    }

    

    //if (!Email || !DocemailRegex.test(Email) || (Email.match(/@/g) || []).length !== 1) {
    //    $("#empemail").addClass("border-danger");
    //    $('#name-error').text('Please Fill the correct Emailid.');

    //}

    if (!phonePattern.test(Phone) || (Phone == '')) {
        isValid = false;
        $('#empphone').addClass('border-danger');
        $('#phone-error').text('Enter your 10 digits mobile no.');

    }
    else {
        $('#empphone').removeClass('border-danger');
    }


    if (ShiftFrom == '') {
        $('#EmpShiftFrom').addClass('border-danger');
        isValid = false;
    }
    else {
        $('#EmpShiftFrom').removeClass('border-danger');
    }

    if (ShiftTo == '') {
        $('#EmpShiftTo').addClass('border-danger');
        isValid = false;
    }
    else {
        $('#EmpShiftTo').removeClass('border-danger');
    }

    if (Password == '') {
        $('#Emppassword').addClass('border-danger');
        isValid = false;
    }
    else {
        $('#Emppassword').removeClass('border-danger');
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
    formData.append("shiftfrom", ShiftFrom);
    formData.append("shiftto", ShiftTo);
    formData.append("Password", Password);
    
    $.ajax({
        type: "POST",
        url: "/HotelManagement/Saveemployeedetails",
        data: formData,
        contentType: false,
        processData: false,
        context: document.body,
        success: function (data) {
            
            alert("Successful");

            window.location.reload();

        },
        error: function (error) {

            console.log(error);
        }
    });
});

function getEmpDetail() {
    
    $.ajax({
        type: 'GET',
        url: "/HotelManagement/GetMethod",
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
                row += "<td>" + value.name + "</td>";
                row += "<td>" + value.address + "</td>";
                row += "<td>" + value.email + "</td>";
                row += "<td>" + value.phone + "</td>";
                row += "<td>" + value.shiftfrom + "</td>";
                row += "<td>" + value.shiftto + "</td>";
                row += "<td>" + value.password + "</td>";
               

                
                row += "<td>" +
                    " <button type='button' class='btn btn-sm btn-success' data-id='' onclick='EditEmployee(" + value.empid + ", event)' ><i class='fa fa-pencil-square-o' aria-hidden='true'></i></button>" +
                    "<button type='button' class='btn btn-sm btn-danger' data-id='' onclick='DeleteOperatorById(" + value.empid + ", event)'><i class='fa fa-trash' aria-hidden='true'></i></button>" +

                    "</td>";

                row += "</tr>";
                //   $("#hiddenId").val(value.operatorId);
                rowcount += 1;
            });
            

            $("#store").append(row);
            //operatorId = $("#hiddenId").val();

        },
        error: function (error) {

            // Handle errors here
            alert("Not Found");
        }
    });
}

function DeleteOperatorById(control, e) {
    
    var Id = control;


    $.ajax({
        type: "POST",
        url: "../HotelManagement/Delete/" + Id,
        //data: (id: Id ), //use id here
        //dataType: "json",
        // contentType: "application/json; charset=utf-8",
        //dataType: "JSON",
        //contentType: "application/json; charset=utf-8",
        success: function (data) {
            
            //getoperator();
            window.location.reload();

        },
        error: function (error) {

            
            alert('Some error occured.');
        }
    });

}

function EditEmployee(Empid, e) {
     
        $.ajax({
            type: "GET",
            url: "../HotelManagement/EditEmployee",
            data: { Empid: Empid },
            dataType:"json",
            context: document.body,
            success: function (data) {
                
                $("#Editempname").val(data.list[0].name);
                $("#Editempaddress").val(data.list[0].address);
                $("#Editempemail").val(data.list[0].email);
                $("#Editempphone").val(data.list[0].phone);
                $("#EditEmpShiftFrom").val(data.list[0].shiftfrom);
                $("#EditEmpShiftTo").val(data.list[0].shiftto);
                $("#EditEmppassword").val(data.list[0].password);
                $("#hiddenId").val(data.list[0].empid)
                $("#Editedmaster-toggle").modal('show');

            },
            error: function (error) {

                
                alert('Some error occured.');
            }
        }); 

}

$('#BtnUpdate').on('click', function () {
    
    var eName = $('#Editempname').val();
    var eAddress = $('#Editempaddress').val();
    var eEmail = $('#Editempemail').val();
    var ePhone = $('#Editempphone').val();
    var editshiftfrom = $('#EditEmpShiftFrom').val();
    var editshiftto = $('#EditEmpShiftTo').val();
    var Password = $('#EditEmppassword').val();
    var editemployeeId = $('#hiddenId').val();
    var phonePattern = /^[0-9]{10}$/;
    var emailRegex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var isValid = true;
    function isValidInput(input) {
        return /^[A-Z][a-z]*$/.test(input);
    }
    function isValidName(input) {
        return /^[A-Z][a-z]* [A-Z][a-z]*$/.test(input);
    }


     //**************************************all condition for validation***************************************

    if (!isValidInput(eName)) {
        $('#Editempname').addClass('border-danger');
        $('#Editempname-error').text('Name should start with a capital letter and only contain alphabets.');
        isValid = false;
    } else {
        $('#Editempname').removeClass('border-danger');
    }

    if (eAddress == '') {
        isValid = false;
        $('#Editempaddress').addClass('border-danger');
    } else {
        $('#Editempaddress').removeClass('border-danger');
    }

    var DocemailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

    if (!eEmail || !DocemailRegex.test(eEmail) || (eEmail.match(/@/g) || []).length !== 1) {
        $("#Editempemail").addClass("border-danger");
        $('#Editempemail-error').text('Please Fill the correct Emailid.');


    }

    if (!phonePattern.test(ePhone) || (ePhone == '')) {
        isValid = false;
        $('#Editempphone').addClass('border-danger');
        $('#Editempphone-error').text('Enter your 10 digits mobile no.');

    }
    else {
        $('#Editempphone').removeClass('border-danger');
    }

    if (editshiftfrom == '') {
        $('#EditEmpShiftFrom').addClass('border-danger');
        isValid = false;
    }
    else {
        $('#EditEmpShiftFrom').removeClass('border-danger');
    }

    if (editshiftto == '') {
        $('#EditEmpShiftTo').addClass('border-danger');
        isValid = false;
    }
    else {
        $('#EditEmpShiftTo').removeClass('border-danger');
    }

    if (Password == '') {
        $('#EditEmppassword').addClass('border-danger');
        isValid = false;
    }
    else {
        $('#EditEmppassword').removeClass('border-danger');
    }

    if (!isValid) {
        alert('Form is Not valid. Please fill out all Details!!!!');
        return;
    }

    var formData = new FormData();

    formData.append('Name', eName);
    formData.append('Address', eAddress);
    formData.append('Email', eEmail);
    formData.append('Phone', ePhone);
    formData.append('shiftfrom', editshiftfrom);
    formData.append('shiftto', editshiftto);
    formData.append('Password', Password );
    formData.append('Empid', editemployeeId);

    $.ajax({
        type: "POST",
        url: "../HotelManagement/UpdateEmployee",
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
