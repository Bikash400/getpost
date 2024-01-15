
$('#btnSaveCountry').click(function (e) {
    e.preventDefault();
    $('#spanMessage').html('');
    debugger
    var Country = $('#Country').val();
    var CountryCode = $('#CountryCode').val();
    var region = $("#RegionID option:selected").val();
    var TimeZone = $("#TimeZoneId option:selected").val();
    var Currency = $("#CurrencyId option:selected").val();
    var Remark = $('#Remarks').val();

    var formData = new FormData();
    var isError = 1;

    if (Country == '') {
        $('#Country').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#Country').removeClass('border-danger');
        isError = 0;
    }

    if (CountryCode == '') {
        $('#CountryCode').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#CountryCode').removeClass('border-danger');
        isError = 0;
    }

    if (region == '') {
        $('#RegionID').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#RegionID').removeClass('border-danger');
        isError = 0;
    }

    if (TimeZone == '') {
        $('#TimeZoneId').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#TimeZoneId').removeClass('border-danger');
        isError = 0;
    }

    if (Currency == '') {
        $('#CurrencyId').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#CurrencyId').removeClass('border-danger');
        isError = 0;
    }


    if (isError) {
        $('#spanMessage').addClass('text-danger').html('Please fill all highlighted fields');
    }
    else {
        $('#spanMessage').removeClass();
        $('#spanMessage').html('');

        var countryid = $("#hdnCountry").val();
        if (countryid == '')
            countryid = 0;

        //var objValue = {};
        //objValue.CountryID = countryid;
        //objValue.CountryCode = CountryCode;
        //objValue.Country = Country;
        //objValue.RegionID = region;
        //objValue.Remarks = Remark;
        //objValue.CurrencyId = Currency;
        //objValue.TimeZoneId = TimeZone;
        formData.append('CountryID', countryid);
        formData.append('CountryCode', CountryCode);
        formData.append('Country', Country);
        formData.append('RegionID', region);
        formData.append('Remarks', Remark);
        formData.append('CurrencyId', Currency);
        formData.append('TimeZoneId', TimeZone);

        $.ajax({
            type: "POST",
            url: "../Master/SaveCountry",
            data: formData,
            processData: false,
            contentType: false,
            dataType: "json",
            success: function (data) {
                debugger
                if (data == "success") {
                    ClearCountryForm();
                    if (countryid == 0)
                        $("#spanMessage").addClass('text-success').html('Country Data successfully saved.');
                    else
                        $("#spanMessage").addClass('text-success').html('Country Data successfully updated.');
                }
                else {
                    $("#spanMessage").addClass('text-danger').html(data);
                }
            },
            error: function (data, error) {
                $("#spanMessage").addClass('text-danger').html(data);
            }
        });
    }

});

function ClearCountryForm() {
    $('#Country').val('');
    $('#CountryCode').val('');
    $('#Remarks').val('');
    //if ($('#RegionID > option').length > 0)
    //    $("#RegionID")[0].selectedIndex = 0
    //if ($('#TimeZoneId > option').length > 0)
    //    $("#TimeZoneId")[0].selectedIndex = 0;
    //if ($('#CurrencyId > option').length > 0)
    //    $("#CurrencyId")[0].selectedIndex = 0;

    $('#RegionID option[value=""]').attr("selected", "selected");
    $('#CurrencyId option[value=""]').attr("selected", "selected");
    $('#TimeZoneId option[value=""]').attr("selected", "selected");


}

function EditCountry(control, e) {
    debugger
    ClearCountryForm()
    e.preventDefault();
    var _row = $(control).parents("tr");
    var cols = _row.children("td");
    var countryId = $(cols[6]).find('input[type="hidden"]').val();

    if (countryId > 0) {
        $.ajax({
            type: "GET",
            // traditional: true,
            //  async: false,
            //  cache: false,
            url: "../Master/GetCountryDetail?countryId=" + countryId,
            context: document.body,
            // data: '{Id: ' + countryId + '}',
            success: function (data) {
                debugger
                $("#Country").val(data.country[0].country);
                $("#CountryCode").val(data.country[0].countryCode);
                var Region = data.country[0].regionID;
                var Currency = data.country[0].currencyId;
                var Zone = data.country[0].timeZoneId;
                $("#Remarks").val(data.country[0].remarks);
                $("#hdnCountry").val(countryId);
                debugger;
                $('#RegionID option:selected').remove();
                $('#CurrencyId option:selected').remove();
                $('#TimeZoneId option:selected').remove();

                $('#RegionID option[value=' + Region + ']').attr("selected", "selected");
                $('#CurrencyId option[value=' + Currency + ']').attr("selected", "selected");
                $('#TimeZoneId option[value=' + Zone + ']').attr("selected", "selected");

                $("#CountrySaveModal").modal('show');
            },
            error: function (xhr) {
                debugger;
                //$("#spanMessage").addClass('text-danger').html('Some error occured.');
                alert('Some error occured.');
            }
        });
    }
    else {
        alert('Some error occured. Please try again.');
    }


}

$('#searchData').keyup(function () {
    // FilterCountryTable($(this).val());
    var searchText = $(this).val();
    //$("#tableBodyCountry tr").filter(function () {
    //    $(this).find('td:nth(0)').toggle($(this).text().toLowerCase().indexOf(searchText) > -1)
    //});

    $('#tableBodyCountry tr').each(function () {
        var found = 'false';
        //$(this).find('td:nth(0)').each(function () {     /////// when you want to searching on particuler column.
        $(this).each(function () {
            if ($(this).text().toLowerCase().indexOf(searchText.toLowerCase()) >= 0) {
                found = 'true';
            }
        });
        if (found == 'true') {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });
});

function DeleteCountry(deletedId) {
    debugger
    var result = confirm("Are you sure you want to delete this country");
    if (result) {
        debugger;
        if (deletedId != '' || deletedId > 0) {
            $.ajax({
                type: "GET",
                url: "../Master/DeleteCountry/" + deletedId,
                //  data: '{Id: ' + countryId + '}',
                contentType: "application/json; charset=utf-8",
                // dataType: "json",
                success: function (data) {
                    alert(data);
                    window.location.href = '../Master/Country';
                },
                error: function (data, error) {
                    alert(data);
                }
            });
        }
        else {
            alert('Please select valid record.')
        }
    }
    else {

    }
}

function CloseCountryModal() {
    // $("#CountrySaveModal").modal('show');
    window.location.href = '../Master/Country';
}




