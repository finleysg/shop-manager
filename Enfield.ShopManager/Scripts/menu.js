// We defer shop floor menu handling to shop scripts (no state)
function initializeMenu(section, page) {
    
    $("#main-menu>li").attr('state', 'unselected');
    $("#main-menu>li[title=" + section + "]").attr('state', 'selected');
    $("#sub-menu ul").css('display', 'none');
    $("#sub-menu ul[id=" + section + "-menu]").css('display', 'block');

    if (section != "shop-floor") {
        $("#sub-menu li").attr('state', 'unselected');
    }

    if (page == undefined && section != "shop-floor")
        $("#sub-menu ul[id=" + section + "-menu]>li:first").attr('state', 'selected');
    else
        $("#sub-menu li[title=" + page + "]").attr('state', 'selected');
}

function initializeMainMenuHandler() {
    $("#main-menu>li").click(function (e) {
        if ($(this).attr("state") != "selected") {
            gotoLocation($(this).attr("title").replace(/\-/g, ''));
        }
    });
}

function initializeSubMenuHandler(currentSection) {
    $("#sub-menu li").click(function (e) {
        if (currentSection != "shop-floor" && $(this).attr("state") != "selected") {
            gotoLocation(currentSection, $(this).attr("title").replace(/\-/g, ''));
        }
    });
}

function gotoLocation(section, page) {
    if (page == undefined) document.location.href = "../" + section + "/";
    else document.location.href = "../" + section + "/" + page;
}
