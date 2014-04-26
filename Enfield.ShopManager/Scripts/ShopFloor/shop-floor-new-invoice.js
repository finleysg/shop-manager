// @reference shop-floor.js

/******************************************************************************************************
    DIALOGS
*******************************************************************************************************/
function BindNewInvoiceDialogs() {
    $("#error-dialog").dialog({
        title: "Error Received From the Server",
        autoOpen: false,
        modal: true,
        height: 270,
        width: 420,
        buttons: { "Ok": function () { $(this).dialog("close"); } }
    });
    $("#account-type-dialog").dialog({
        title: "Select Account Type",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 215,
        width: 300,
        buttons: {
            "OK": function () { AssignAccountType(); $(this).dialog("close"); },
            "Cancel": function () { $(this).dialog("close"); }
        }
    });
}

/******************************************************************************************************
    NEW VEHICLE ACCOUNT AUTOCOMPLETE
*******************************************************************************************************/
//function BindAccountNameAutoComplete() {
//    $("#AccountName").autocomplete({
//        source: function (request, response) {
//            $.ajax({
//                url: $("#account-preview-url").val(),
//                dataType: "json",
//                data: {
//                    startsWith: $("#AccountName").val(),
//                    limit: 12
//                },
//                success: function (data) {
//                    $("#AccountId").val("0");
//                    response($.map(data, function (item) {
//                        return {
//                            label: item.DisplayName,
//                            value: item.Id
//                        }
//                    }));
//                },
//                error: function (XMLHttpRequest, textStatus, errorThrown) {
//                    ShowError(errorThrown);
//                }
//            });
//        },
//        minLength: 1,
//        select: function (event, ui) {
//            $("#AccountId").val(ui.item.value);
//            $("#AccountName").val(ui.item.label);
//            return false;
//        },
//        change: function (event, ui) {
//            var accountId = $("#AccountId").val();
//            if (accountId == 0) {
//                $("#account-type-dialog").dialog("open");
//            }
//        },
//        open: function () {
//            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
//        },
//        close: function () {
//            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
//        }
//    });
//}
function AssignAccountType() {
    $("#AccountTypeId").val($("#account-type-id").val());
}
