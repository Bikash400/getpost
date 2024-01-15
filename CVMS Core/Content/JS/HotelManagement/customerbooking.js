$(document).ready(function () {
    GetcustomerStatus();
})


  
$('#hotelbooking').click(function () {
    window.location.href = 'https://localhost:7044/hotelmanagement/cutomerformbooking';
    });

var Userid = $("#hiddenid").val();
$("#bookHotelLink").click(function (e) {
    
    e.preventDefault();

    // Construct the new URL with Userid as a parameter
    var newUrl = $(this).attr("href") + "?Userid=" + Userid;

    // Redirect to the new URL
    window.location.href = newUrl;
});


function GetcustomerStatus() {
    
    var Userid = $("#hiddenid").val();
    $.ajax({
        type: "GET",
        url: "/HotelManagement/Getcustomerstatus",
        data: { Userid: Userid },
        dataType: 'json',
        context: document.body,
        success: function (data) {
            
            var row = "";
            $.each(data.customer ,function (item, value) {
                
                row += "<tr>";
                row += "<td>" + value.firstName + "</td>";
                row += "<td>" + value.lastName + "</td>";
                row += "<td>" + value.aadharNumber + "</td>";
                row += "<td>" + value.address + "</td>";
                row += "<td>" + value.city + "</td>";
                row += "<td>" + value.hotel + "</td>";
                row += "<td>" + value.phone + "</td>";
                row += "<td>" + value.emailid + "</td>";
                row += "<td>" + value.roomPreference + "</td>";
                row += "<td>" + value.numberofAdults + "</td>";
                row += "<td>" + value.checkin + "</td>";
                row += "<td>" + value.checkout + "</td>";
                row += "<td>" + value.payment + "</td>";
                row += "</tr>";
            });
            $("#patienttable").append(row);

        },
        error: function (error) {

            alert("error");
        }
    });
}
