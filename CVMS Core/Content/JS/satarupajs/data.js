$(document).ready(function () {
    debugger
    alert("123");
    getemployee();

});

function getemployee() {
    debugger
    $.ajax({
        type: 'GET',
        url: "/Satarupa/GetEmployeeList",
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