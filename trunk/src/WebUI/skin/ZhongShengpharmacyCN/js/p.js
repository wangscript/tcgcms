/// <reference path="../../../manage/js/jquery-1.3.1-vsdoc.js" />


var LocString = String(window.document.location.href);

function getQueryStr(str) {
    var rs = new RegExp("(^|)" + str + "=([^\&]*)(\&|$)", "gi").exec(LocString), tmp;

    if (tmp = rs) {
        return tmp[2];
    }

    // parameter cannot be found    
    return "";
}

var page = parseInt(getQueryStr("page"));
$(document).ready(function () {
    page = parseInt(getQueryStr("page"));
    if (!page) page = 0;

    $.get("/interface/aspx/resources.aspx?w=getresourcelist&iClassId=839985c8-fe76-4347-ab34-86b0db07c4fc&keywords=" + getQueryStr("keywords") + "&skinid=ZhongShengpharmacyCN&temp=" + new Date().toString(),
        { Action: "get" },
        function (data, textStatus) {
            eval(data);

            if (bkeywords) {
                $("#title1").html("关键字:"+ bkeywords);
                $("#title2").html("首页 > 产品中心 > 关键字:" + bkeywords);
            } else {
                $("#title1").html("最新产品");
                $("#title2").html("首页 > 产品中心 > 最新产品");
            }

            if (_Resources != null && _Resources.length > 0) {
                $("#ContentBox").html("");
                for (var i = 0; i < _Resources.length; i++) {
                    var p = _Resources[i];
                    var txt = "<div class=\"OneProduct\">"
		                + "<div class=\"leftImg\"><img src=\"" + p.vcSmallImg + "\" alt=\"\" width=\"227\" height=\"154\" />"
                        + "<a href=\"" + p.vcSmallImg + "\">(点击查看放大图)</a></div>"
		                + "<div class=\"RightText\"><span class=\"BlueText p_title\">药品名称：</span> " + p.vcTitle + "<br /> "
                        + "<div id=\"p_" + p.Id + "\"></div>"
                        + "<span class=\"BlueText p_title\">&nbsp;</span><a href=\"" + p.vcFilePath + "\"><img border=\"0\" src=\"/skin/ZhongShengpharmacyCN/images/pic_more.jpg\" alt=\"了解更多\" /></a></div>"
                        + "<div><img src=\"/skin/ZhongShengpharmacyCN/images/bk_product.jpg\" alt=\"\" /></div>";

                    $(txt).appendTo($("#ContentBox"));
                    $.get("/interface/aspx/resources.aspx?w=getresourceproperties&nid=" + p.Id + "&temp=" + new Date().toString(),
                        { Action: "get" },
                        function (data, textStatus) {
                            eval(data);
                            var txt1 = "";
                            if (_ResourceProperties != null && _ResourceProperties.length > 0) {
                                for (var n = 0; n < _ResourceProperties.length; n++) {
                                    var o = _ResourceProperties[n];
                                    if (n < 5 && o.PropertieName != "社保类") {
                                        txt1 += "<span class=\"p_title BlueText\">" + o.PropertieName + "：</span>" + o.PropertieValue + "<br />";
                                    } else if (o.PropertieName == "社保类") {
                                        txt1 += "<span class=\"p_title BlueText\">&nbsp;&nbsp;</span><font color=\"red\">☆</font>" + o.PropertieValue + "<font color=\"red\">☆</font><br />"
                                    }
                                }
                            }

                            $(txt1).appendTo($("#p_" + o.ResourceId));
                        });

                }
                var pager = $("#pager");
                $("<a href=\"/html/d/zsyy/newproduct.html\">首页<a> ").appendTo(pager);

                var temp = "/html/d/zsyy/newproduct.html?page={0}";
                if (page == 0 || page == 1) {
                    $("<a href=\"/html/d/zsyy/newproduct.html\">上一页<a> ").appendTo(pager);
                } else {
                    $("<a href=\"" + temp.replace("{0}", (page - 1)) + "\">上一页<a> ").appendTo(pager);
                }

                if (page >= pageCount) {
                    $("<a href=\"" + temp.replace("{0}", pageCount) + "\">下一页<a> ").appendTo(pager);
                } else {
                    $("<a href=\"" + temp.replace("{0}", (page + 1)) + "\">下一页<a> ").appendTo(pager);
                }

                $("<a href=\"" + temp.replace("{0}", pageCount) + "\">末页</a> ").appendTo(pager);
                $("<a>第" + pageCount + "页/共" + pageCount + "页 共" + count + "条</a>").appendTo(pager);


                //首页 上一页 下一页 末页 第{1}页/共1页 共1条
            } else {
                $("#ContentBox").html("未找到您要查找的产品！");
            }

        });
});


        

//<div class="OneProduct">
//		  <div class="leftImg"><img src="$item2_vcSmallImg$" alt="" width="227" height="154" /></div>
//		  <div class="RightText"><span class="BlueText">药品名称：</span> $item2_vcTitle$<br /> 
//				<ResourcePropertiesList num="5" hide="社保类">
//					<span class="BlueText">$PropertieName$：</span>$PropertieValue$<br />
//				</ResourcePropertiesList>
//                                <RPItem><span class="BlueText">&nbsp;&nbsp;</span><font color="red">☆</font>$社保类$<font color="red">☆</font><br /></RPItem>
//				<a href="$item2_vcFilePath$"><img border="0" src="_$SkinPath$_/images/pic_more.jpg" alt="了解更多" /></a></div>
//        <div><img src="images/bk_product.jpg" alt="" /></div>
//		</div>