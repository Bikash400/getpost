
function BackToService() {

    debugger;
    //if (ParamValue != '' && idurl != "#") {
    /*window.location.href = "" + idurl + "?param=" + ParamValue*/
    window.location.href = '../Account/BackToService';
    //$.ajax({
    //    //type: "POST",
    //    url: "../Account/BackToService",
    //    //data: { 'param': ParamValue },
    //    contentType: "application/json; charset=utf-8",
    //    context: document.body,
    //    success: function (data) {
    //        debugger
    //        if (data != '')
    //            window.location.href = data;
    //    },
    //    error: function (xhr) {
    //        debugger;
    //        alert('Some error occured.');
    //        window.location.href = "https://localhost:64741";
    //    }
    //});
    ////}
    ////else {
    ////    alert('Link is not Activeted');
    ////}    
}

function GetModuleUrl(control, mid) {
    debugger
    //http://ants.cvms.world
    var idurl = control.id;
    //idurl = 'https://localhost:44378/Vendor/Dashboard';
    //var txt = control.text();

    //var ss = idurl.replace("world", "helllo");


    var hdnId = $(control).closest().find("input [type='hidden']").val();
    var ParamValue = GetParameterValues('param');

    if (ParamValue != '' && idurl != "#" && mid != '' && typeof mid != 'undefined') {
        window.location.href = "" + idurl + "?param=" + ParamValue + "&param2=" + mid
    }
    else {
        //  alert('Link is not Activeted');
        //$('#spanWaringMessage').html('This service coming soon...');
        //$('#warningModal').modal('show');
        // return false;
    }
}

function GetParameterValues(param) {
    debugger
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] == param) {
            return urlparam[1];
        }
    }
}
