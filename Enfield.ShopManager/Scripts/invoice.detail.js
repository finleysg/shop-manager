function UpdateInvoicePaidStatus() {
    $("#IsPaid").click(function (e) {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "UpdateInvoicePaidStatus",
            data: {
                id: $("#Id").val(),
                isPaid: $(this).is(':checked')
            },
            error: function (XMLHttpRequest, status, errorThrown) {
                ShowError(XMLHttpRequest.responseText);
            },
            success: function (data, status) {
                ClearError();
            }
        });
    });
}
function UpdateInvoicePurchaseOrder() {
    $("#PurchaseOrderNumber").change(function (e) {
        $.ajax({
            type: "POST",
            dataType: "json",
            url: "UpdateInvoicePurchaseOrder",
            data: {
                id: $("#Id").val(),
                purchaseOrder: $(this).val()
            },
            error: function (XMLHttpRequest, status, errorThrown) {
                ShowError(XMLHttpRequest.responseText);
            },
            success: function (data, status) {
                ClearError();
            }
        });
    });
}
function EditServiceRate() {
    var serviceDialog =
        $("#edit-service-dialog").dialog({
            title: "Change the service rate",
            autoOpen: false,
            modal: true,
            height: 210,
            width: 300,
            buttons: {
                "OK": function () {
                    ShowBusy();
                    $.ajax({
                        type: "POST",
                        dataType: "html",
                        url: "EditServiceRate",
                        data: {
                            invoiceId: $("#Id").val(),
                            serviceId: $("#service-id").val(),
                            rate: $("#service-rate").val()
                        },
                        success: function (data, textStatus) {
                            ClearBusy();
                            ClearError();
                            $('#service-detail').replaceWith(data);
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            ClearBusy();
                            ShowError(XMLHttpRequest.responseText);
                        }
                    });
                    $(this).dialog("close");
                },
                "Cancel": function () { $(this).dialog("close"); }
            }
        });
    $(".service-edit").livequery("click", function () {
        $("#service-id").val($(this).attr("id").split("-")[1]);
        $("#service-type-description").html($(this).parents("td").next().html());
        $("#service-rate").val($(this).parents("tr").find(".rate").html().replace("$", ""));
        serviceDialog.dialog("open");
    });
}
function EditLaborRate() {
    var laborDialog =
        $("#edit-labor-dialog").dialog({
            title: "Change the labor rate",
            autoOpen: false,
            modal: true,
            height: 210,
            width: 300,
            buttons: {
                "OK": function () {
                    ShowBusy();
                    $.ajax({
                        type: "POST",
                        dataType: "html",
                        url: "EditLaborRate",
                        data: {
                            invoiceId: $("#Id").val(),
                            laborId: $("#labor-id").val(),
                            rate: $("#labor-rate").val()
                        },
                        success: function (data, textStatus) {
                            ClearBusy();
                            ClearError();
                            $('#labor-detail').replaceWith(data);
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            ClearBusy();
                            ShowError(XMLHttpRequest.responseText);
                        }
                    });
                    $(this).dialog("close");
                },
                "Cancel": function () { $(this).dialog("close"); }
            }
        });
        $(".labor-edit").livequery("click", function () {
            $("#labor-id").val($(this).attr("id").split("-")[1]);
            $("#labor-type-description").html($(this).parents("td").next().html() + " - " + $(this).parents("td").next().next().html());
            $("#labor-rate").val($(this).parents("tr").find(".rate").html().replace("$", ""));
            laborDialog.dialog("open");
        });
}
function ShowError(text) {
    var errorDialog =
        $("#error-dialog").dialog({
            title: "Error Received From the Server",
            autoOpen: false,
            modal: true,
            height: 270,
            width: 420,
            buttons: {"Ok": function () { $(this).dialog("close"); }}
        });
    $(".error-detail").html("<p>" + text + "</p>");
    errorDialog.dialog("open");
}
function ClearError() {
    $(".error-detail").html("<p></p>");
}
function ShowBusy() {
}
function ClearBusy() {
}