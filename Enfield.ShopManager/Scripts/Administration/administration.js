// @reference ../Common/shop.js

function InitializeAdministration(page) {
    InitializeSubMenu(); //in shop.js
    switch (page) {
        case "invoice-listing":
            BindPaidFlags();
            BindSizeFilterEvent();
            BindFormSubmit();
            break;
        case "invoice-detail":
            //BindAutoComplete();
            BindInvoiceAdministrationEvents();
            BindInvoiceDialogs()
            BindFormSubmit();
            break;
        case "user-listing":
            BindSizeFilterEvent();
            break;
        case "user-detail":
            BindPasswordGenerator();
            break;
        case "services-and-labor":
            BindContextMenus();
            BindServiceAndLaborDialogs();
            BindAjaxButtons();
            break;
        case "location-detail":
            BindAccountNameAutoComplete("#DefaultAccountName", "#DefaultAccountId");
            break;
        case "account-listing":
            BindSizeFilterEvent();
        case "security-log":
            BindSizeFilterEvent();
        default:
            break;
    }
}

function BindPaidFlags() {
    $("td input").click(function (e) {
        PostUpdateInvoicePaidStatus($(this));
    });
}

function BindSizeFilterEvent() {
    $("#Filter_Size").change(function (e) {
        $("#Size").val($(this).find(":selected").attr("value"));
        $("form").submit();
    });
}
function BindFormSubmit() {
    $("#apply-filter").click(function (e) {
        $("#DoEvaluate").val("true");
        $("#Page").val("1");
        $("form").submit();
    });
}
function BindPasswordGenerator() {
    $("#generate-password").click(function (e) {
        $.getJSON($(this).data("url"),
            function (data) {
                $("#User_PasswordString").val(data);
            }
        );
    });
}
function BindInvoiceAdministrationEvents() {
    $(".labor-edit").livequery("click", function () { OpenEditLaborDialog($(this).parents("tr")); });
    $(".service-edit").livequery("click", function () { OpenEditServiceDialog($(this).parents("tr")); });
    $("#IsPaid").click(function (e) { PostUpdateInvoicePaidStatus($(this)); });
    $("#PurchaseOrderNumber").change(function (e) { PostUpdateInvoicePurchaseOrder($(this)); });
}

/******************************************************************************************************
    DEFINE CONTEXT MENUS
*******************************************************************************************************/
function BindContextMenus() {
    $.contextMenu({
        selector: '.account-type',
        callback: function (key, options) {
            OpenAddServiceDialog($(this).context);
        },
        items: {
            "add": { name: "Add Service", icon: "copy" }
        }
    });
    $.contextMenu({
        selector: '.account-type-service',
        callback: function (key, options) {
            if (key == "add") {
                OpenAddLaborDialog($(this).context);
            }
            else if (key == "delete") {
                OpenRemoveServiceDialog($(this).context);
            }
            else {
                OpenUpdateServiceTypeDialog($(this).context);
            }
        },
        items: {
            "add": { name: "Add Labor", icon: "copy" },
            "delete": { name: "Remove Service", icon: "delete" },
            "rename": { name: "Rename", icon: "edit" }
        }
    });
    $.contextMenu({
        selector: '.account-type-labor',
        callback: function (key, options) {
            if (key == "delete") {
                OpenRemoveLaborDialog($(this).context);
            }
            else {
                OpenUpdateLaborTypeDialog($(this).context);
            }
        },
        items: {
            "delete": { name: "Remove Labor", icon: "delete" },
            "rename": { name: "Rename", icon: "edit" }
        }
    });
}

function BindAjaxButtons() {
    $("#add-service").click(function (e) { OpenCreateServiceDialog($(this));  });
    $("#add-labor").click(function (e) { OpenCreateLaborDialog($(this)); });
    $("#account-type-id").change(function (e) {
        var selected = $(this).find('option:selected');
        window.location = selected.data("url");
    });
}


