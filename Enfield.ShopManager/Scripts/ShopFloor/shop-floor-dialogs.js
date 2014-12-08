// @reference shop-floor.js

/******************************************************************************************************
    SHOP FLOOR SUBMENU HANDLERS
*******************************************************************************************************/
function GetNewInvoice(menuItem) {
    if (menuItem.attr("data-enabled")) {
        window.location = menuItem.attr("data-url");
    }
}
function OpenDeleteInvoiceDialog(menuItem) {
    if (menuItem.attr("data-enabled")) {
        $("p.confirm").html("Are you sure we should delete invoice # " + $("#Id").val() + "?");
        $("#confirm-action-dialog").dialog("option", "buttons", {
            "Ok": function () { PostInvoiceDelete(menuItem.attr("data-url")); $(this).dialog("close"); },
            "Cancel": function () { $(this).dialog("close"); }
        });
        $("#confirm-action-dialog").dialog("open");
    }
}
function OpenCompleteInvoiceDialog(menuItem) {
    if (menuItem.attr("data-enabled")) {
        $("p.confirm").html("Are you sure invoice # " + $("#Id").val() + " should be completed?");
        $("#confirm-action-dialog").dialog("option", "buttons", {
            "Ok": function () {
                $("#invoice-to-print").val($("#Id").val());
                OpenInvoiceReport();
                PostInvoiceComplete(menuItem.attr("data-url"));
                $(this).dialog("close");
            },
            "Cancel": function () { $(this).dialog("close"); }
        });
        $("#confirm-action-dialog").dialog("open");
    }
}
function OpenRecallInvoiceDialog(menuItem) {
    if (menuItem.attr("data-enabled")) {
        $("p.confirm").html("Are you sure invoice # " + $("#Id").val() + " should be recalled?");
        $("#confirm-action-dialog").dialog("option", "buttons", {
            "Ok": function () { PostInvoiceRecall(menuItem.attr("data-url")); $(this).dialog("close"); },
            "Cancel": function () { $(this).dialog("close"); }
        });
        $("#confirm-action-dialog").dialog("open");
    }
}
function OpenPrintInvoiceDialog(menuItem) {
    if (menuItem.attr("data-enabled")) {
        $("#invoice-to-print").val($("#Id").val());
        $("#print-invoice-dialog").dialog("option", "buttons", {
            "OK": function () { OpenInvoiceReport(menuItem.attr("data-url")); $(this).dialog("close"); },
            "Cancel": function () { $(this).dialog("close"); }
        });
        $("#print-invoice-dialog").dialog("open");
    }
}
function OpenSignInDialog(menuItem) {
    if (menuItem.attr("data-enabled")) {
        $("#sign-in-name").val("100"); //100 = default employee id
        $("#sign-in-password").val("");
        $("#sign-in-dialog").dialog("option", "buttons", {
            "OK": function () { PostSignInCredentials(menuItem.attr("data-url")); $(this).dialog("close"); },
            "Cancel": function () { $(this).dialog("close"); }
        });
        $("#sign-in-dialog").dialog("open");
    }
}
function OpenSignOutDialog(menuItem) {
    if (menuItem.attr("data-enabled")) {
        $("#sign-out-name").val("0"); //0 = everyone
        $("#sign-out-dialog").dialog("option", "buttons", {
            "OK": function () { PostSignOut(menuItem.attr("data-url")); $(this).dialog("close"); },
            "Cancel": function () { $(this).dialog("close"); }
        });
        $("#sign-out-dialog").dialog("open");
    }
}
function OpenSearchDialog(menuItem) {
    if (menuItem.attr("data-enabled")) {
        var searchUrl = menuItem.attr("data-url");
        $("#invoice-to-search").val("");
        $("#vin-to-search").val("");
        $("#search-dialog").dialog("option", "buttons", {
            "Ok": function () {
                window.location = searchUrl + "?invoiceId=" + $("#invoice-to-search").val() + "&vin=" + $("#vin-to-search").val();
            },
            "Cancel": function () { $(this).dialog("close"); }
        });
        $("#search-dialog").dialog("open");
    }
}
function OpenHistoryDialog(menuItem) {
    if ($("#VIN").val().length == 0) {
        alert("We must have a VIN to add notes or history.");
        return false;
    }
    if (menuItem.attr("data-enabled")) {
        $("#edit-history-note").val("");
        $("#edit-history-dialog").dialog("option", "buttons", {
            "Ok": function () { PostAddHistory(menuItem.attr("data-url"), $("#edit-history-note").val()); $(this).dialog("close"); },
            "Cancel": function () { $(this).dialog("close"); }
        });
        $("#edit-history-dialog").dialog("open");
    }
}

/******************************************************************************************************
    INVOICE DETAIL HANDLERS
*******************************************************************************************************/
function OpenDeleteServiceDialog(tr) {
    $("p.confirm").html("Are you sure we should delete the " + tr.find(".description").html() + " service?");
    $("#confirm-action-dialog").dialog("option", "buttons", {
        "Ok": function () { PostDeleteService(tr.attr("data-id")); $(this).dialog("close"); },
        "Cancel": function () { $(this).dialog("close"); }
    });
    $("#confirm-action-dialog").dialog("open");
}
function OpenEditServiceDialog(tr) {
    $("#edit-servicetype-description").html(tr.find(".description").html());
    $("#edit-service-id").val(tr.attr("data-id"));
    $("#edit-service-rate").val(tr.find(".rate").html().replace("$", ""));
    $("#edit-service-dialog").dialog("open");
}
function OpenNewServiceDialog(div) {
    $("#new-servicetype-description").html(div.html());
    $("#new-servicetype-id").val(div.attr("data-id"));
    $("#new-service-rate").val("0");
    $("#new-service-dialog").dialog("open");
}
function OpenNewLaborDialog(div) {
    $("#new-labortype-description").html(div.html());
    $("#new-labortype-id").val(div.attr("data-id"));
    $("#new-labor-rate").val("");
    $("#new-labor-dialog").dialog("open");
}
function OpenDeleteLaborDialog(tr) {
    if (tr.attr("data-id") == "" || tr.attr("data-id") == "0" || tr.attr("data-id") == 0) {
        alert("The id of this record is missing (labor id = 0), so we can't continue. Try refreshing this invoice by clicking on the same invoice # on the left In Shop listing.");
        return;
    }
    $("p.confirm").html("Are you sure we should delete the " + tr.find(".description").html() + " labor for " + tr.find(".employee").html() + "?");
    $("#confirm-action-dialog").dialog("option", "buttons", {
        "Ok": function () { PostDeleteLabor(tr.attr("data-id")); $(this).dialog("close"); },
        "Cancel": function () { $(this).dialog("close"); }
    });
    $("#confirm-action-dialog").dialog("open");
}
function OpenEditLaborDialog(tr) {
    var laborType = tr.find(".description").html();
    var employee = tr.find(".employee").html();
    $("#edit-labortype-description").html(laborType + " - " + employee);
    $("#edit-labor-id").val(tr.attr("data-id"));
    $("#edit-labor-rate").val(tr.find(".rate").html().replace("$", ""));
    $("#edit-labor-dialog").dialog("open");
}
function OpenEditAccountDialog(a) {
    $("#edit-account-id").val("0");
    $("#edit-account-name").val("");
    $("#edit-account-dialog").dialog("option", "buttons", {
        "Ok": function () {
            PostUpdateAccount($("#edit-account-id"));
            $("#AccountName").val($("#edit-account-name").val());
            $(this).dialog("close");
        },
        "Cancel": function () { $(this).dialog("close"); }
    });
    $("#edit-account-dialog").dialog("open");
}

/******************************************************************************************************
    DIALOG BINDING
*******************************************************************************************************/
function BindShopFloorDialogs() {
    $("#edit-service-dialog").dialog({
        title: "Change Service Rate",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 200,
        width: 300,
        buttons: {
            "OK": function () { PostEditServiceRate(); $(this).dialog("close"); },
            "Cancel": function () { $(this).dialog("close"); }
        }
    });
    $("#edit-labor-dialog").dialog({
        title: "Change Labor Rate",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 200,
        width: 300,
        buttons: {
            "OK": function () { PostEditLaborRate(); $(this).dialog("close"); },
            "Cancel": function () { $(this).dialog("close"); }
        }
    });
    $("#new-service-dialog").dialog({
        title: "Enter Service Rate",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 215,
        width: 300,
        buttons: {
            "OK": function () { PostAddService(); $(this).dialog("close"); },
            "Cancel": function () { $(this).dialog("close"); }
        }
    });
    $("#new-labor-dialog").dialog({
        title: "Enter Base Labor Rate",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 215,
        width: 300,
        buttons: {
            "OK": function () { PostAddLabor(); $(this).dialog("close"); },
            "Cancel": function () { $(this).dialog("close"); }
        }
    });
    $("#edit-history-dialog").dialog({
        title: "Add a Note to History",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 194,
        width: 256,
        buttons: {
            "OK": function () { PostAddHistory(); $(this).dialog("close"); },
            "Cancel": function () { $(this).dialog("close"); }
        }
    });
    $("#edit-account-dialog").dialog({
        title: "Change the Account",
        modal: true,
        autoOpen: false,
        height: 200,
        width: 300
    });
    $("#confirm-action-dialog").dialog({
        title: "Confirm Action",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 150,
        width: 300,
        buttons: {
            "OK": function () { },
            "Cancel": function () { $(this).dialog("close"); }
        }
    });
    $("#sign-in-dialog").dialog({
        title: "Sign In",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 210,
        width: 300,
        buttons: {
            "OK": function () { },
            "Cancel": function () { $(this).dialog("close"); }
        }
    });
    $("#sign-out-dialog").dialog({
        title: "Sign Out",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 210,
        width: 300,
        buttons: {
            "OK": function () { },
            "Cancel": function () { $(this).dialog("close"); }
        }
    });
    $("#print-invoice-dialog").dialog({
        title: "Print Invoice",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 200,
        width: 300,
        buttons: {
            "OK": function () { },
            "Cancel": function () { $(this).dialog("close"); }
        }
    });
    $("#search-dialog").dialog({
        title: "Search",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 240,
        width: 300,
        buttons: {
            "OK": function () { OpenSearchDialog(); $(this).dialog("close"); },
            "Cancel": function () { $(this).dialog("close"); }
        }
    });
}

/******************************************************************************************************
    PRINT INVOICE
*******************************************************************************************************/
function OpenInvoiceReport(url) {
    if (url == null || url.length == 0)
        url = $("#invoice-report-url").val();
    window.open(url + "/" + $("#invoice-to-print").val());
}

/******************************************************************************************************
    UTILITY / MISCELLANY
*******************************************************************************************************/
function GetLastServiceAdded() {
    var serviceId = 0;
    var tr = null;
    $("#service-detail").find("tr").each(function (i) {
        var id = $(this).attr("data-id");
        if (id > serviceId) {
            serviceId = id;
            tr = $(this);
        }
    });
    return tr;
}