// @reference shop-floor.js

/******************************************************************************************************
    DRAG AND DROP
*******************************************************************************************************/
function BindShopFloorDraggables() {
    $("#service-tabs").tabs({ cookie: { expires: 1} });
    $("#service-tabs").removeClass("ui-corner-all");
    $("#service-tabs ul").removeClass("ui-corner-all");
    $(".service-type").draggable({
        helper: 'clone',
        appendTo: 'body',
        opacity: 0.5,
        zIndex: 9999
    });
    $(".labor-type").draggable({
        helper: 'clone',
        appendTo: 'body',
        opacity: 0.5,
        zIndex: 9999
    });
    $(".working-employee").draggable({
        helper: 'clone',
        appendTo: 'body',
        opacity: 0.5,
        zIndex: 9999
    });
}
function BindShopFloorDragTargets() {
    $("#service-detail").droppable({
        accept: ".service-type",
        activeClass: 'target-active',
        hoverClass: 'ui-state-hover',
        drop: function (ev, ui) { OpenNewServiceDialog(ui.draggable); }
    });
    $("#labor-detail").droppable({
        accept: ".labor-type",
        activeClass: 'target-active',
        hoverClass: 'ui-state-hover',
        drop: function (ev, ui) { OpenNewLaborDialog(ui.draggable); }
    });
    $(".labor-row").droppable({
        accept: ".working-employee",
        activeClass: 'target-active',
        hoverClass: 'ui-state-hover',
        drop: function (ev, ui) { PostAddEmployeeToLabor($(this), ui.draggable); }
    });
}
