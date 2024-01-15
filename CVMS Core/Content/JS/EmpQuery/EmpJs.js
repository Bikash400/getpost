//$(document).ready(function () {
//    alert("123");


//});

//$('#submitbtn').click(function () {
//    saveemp();
//});



//function saveemp() {
//    debugger
//    var name = $('#name').val();
//    var age = $('#age').val();
//    var gender = $('#gender').val();
//    var address = $('#address').val();


//    var formData = new FormData();

//    formData.append('Name', name);
//    formData.append('Age', age);
//    formData.append('Gender', gender);
//    formData.append('Address', address);

//    $.ajax({
//        type: "POST",
//        url: "../Employee/Savemployee",
//        data: formData,
//        contentType: false,
//        processData: false,

//        context: document.body,
//        success: function (data) {
//            debugger

//            alert('data has been stored to database');
//        }
//    });
//};


$(document).ready(function () {
    alert("123");
    getemployee();


});

$('#submitbtn').click(function () {
    var name = $('#name').val();
    var age = $('#age').val();
    var gender = $("#gender").val();
    var address = $("#address").val();

    var iserror = false; // Initialize error flag

    // Check for empty values and add/remove the 'border-danger' class accordingly
    if (name == '') {
        $('#name').addClass('border-danger');
        iserror = true;
    } else {
        $('#name').removeClass('border-danger');
    }

    if (age == '') {
        $('#age').addClass('border-danger');
        iserror = true;
    } else {
        $('#age').removeClass('border-danger');
    }

    if (gender == '') {
        $('#gender').addClass('border-danger');
        iserror = true;
    } else {
        $('#gender').removeClass('border-danger');
    }

    if (address == '') {
        $('#address').addClass('border-danger');
        iserror = true;
    } else {
        $('#address').removeClass('border-danger');
    }

    // If any field is empty, do not proceed with saving the data
    if (iserror) {
        alert('Please Enter Valid Data!!!');
        return; // Exit the function without making the AJAX request
    }

    // All fields are filled, proceed to save employee data
    saveemp(name, age, gender, address);
});

function saveemp(name, age, gender, address) {
    debugger;

    var formData = new FormData();
    formData.append('Name', name);
    formData.append('Age', age);
    formData.append('Gender', gender);
    formData.append('Address', address);

    $.ajax({
        type: "POST",
        url: "../Employee/Savemployee",
        data: formData,
        contentType: false,
        processData: false,
        context: document.body,
        success: function (data) {
            debugger;
            alert('Data has been stored in the database');
            window.location.reload();

        }

    });
}
function getemployee() {
    debugger
    $.ajax({
        type: 'GET',
        url: "../Employee/GetEmployeeList",
        data: {},
        dataType: 'json',
        context: document.body,
        success: function (data) {
            debugger
            var row = "";
            var rowcount = 1;
            //console.log(employees);
            $.each(data.empClasses, function (item, value) {
                debugger
                row += "<tr>";
                row += "<td>" + rowcount + "</td>";
                row += "<td>" + value.name + "</td>";
                row += "<td>" + value.age + "</td>";
                row += "<td>" + value.gender + "</td>";
                row += "<td>" + value.address + "</td>";
                row += "</tr>";
                rowcount += 1;
            });
            $("#employeeTable").append(row);

        },
        error: function (error) {
            debugger
            // Handle errors here
            alert("Not Found");
        }
    });
}

