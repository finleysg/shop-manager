// @reference ../Common/shop.js

function InitializeReporting() {
    InitializeSubMenu();
    BindForm();
    $("#select-all").bind("click", function (e) {
        $(":checkbox").each(function (i) {
            $(this).prop("checked", !this.checked);
        });
        if ($(this).prop("innerText") == "Clear All")
            $(this).prop("innerHTML", "<span class='ui-icon ui-icon-check\'></span>Select All");
        else
            $(this).prop("innerHTML", "<span class='ui-icon ui-icon-check\'></span>Clear All");
    });
}

function BindForm() {
    $("form").submit(function (e) {
        e.preventDefault();
        var dataString = $(this).serialize();
        $.ajax({
            dataType: "html",
            type: "POST",
            url: $(this).attr("action"),
            data: dataString,
            error: function (XMLHttpRequest, status, errorThrown) {
                ShowError(XMLHttpRequest.responseText);
            },
            success: function (data, status) {
                var url = eval(data);
                window.open(url);
            }
        });
    });
}
