// @reference ../jQuery/jquery.js
// @reference ../jQueryUi/jquery-ui.js

/******************************************************************************************************
INITIALIZATION
*******************************************************************************************************/
function Initialize(section, page) {
    InitializeMainMenu();
    switch (section) {
        case "administration":
            InitializeAdministration(page);
            break;
        case "shop-floor":
            InitializeShopFloor(page);
            break;
        case "accounts":
            InitializeAccounts(page);
            break;
        case "reporting":
            InitializeReporting(page);
            break;
        default:
            break;
    }
    $(".dp").datepicker({
        changeMonth: true,
        changeYear: true,
        dateFormat: 'yy-mm-dd'
    });
    $("#error-dialog").dialog({
        title: "Error Received From the Server",
        autoOpen: false,
        modal: true,
        height: 270,
        width: 420,
        buttons: { "Ok": function () { $(this).dialog("close"); } }
    });
}
function InitializeMainMenu() {
    $("#main-menu>li").click(function (e) {
        if ($(this).attr("data-enabled") == "True" && $(this).attr("data-selected") != "True") {
            document.location.href = $(this).attr("data-url");
        }
    });
}
function InitializeSubMenu() {
    $("#sub-menu li").click(function (e) {
        if ($(this).attr("data-enabled") == "True" && $(this).attr("data-selected") != "True") {
            document.location.href = $(this).attr("data-url");
        }
    });
}

function ShowError(text) {
    $(".error-detail").html("<p>" + text + "</p>");
    $("#error-dialog").dialog("open");
}
function ClearError() {
    $(".error-detail").html("<p></p>");
}

function ShowBusy() {
}
function ClearBusy() {
}

/******************************************************************************************************
    ACCOUNT AUTOCOMPLETE
*******************************************************************************************************/
function BindAccountNameAutoComplete(nameField, idField) {
    $(nameField).autocomplete({
        source: function (request, response) {
            $.ajax({
                url: $("#account-preview-url").val(),
                dataType: "json",
                data: {
                    startsWith: $(nameField).val(),
                    limit: 12
                },
                success: function (data) {
                    $(idField).val("0");
                    response($.map(data, function (item) {
                        return {
                            label: item.DisplayName,
                            value: item.Id
                        }
                    }));
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    ShowError(errorThrown);
                }
            });
        },
        minLength: 1,
        select: function (event, ui) {
            $(idField).val(ui.item.value);
            $(nameField).val(ui.item.label);
            return false;
        },
        change: function (event, ui) {
            var accountId = $(idField).val();
            if (accountId == 0) {
                $("#account-type-dialog").dialog("open");
            }
        },
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        }
    });
}

function isNumber(n) {
    return !isNaN(parseFloat(n)) && isFinite(n);
}