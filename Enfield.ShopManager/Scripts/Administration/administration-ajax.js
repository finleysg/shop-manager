/******************************************************************************************************
AJAX POST
*******************************************************************************************************/
function PostUpdateInvoicePaidStatus(cb) {
    var invoice = cb.attr("data-id");
    if (invoice == null || invoice == undefined) {
        invoice = $("#Id").val();
    }
    $.ajax({
        type: "POST",
        dataType: "json",
        url: cb.attr("data-url"),
        data: {
            id: invoice,
            isPaid: cb.is(':checked')
        },
        error: function (XMLHttpRequest, status, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        },
        success: function (data, status) {
            ClearError();
        }
    });
}
function PostUpdateInvoicePurchaseOrder(tb) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: tb.attr("data-url"),
        data: {
            id: $("#Id").val(),
            purchaseOrder: tb.val()
        },
        error: function (XMLHttpRequest, status, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        },
        success: function (data, status) {
            ClearError();
        }
    });
}

function PostEditServiceRate(url, data) {
    $.ajax({
        type: "POST",
        dataType: "html",
        url: url,
        data: data,
        success: function (data, textStatus) {
            $('#service-detail').replaceWith(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}

function PostEditLaborRate(url, data) {
    $.ajax({
        type: "POST",
        dataType: "html",
        url: url,
        data: data,
        success: function (data, textStatus) {
            $('#labor-detail').replaceWith(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}

function PostToServicesAndLabor(url, data) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: url,
        data: data,
        success: function (data, textStatus) {
            location.reload();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}
