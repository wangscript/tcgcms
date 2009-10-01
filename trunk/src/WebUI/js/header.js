//头部选项卡状态
$(document).ready(function() {
    var o = { onClass: "on" }
    var emus = $(".emu").eq(0).find("li");
    if (typeof (ClassId) != "undefined") {
        try {
            var indexNuN = 0;
            if (ClassId == 3) indexNuN = 1;
            if (ClassId == 5) indexNuN = 2;
            if (ClassId == 6) indexNuN = 3;
            if (ClassId == 7) indexNuN = 4;
            if (ClassId == 4) indexNuN = 5;
            if (ClassId == 9) indexNuN = 6;
            emus.eq(indexNuN).addClass(o.onClass).siblings().removeClass(o.onClass);
        } catch (err) { }
    } else {
        emus.eq(0).addClass(o.onClass).siblings().removeClass(o.onClass);
    }
});