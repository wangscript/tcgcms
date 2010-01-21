/// <reference path="jquery-1.3.1-vsdoc.js" />

$(window).ready(function() {
    var tree_skin = $("#tree_skin");
    var tree_temp_1 = $("#tree_temp_1");

    var top1 = parseInt(tree_skin.offset().top);
    var top2 = parseInt(tree_temp_1.offset().top);

    var left1 = parseInt(tree_skin.offset().left);
    var left2 = parseInt(tree_temp_1.offset().left);



    var width1 = parseInt(tree_skin.outerWidth());   //第一个的宽度
    var width2 = parseInt(tree_temp_1.outerWidth()); //第二个的宽度

    var height1 = parseInt(tree_skin.outerHeight());
    var height2 = parseInt(tree_temp_1.outerHeight());

    var tleft1 = left1 + width1 / 2;
    var tleft2 = left2 + width2 / 2;

    var ttop1 = top1 + height1;
    var ttop2 = top2;

    var left = (tleft1 > tleft2) ? tleft2 : tleft1;
    var width = Math.abs(tleft2 - tleft1);

    var top = (top1 > top2) ? top2 + height2 : top1 + height1;
    var height = Math.abs(top1 - top2) - height1;

    alert(height);
    alert(top);
   
    var style1 = "top:" + top + "px;left:" + left + "px;width:" + width + "px;height:" + parseInt(height / 2) + "px;border-bottom: solid red 1px;border-right: solid red 1px;";
    var style2 = "top:" + (parseInt(top) + (height - parseInt(height / 2))) + "px;left:" + left + "px;width:" + width + "px;height:" + parseInt(height - parseInt(height / 2)) + "px;border-left: solid red 1px;";


    var line = $("<div class='line' style='" + style1 + "'></div><div class='line' style='" + style2 + "'></div>");
    $(document.body).append(line);
});