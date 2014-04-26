function initializeMenu(section, page) {
    
    $("#main-menu>li").attr('state', 'unselected');
    $("#main-menu>li[title=" + section + "]").attr('state', 'selected');
    $("#sub-menu ul").css('display', 'none');
    $("#sub-menu ul[id=" + section + "-menu]").css('display', 'block');

    //delegate shop floor menu handling to shop scripts
    if (section != "shop-floor") {
        $("#sub-menu li").attr('state', 'unselected');
    }

    if (page == undefined && section != "shop-floor")
        $("#sub-menu ul[id=" + section + "-menu]>li:first").attr('state', 'selected');
    else
        $("#sub-menu li[title=" + page + "]").attr('state', 'selected');
}
