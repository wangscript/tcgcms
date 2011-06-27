/// <reference path="../../../manage/js/jquery-1.3.1-vsdoc.js" />

$(document).ready(function () {

    leftShowBind("ranking_panel");

    leftShowBind("ranking_panel2");
    //dts.eq(0).removeClass("selected").addClass("selected");

    //ranking_panel.find("dd").eq(0).css({ "display": "" });
});


function leftShowBind(objid) {
    var ranking_panel = $("#"+ objid);

    var dts = ranking_panel.find("dt");
    var dds = ranking_panel.find("dd");

    dts.each(function (i) {

        var dt = $(dts[i]);
        dt.mousemove(function () {

            dts.each(function (n) {
                var dt1 = $(dts[n]);
                dt1.removeClass("selected");
                $("#" + objid + "_newpdd_" + dt1.attr("id").replace(objid + "_newpdt_", "")).css({ "display": "none" });
            });

            var obj = $(this);
            obj.removeClass("selected").addClass("selected");
            $("#" + objid + "_newpdd_" + obj.attr("id").replace(objid + "_newpdt_", "")).css({ "display": "" });


        });

    });

}