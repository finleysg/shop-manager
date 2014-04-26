/******************************************************************************************************
DIALOG BINDING
*******************************************************************************************************/
function BindServiceAndLaborDialogs() {
    $("#add-service-dialog").dialog({
        title: "Add Service",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 215,
        width: 300
    });
    $("#add-labor-dialog").dialog({
        title: "Add Labor",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 215,
        width: 300
    });
    $("#add-type-dialog").dialog({
        title: "Add New Type",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 210,
        width: 300
    });
    $("#rename-type-dialog").dialog({
        title: "Rename Type",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 210,
        width: 300
    });
    $("#confirm-action-dialog").dialog({
        title: "Confirm",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 200,
        width: 300
    });
}

function BindInvoiceDialogs() {
    $("#edit-service-dialog").dialog({
        title: "Change Service Rate",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 210,
        width: 300
    });
    $("#edit-labor-dialog").dialog({
        title: "Change Labor Rate",
        modal: true,
        resizable: false,
        autoOpen: false,
        height: 210,
        width: 300
    });
}

/******************************************************************************************************
DIALOG OPEN HANDLERS
*******************************************************************************************************/
function OpenEditServiceDialog(tr) {
    $("#edit-servicetype-description").html(tr.find(".description").html());
    $("#edit-service-rate").val(tr.find(".rate").html().replace("$", ""));
    $("#edit-service-dialog").dialog("option", "buttons", {
        "OK": function () {
            PostEditServiceRate(
                $(tr).attr("data-edit-url"),
                {
                    invoiceId: $("#Id").val(),
                    serviceId: $(tr).attr("data-id"),
                    rate: $("#edit-service-rate").val()
                }
            );
            $(this).dialog("close");
        },
        "Cancel": function () { $(this).dialog("close"); }
    });
    $("#edit-service-dialog").dialog("open");
}

function OpenEditLaborDialog(tr) {
    var laborType = tr.find(".description").html();
    var employee = tr.find(".employee").html();
    $("#edit-labortype-description").html(laborType + " - " + employee);
    $("#edit-labor-rate").val(tr.find(".rate").html().replace("$", ""));
    $("#edit-labor-dialog").dialog("option", "buttons", {
        "OK": function () {
            PostEditLaborRate(
                $(tr).attr("data-edit-url"),
                {
                    invoiceId: $("#Id").val(),
                    laborId: $(tr).attr("data-id"),
                    rate: $("#edit-labor-rate").val()
                }
            );
            $(this).dialog("close");
        },
        "Cancel": function () { $(this).dialog("close"); }
    });
    $("#edit-labor-dialog").dialog("open");
}

function OpenCreateServiceDialog(btn) {
    $("#new-type-name").val("");
    $("#new-type-name-label").html("New service type:");
    $("#add-type-dialog").dialog("option", "buttons", {
        "OK": function () {
            PostToServicesAndLabor(
                $(btn).attr("data-url"),
                {
                    description: $("#new-type-name").val()
                }
            );
            $(this).dialog("close");
        },
        "Cancel": function () { $(this).dialog("close"); }
    });
    $("#add-type-prompt").html("Create a new service type.");
    $("#add-type-dialog").dialog("open");
}

function OpenCreateLaborDialog(btn) {
    $("#new-type-name").val("");
    $("#new-type-name-label").html("New labor type:");
    $("#add-type-dialog").dialog("option", "buttons", {
        "OK": function () {
            PostToServicesAndLabor(
                $(btn).attr("data-url"),
                {
                    description: $("#new-type-name").val()
                }
            );
            $(this).dialog("close");
        },
        "Cancel": function () { $(this).dialog("close"); }
    });
    $("#add-type-prompt").html("Create a new labor type.");
    $("#add-type-dialog").dialog("open");
}

function OpenAddServiceDialog(li) {
    $("#add-service-dialog").dialog("option", "buttons", {
        "OK": function () {
            PostToServicesAndLabor(
                $(li).attr("data-add-service-url"),
                {
                    accountTypeId: $(li).attr("data-id"),
                    serviceTypeId: $("#add-service-type-id").val()
                }
            );
            $(this).dialog("close");
        },
        "Cancel": function () { $(this).dialog("close"); }
    });
    $("#add-service-prompt").html("Select a service to associate with the " + $(li).html() + " account type");
    $("#add-service-dialog").dialog("open");
}

function OpenAddLaborDialog(li) {
    $("#add-labor-dialog").dialog("option", "buttons", {
        "OK": function () {
            PostToServicesAndLabor(
                $(li).attr("data-add-labor-url"),
                {
                    accountTypeId: $(li).attr("data-parent-id"),
                    accountTypeServiceId: $(li).attr("data-id"),
                    laborTypeId: $("#add-labor-type-id").val()
                }
            );
            $(this).dialog("close");
        },
        "Cancel": function () { $(this).dialog("close"); }
    });
    $("#add-labor-prompt").html("Select a labor type to associate with the " + $(li).html() + " service");
    $("#add-labor-dialog").dialog("open");
}

function OpenRemoveServiceDialog(li) {
    $("p.confirm").html("Are you sure we should remove the " + $(li).html() + " service from this account type?");
    $("#confirm-action-dialog").dialog("option", "buttons", {
        "Ok": function () {
            PostToServicesAndLabor(
                $(li).attr("data-remove-url"),
                {
                    accountTypeId: $(li).attr("data-parent-id"),
                    accountTypeServiceId: $(li).attr("data-id")
                }
            );
            $(this).dialog("close");
        },
        "Cancel": function () { $(this).dialog("close"); }
    });
    $("#confirm-action-dialog").dialog("open");
}

function OpenUpdateServiceTypeDialog(li) {
    $("#type-name").val($(li).html());
    $("#type-name-label").html("New service name:");
    $("#rename-type-dialog").dialog("option", "buttons", {
        "OK": function () {
            PostToServicesAndLabor(
                $(li).attr("data-edit-url"),
                {
                    id: $(li).attr("data-type-id"),
                    description: $("#type-name").val()
                }
            );
            $(this).dialog("close");
        },
        "Cancel": function () { $(this).dialog("close"); }
    });
    $("#rename-type-prompt").html("Change the name of " + $(li).html());
    $("#rename-type-dialog").dialog("open");
}

function OpenRemoveLaborDialog(li) {
    $("p.confirm").html("Are you sure we should remove the " + $(li).html() + " labor from this service?");
    $("#confirm-action-dialog").dialog("option", "buttons", {
        "Ok": function () {
            PostToServicesAndLabor(
                $(li).attr("data-remove-url"),
                {
                    accountTypeid: $(li).attr("data-toplevel-id"),
                    accountTypeServiceId: $(li).attr("data-parent-id"),
                    accountTypeLaborId: $(li).attr("data-id")
                }
            );
            $(this).dialog("close");
        },
        "Cancel": function () { $(this).dialog("close"); }
    });
    $("#confirm-action-dialog").dialog("open");
}

function OpenUpdateLaborTypeDialog(li) {
    $("#type-name").val($(li).html());
    $("#type-name-label").html("New labor name:");
    $("#rename-type-dialog").dialog("option", "buttons", {
        "OK": function () {
            PostToServicesAndLabor(
                $(li).attr("data-edit-url"),
                {
                    id: $(li).attr("data-type-id"),
                    description: $("#type-name").val()
                }
            );
            $(this).dialog("close");
        },
        "Cancel": function () { $(this).dialog("close"); }
    });
    $("#rename-type-prompt").html("Change the name of " + $(li).html());
    $("#rename-type-dialog").dialog("open");
}