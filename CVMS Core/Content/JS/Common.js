function isValidEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email)
        ;
}

$('.allownumeric').keypress(function (e) {
    var charCode = (e.which) ? e.which : event.keyCode
    if (String.fromCharCode(charCode).match(/[^0-9]/g))
        return false;
});

$('.allownumericspecialchar').keypress(function (e) {
    var isValid = false;
    var charCode = (e.which) ? e.which : event.keyCode
    var regex = /^[0-9-+()]*$/;
    isValid = regex.test(charCode);
    return isValid;
});

$('.allowOnlyAlphabetsNumbersUnderscore').keypress(function (e) {
    //var pressedKey;
    //if (window.event) {
    //    pressedKey = e.keyCode;
    //} else if (e.which) {
    //    pressedKey = e.which;
    //}
    //alert(String.fromCharCode(pressedKey));


    var pressedKey = e.keyCode || e.which;
    var InputKeyValue = String.fromCharCode(pressedKey);
    var InputValue = this.value;
    var InputValueLength = InputValue.length;
    //Regex to allow only Alphabets Numbers Underscore
    var pattern = /^[a-z\d\_]+$/i;
    //Validating the textBox value against our regex pattern.
    var isValid = pattern.test(InputKeyValue);

    if (!isValid) {
        return false;
    }
    //if (e.which == 32) {
    //    return false;
    //}
    if (InputValueLength > 20) {
        return false;
    }
});

$('.allowOnlyAlphabetsNumbersUnderscoreSpace').keypress(function (e) {
    var pressedKey = e.keyCode || e.which;
    var InputKeyValue = String.fromCharCode(pressedKey);
    var InputValue = this.value;
    var InputValueLength = InputValue.length;
    //Regex to allow only Alphabets Numbers Underscore Space
    var pattern = /^[a-z\d\_\s]+$/i;
    //Validating the textBox value against our regex pattern.
    var isValid = pattern.test(InputKeyValue);

    if (!isValid) {
        return false;
    }
    //if (e.which == 32) {
    //    return false;
    //}
    if (InputValueLength > 20) {
        return false;
    }
});

function showloader() {
    $('#divLoader').show();
}

function hideloader() {
    $('#divLoader').hide();
}

/*Dragable modal*/
//$('.modal-dialog').draggable();


/*open modal on keyboard shotcut key*/
$(document).on('keydown', function (e) {
    // You may replace `m` with whatever key you want
    if ((e.metaKey || e.ctrlKey) && (String.fromCharCode(e.which).toLowerCase() === 'm')) {
        $(".modal").modal('show');
    }
});
$(document).ready(function () {
    $('.search-strip').find('> .btn-add-master').addClass("add_popup");
    $('.add_popup').html('+ Add / <span class="badge badge-danger">(CTRL + M)</span>');

    //var countAddButton = 1;
    //var countAddPopUp = 1;
    //$('.search-strip').find('.btn-add-master').each(function () {
    //    debugger
    //    if (countAddButton == 1) {
    //        var existText = $(this).html();
    //        $(this).html(existText + ' / <span class="badge badge-danger">(CTRL + M)</span>');
    //    }
    //    countAddButton += 1;
    //});

    //$('.add_popup').each(function () {
    //    debugger
    //    if (countAddPopUp == 1) {
    //        var existText = $(this).html();
    //        $(this).html(existText + ' / <span class="badge badge-danger">(CTRL + M)</span>');
    //    }
    //    countAddPopUp += 1;
    //});
})



/*sidebar slider*/
$(".close-slider").click(function () {
    $(".side-slider").toggleClass("hide-side-slider");
    $(".close-slider > .fa").toggleClass("fa-times fa-chevron-left");
    // $(".close-slider > .fa").addClass("fa-chevron-left");

});

/*Input plus minus values with button*/

function minusclick(control) {

    //var $input = $(control).parent().find('input');
    //alert($input.val());
    //var count = parseInt($input.val()) - 1;
    //count = count < 1 ? 1 : count;
    //$input.val(count);
    //$input.change();
    //return false;


    var $input = $(control).parent().find('input:text');
    // alert($input.val());
    var count = parseInt($input.val()) - 1;
    count = count < 1 ? 1 : count;
    $input.val(count);
    $input.change();
    return false;
};

function plusclick(control) {
    //var $input = $(control).parent().find('input');
    //$input.val(parseInt($input.val()) + 1);
    //$input.change();
    //return false;


    var $input = $(control).parent().find('input:text');
    $input.val(parseInt($input.val()) + 1);
    $input.change();
    return false;
};

/*background-color change*/
function myFunction() {
    document.querySelectorAll('.wraper-main-detail').style.backgroundColor = "lightblue";
}

$(document).ready(function () {
    var colorWell;
    var defaultColor = "#ffffff";
    window.addEventListener("load", startup, false);
    function startup() {
        colorWell = document.querySelector("#color1");
        colorWell.value = "#ffffff";
        colorWell.addEventListener("input", updateFirst, false);
        colorWell.select();
    }

    function updateFirst(event) {
        $(".wraper-main-detail").css('backgroundColor', event.target.value);

    }
});

function CheckPasswordFormat(strInput) {
    var result = '';

    var patternLowercase = new RegExp("^(?=.*[a-z]).+$");
    var patternUppercase = new RegExp("^(?=.*[A-Z]).+$");
    var patternNumeric = new RegExp("^(?=.*\\d).+$");
    var patternSpecialCharacter = new RegExp("^(?=.*[-+_!@#$%^&*.,?]).+$");

    var stringLength = strInput.length;
    var whiteSpaceLength = strInput.indexOf(' ');

    if (whiteSpaceLength >= 0) {
        result += '=> <div class="col-sm-12">Space not allow</div>';
    }
    else {
        if (stringLength < 6 || stringLength > 17) {
            result += '<div class="col-sm-12">=> Password lenght should be minimum 6 character and maximum 16  character.</div>'
        }
        if (!patternLowercase.test(strInput)) {
            result += '<div class="col-sm-12">=> Lower case required at least one.</div>';
        }
        if (!patternUppercase.test(strInput)) {
            result += '<div class="col-sm-12">=> Upper case required at least one.</div>';
        }
        if (!patternNumeric.test(strInput)) {
            result += '<div class="col-sm-12">=> Numeric case required at least one.</div>';
        }
        if (!patternSpecialCharacter.test(strInput)) {
            result += '<div class="col-sm-12">=> Special case required at least one.</div>';
        }
    }


    return result;
}