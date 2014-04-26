// @reference ../Common/shop.js

function InitializeShopFloor(page) {
    BindShopFloorSubMenu();
    switch (page) {
        case "index":
            BindInvoiceNavigation();
            BindInvoiceEvents();
            BindShopFloorDialogs();
            BindShopFloorDraggables();
            BindShopFloorDragTargets();
            BindChangeAccountAutoComplete();
            break;
        case "new-vehicle":
            BindNewInvoiceDialogs();
            BindAccountNameAutoComplete("#AccountName", "#AccountId");
            break;
        default:
            break;
    }
}

/******************************************************************************************************
    SHOP FLOOR EVENT BINDING
*******************************************************************************************************/
function BindShopFloorSubMenu() {
    $("li[id=new-vehicle-menuitem]").bind("click", function (e) { GetNewInvoice($(this)); });
    $("li[id=print-invoice-menuitem]").bind("click", function (e) { OpenPrintInvoiceDialog($(this)); });
    $("li[id=complete-vehicle-menuitem]").bind("click", function (e) { OpenCompleteInvoiceDialog($(this)); });
    $("li[id=recall-vehicle-menuitem]").bind("click", function (e) { OpenRecallInvoiceDialog($(this)); });
    $("li[id=add-history-menuitem]").bind("click", function (e) { OpenHistoryDialog($(this)); });
    $("li[id=delete-invoice-menuitem]").bind("click", function (e) { OpenDeleteInvoiceDialog($(this)); });
    $("li[id=find-invoice-menuitem]").bind("click", function (e) { OpenSearchDialog($(this)); });
    $("li[id=sign-in-menuitem]").bind("click", function (e) { OpenSignInDialog($(this)); });
    $("li[id=sign-out-menuitem]").bind("click", function (e) { OpenSignOutDialog($(this)); });
}
function BindInvoiceNavigation() {
    $(".shop-vehicle").bind("click", function (e) {
        window.location = $(this).attr("data-url");
    });
}
function BindInvoiceEvents() {
    $("#service-detail a.edit").livequery("click", function () { OpenEditServiceDialog($(this).parents("tr")); });
    $("#service-detail a.delete").livequery("click", function () { OpenDeleteServiceDialog($(this).parents("tr")); });
    $("#labor-detail a.edit").livequery("click", function () { OpenEditLaborDialog($(this).parents("tr")); });
    $("#labor-detail a.delete").livequery("click", function () { OpenDeleteLaborDialog($(this).parents("tr")); });
    $(".updateable").change(function (e) { PostUpdateInvoice($(this)); });
    $("#edit-account").click(function (e) { OpenEditAccountDialog($(this)); });
}
function BindChangeAccountAutoComplete() {
    $("#edit-account-name").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: $("#account-preview-url").val(),
                dataType: "json",
                data: {
                    startsWith: $("#edit-account-name").val(),
                    limit: 12
                },
                success: function (data) {
                    $("#edit-account-id").val("0");
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
            if (ui.item.value != 0 || ui.item.value != "0" || ui.item.value != "") {
                $("#edit-account-id").val(ui.item.value);
                $("#edit-account-name").val(ui.item.label);
            }
            return false;
        },
        open: function () {
            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        }
    });
}