/// <reference path="../../../manage/js/jquery-1.3.1-vsdoc.js" />

//头部二级菜单显示JS
function showMenu(obj,num) {
    if (myMenu == null) return;

    $(myMenu).each(function (i) {
        var m = $(myMenu).eq(i);
        if (num == i + 1) {
            m.show();
            m.css({ "left": $(obj).css("left") });
        } else {
            m.hide();
        }
    });
}

function hideMenu() {
    if (myMenu == null) return;
    $(myMenu).each(function (i) {
        $(myMenu[i]).animate({ opacity: 'hide' }, 300);
    });
}

$(document).bind('click', function (e) {
    hideMenu();
});

var myMenu = null;
$(document).ready(function () {
    myMenu = $(".subnav");
}); 