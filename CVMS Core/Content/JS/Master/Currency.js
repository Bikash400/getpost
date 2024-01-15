
$('#btnSaveCurrency').click(function (e) {
    e.preventDefault();
    $('#spanMessage').html('');
    debugger
    var CurrencyName = $('#CurrencyName').val();
    var CurrencyCode = $('#CurrencyCode').val();
    var CountryId = $("#CountryId option:selected").val();
    var Remark = $('#Remarks').val();

    var isError = 1;
    var formData = new FormData();

    if (CurrencyName == '') {
        $('#CurrencyName').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#CurrencyName').removeClass('border-danger');
        isError = 0;
    }

    if (CurrencyCode == '') {
        $('#CurrencyCode').addClass('border-danger');
        isError = 1;
    }
    else {
        $('#CurrencyCode').removeClass('border-danger');
        isError = 0;
    }

    if (isError) {
        $('#spanMessage').addClass('text-danger').html('Please fill all highlighted fields');
    }
    else {
        $('#spanMessage').removeClass();
        $('#spanMessage').html('');

        var currencyid = $("#hdnCurrency").val();
        if (currencyid == '')
            currencyid = 0;

        //var objValue = {};
        //objValue.Id = currencyid;
        //objValue.CurrencyCode = CurrencyCode;
        //objValue.CurrencyName = CurrencyName;
        //objValue.CountryId = CountryId;
        //objValue.Remarks = Remark;
        formData.append('Id', currencyid);
        formData.append('CurrencyCode', CurrencyCode);
        formData.append('CurrencyName', CurrencyName);
        formData.append('CountryId', CountryId);
        formData.append('Remarks', Remark);

        $.ajax({
            type: "POST",
            url: "../Master/SaveCurrency",
            data: formData,
            processData: false,
            contentType: false,
            dataType: "json",
            success: function (data) {
                if (data == "success") {
                    ClearSubmitForm();
                    if (currencyid == 0)
                        $("#spanMessage").addClass('text-success').html('Currency Data successfully saved.');
                    else
                        $("#spanMessage").addClass('text-success').html('Currency Data successfully updated.');
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

function ClearSubmitForm() {
    $('#CurrencyName').val('');
    $('#CurrencyCode').val('');
    $('#Remarks').val('');
    $('#CountryId option[value=""]').attr("selected", "selected");
}

function EditCurrency(control, e) {
    debugger
    ClearSubmitForm()
    e.preventDefault();
    //var _row = $(control).parents("tr");
    //var cols = _row.children("td");
    //var currencyId = control; //$(cols[6]).find('input[type="hidden"]').val();

    var _row = $(control).closest("td");
    var cols = _row.children("td");
    var currencyId = control;//$(_row).find('input[type="hidden"]').val();

    if (currencyId > 0) {
        $.ajax({
            type: "GET",
            url: "../Master/GetCurrencyDetail?currencyId=" + currencyId,
            context: document.body,
            success: function (data) {
                debugger
                $("#CurrencyName").val(data.currency[0].currencyName);
                $("#CurrencyCode").val(data.currency[0].currencyCode);
                var countryId = data.currency[0].countryId;
                $("#Remarks").val(data.currency[0].remarks);
                $("#hdnCurrency").val(currencyId);
                debugger;
                $('#CountryId option:selected').remove();
                $('#CountryId option[value=' + countryId + ']').attr("selected", "selected");

                $("#CloseCurrencyModal").modal('show');
            },
            error: function (xhr) {
                debugger;
                alert('Some error occured.');
            }
        });
    }
    else {
        alert('Some error occured. Please try again.');
    }


}

$('#searchData').keyup(function () {
    var searchText = $(this).val();
    $('#tbodyCurrencyTable tr').each(function () {
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

function DeleteCurrency(deletedId) {
    var result = confirm("Are you sure you want to delete this currency");
    if (result) {
        debugger;
        if (deletedId != '' || deletedId > 0) {
            $.ajax({
                type: "GET",
                url: "../Master/DeleteCurrency/" + deletedId,
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    alert(data);
                    window.location.href = '../Master/Currency';
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

function CloseCurrencyModal() {
    //$("#CloseCurrencyModal").modal('show');
    window.location.href = '../Master/Currency';
}




