// @reference shop-floor.js

/******************************************************************************************************
    AJAX POST
*******************************************************************************************************/
function PostUpdateInvoice(input) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: $("#update-invoice-url").val(),
        data: {
            invoiceId: $("#Id").val(),
            field: input.attr("id"),
            value: input.val()
        },
        error: function (XMLHttpRequest, status, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        },
        success: function (data, status) {
            ClearError();
        }
    });
}
function PostUpdateAccount(input) {
    if (input.val() == 0 || input.val() == "0") {
        alert("For some reason the account number you selected is 0. We can't post that to the server. If this keeps happening, send a note to your programmer with detail about what invoice you are on and the clicks you have made.");
        return;
    }
    $.ajax({
        type: "POST",
        dataType: "json",
        url: input.data("url"),
        data: {
            invoiceId: $("#Id").val(),
            accountId: input.val()
        },
        error: function (XMLHttpRequest, status, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        },
        success: function (data, status) {
            ClearError();
        }
    });
}
function PostAddHistory(url, note) {
    $.ajax({
        type: "POST",
        dataType: "html",
        url: url,
        data: {
            invoiceId: $("#Id").val(),
            note: $("#edit-history-note").val()
        },
        success: function (data, textStatus) {
            $('#history-detail').replaceWith(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}
function PostAddService() {
    $.ajax({
        type: "POST",
        dataType: "html",
        url: $("#add-service-url").val(),
        data: {
            invoiceId: $("#Id").val(),
            serviceTypeId: $("#new-servicetype-id").val(),
            rate: $("#new-service-rate").val()
        },
        success: function (data, textStatus) {
            //$('#service-detail').replaceWith(data);
            //BindShopFloorDragTargets();
            //OpenEditServiceDialog(GetLastServiceAdded());
            location.reload();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}

function PostEditServiceRate() {
    $.ajax({
        type: "POST",
        dataType: "html",
        url: $("#edit-service-url").val(),
        data: {
            invoiceId: $("#Id").val(),
            serviceId: $("#edit-service-id").val(),
            rate: $("#edit-service-rate").val()
        },
        success: function (data, textStatus) {
            $('#service-detail').replaceWith(data);
            BindShopFloorDragTargets();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}

function PostDeleteService(serviceId) {
    $.ajax({
        type: "POST",
        dataType: "html",
        url: $("#delete-service-url").val(),
        data: {
            invoiceId: $("#Id").val(),
            serviceId: serviceId
        },
        success: function (data, textStatus) {
            $('#service-detail').replaceWith(data);
            BindShopFloorDragTargets();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}

function PostAddLabor(laborTypeId) {
    var rate = $("#new-labor-rate").val();
    if (rate == null || !isNumber(rate) || rate == 0) {
        alert("You must enter an initial labor rate");
        return;
    }
    $.ajax({
        type: "POST",
        dataType: "html",
        url: $("#add-labor-url").val(),
        data: {
            invoiceId: $("#Id").val(),
            laborTypeId: $("#new-labortype-id").val(),
            rate: $("#new-labor-rate").val()
        },
        success: function (data, textStatus) {
            //$('#labor-detail').replaceWith(data);
            //BindShopFloorDragTargets();
            location.reload();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}

function PostAddEmployeeToLabor(tr, div) {
    $.ajax({
        type: "POST",
        dataType: "html",
        url: $("#add-employee-url").val(),
        data: {
            invoiceId: $("#Id").val(),
            laborId: tr.attr("data-id"),
            employeeId: div.attr("data-id")
        },
        success: function (data, textStatus) {
            $('#labor-detail').replaceWith(data);
            BindShopFloorDragTargets();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}

function PostEditLaborRate() {
    $.ajax({
        type: "POST",
        dataType: "html",
        url: $("#edit-labor-url").val(),
        data: {
            invoiceId: $("#Id").val(),
            laborId: $("#edit-labor-id").val(),
            rate: $("#edit-labor-rate").val()
        },
        success: function (data, textStatus) {
            $('#labor-detail').replaceWith(data);
            BindShopFloorDragTargets();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}

function PostDeleteLabor(laborId) {
    $.ajax({
        type: "POST",
        dataType: "html",
        url: $("#delete-labor-url").val(),
        data: {
            invoiceId: $("#Id").val(),
            laborId: laborId
        },
        success: function (data, textStatus) {
            $('#labor-detail').replaceWith(data);
            BindShopFloorDragTargets();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}

function PostInvoiceComplete(url) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: url,
        data: {
            invoiceId: $("#Id").val()
        },
        success: function (data, textStatus) {
            window.location = $("#get-default-invoice-url").val();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}

function PostInvoiceDelete(url) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: url,
        data: {
            invoiceId: $("#Id").val()
        },
        success: function (data, textStatus) {
            window.location = $("#get-default-invoice-url").val();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}

function PostInvoiceRecall(url) {
    $.ajax({
        type: "POST",
        dataType: "json",
        url: url,
        data: {
            invoiceId: $("#Id").val()
        },
        success: function (data, textStatus) {
            window.location = $("#get-default-invoice-url").val();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}

function PostSignInCredentials(url) {
    $.ajax({
        type: "POST",
        dataType: "html",
        url: url,
        data: {
            id: $("#sign-in-name").val()
        },
        success: function (data, textStatus) {
            $('#employee-tab').html(data);
            BindShopFloorDraggables();
            BindShopFloorDragTargets();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}

function PostSignOut(url) {
    $.ajax({
        type: "POST",
        dataType: "html",
        url: url,
        data: {
            id: $("#sign-out-name").val()
        },
        success: function (data, textStatus) {
            $('#employee-tab').html(data);
            BindShopFloorDraggables();
            BindShopFloorDragTargets();
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowError(XMLHttpRequest.responseText);
        }
    });
}