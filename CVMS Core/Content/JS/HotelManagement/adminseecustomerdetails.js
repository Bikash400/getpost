$(document).ready(function () {
   
    getadminseecustomerdetails();
    getadminhotelbinding();
    getadmincitybinding();
    });



$("#city").change(function () {
    
    getadminhotelbinding();


});
function getadminhotelbinding() {
   

    $("#hotel").empty();
    $("#hotel").append($('<option>Select</option>'));

    var cid = $("#city").val();

    $.ajax({
        type: 'GET',
        url: "../HotelManagement/GetadminHotelDetails",
        data: { cid: cid },
        dataType: 'json',
        context: document.body,
        success: function (data) {
            


            $.each(data.hotel, function (item, value) {
                
                $("#hotel").append($("<option></option>").val(value.hotelid).html(value.hotelname));
            });

        },
        error: function (error) {

            //Handle errors here
            alert("Not Found");
        }
    });
}

//---------------------------------------------------------------------------------------------------------------


$("#hotel").change(function () {
    
    var filterhotel = $("#state option:selected").html().toLowerCase();
    $('#usertbl tbody tr').each(function () {
        
        var lineStr = $(this).text().toLowerCase();
        if (lineStr.indexOf(filterhotel) === -1) {
            $(this).hide();
        } else {
            $(this).show();
        }
    });
});
$("#city").change(function () {
    GetEditHotel();


});

function GetEditHotel() {

    $("#hotel").empty();
    $("#hotel").append($('<option>Select</option>'));

    var HotelSelector = $("#city").val();

    $.ajax({
        type: 'GET',
        url: "../HotelManagement/GetadminHotelDetails",
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

function getadmincitybinding() {
    
    $.ajax({
        type: 'GET',
        url: "../HotelManagement/GetadminCityDetails",
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
//--------------------------------------------------------------------------------------------------------
$("#city").change(function () {
    
    var filtercity = $("#city option:selected").html().toLowerCase();

    $('#usertbl tbody tr').each(function () {
        
        var lineStr = $(this).text().toLowerCase();
        if (lineStr.indexOf(filtercity) === -1) {
            $(this).hide();
        } else {
            $(this).show();
        }
    });
});

function getadminseecustomerdetails() {
    
    $.ajax({
        type: 'GET',
        url: "/HotelManagement/Getadmincustomerbookingdetails",
        data: {},
        dataType: 'json',
        context: document.body,
        success: function (data) {

            
            var row = "";
            var rowcount = 1;

            $.each(data.customer, function (item, value) {
                
                
                row += "<tr>";
                row += "<td>" + rowcount + "</td>";
                row += "<td>" + value.firstName + "</td>";
                row += "<td>" + value.lastName + "</td>";
                row += "<td>" + value.aadharNumber + "</td>";
                row += "<td>" + value.address + "</td>";
                row += "<td>" + value.cityname + "</td>";
                row += "<td>" + value.hotelname + "</td>";
                row += "<td>" + value.phonenumber + "</td>";
                row += "<td>" + value.emailid + "</td>";
                row += "<td>" + value.roomtype + "</td>";
                row += "<td>" + value.numberofAdults + "</td>";
                //row += "<td>" +
                //    "<button type='button' class='btn btn-sm btn-info'  data-id='' onclick='ViewOperator(" + value.operatorId + ", event)' ><i class='fa fa-eye' aria-hidden='true'></i></button>" +
                //    " <button type='button' id='editnew' class='btn btn-sm btn-success' data-id='' onclick='EditFault(" + value.id + ", event)' ><i class='fa fa-pencil-square-o' aria-hidden='true'></i></button>" +
                //    "<button type='button'  class='btn btn-sm btn-danger' data-id='' onclick='DeleteFault(" + value.id + ", event)'><i class='fa fa-trash' aria-hidden='true'></i></button>" +
                //    "</td>";

                row += "</tr>";

                rowcount += 1;
            })
            $("#mytable").append(row);
        },
        error: function (error) {

            alert("Not Found");
        }
    });
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