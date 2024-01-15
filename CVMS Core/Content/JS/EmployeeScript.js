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