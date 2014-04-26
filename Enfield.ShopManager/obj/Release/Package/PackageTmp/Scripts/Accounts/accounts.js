// @reference ../Common/shop.js

function InitializeAccounts(page) {
    InitializeSubMenu(); //in shop.js
    switch (page) {
        case "account-listing":
            BindSizeFilterEvent();
        case "account-listing":
            BindAccountDialogs();
        default:
            break;
    }
}
function BindSizeFilterEvent() {
    $("#Filter_Size").change(function (e) {
        $("#Size").val($(this).find(":selected").attr("value"));
        $("form").submit();
    });
}
function BindAccountDialogs() {
    $("#contact-dialog").dialog({
        title: "Edit Contact Information",
        autoOpen: false,
        modal: true,
        height: 270,
        width: 420
    });
}