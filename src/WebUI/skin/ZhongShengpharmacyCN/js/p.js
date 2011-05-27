/// <reference path="../../../manage/js/jquery-1.3.1-vsdoc.js" />




$.get("/interface/aspx/resources.aspx?w=getresourcelist&iClassId=839985c8-fe76-4347-ab34-86b0db07c4fc&skinid=ZhongShengpharmacyCN&temp=" + new Date().toString(),
        { Action: "get" },
        function (data, textStatus) {
            eval(data);
            if (_Resources != null && _Resources.length > 0) {
                $("#ContentBox").html("");
                for (var i = 0; i < _Resources.length; i++) {
                    var p = _Resources[i];
                    var txt = "<div class=\"OneProduct\">"
		                + "<div class=\"leftImg\"><img src=\"" + p.vcSmallImg + "\" alt=\"\" width=\"227\" height=\"154\" /></div>"
		                + "<div class=\"RightText\"><span class=\"BlueText\">药品名称：</span> " + p.vcTitle + "<br /> ";

                    $.get("/interface/aspx/resources.aspx?w=getresourceproperties&nid=" + p.Id + "&temp=" + new Date().toString(),
                        { Action: "get" },
                        function (data, textStatus) {
                            eval(data);
                            if (_ResourceProperties != null && _ResourceProperties.length > 0) {
                                for (var n = 0; n < _ResourceProperties.length; n++) {
                                    var o = _ResourceProperties[n];
                                    if (n < 5 && o.PropertieName != "省保类") {
                                        txt += "<span class=\"BlueText\">" + o.PropertieName + "：</span>" + o.PropertieValue + "<br />";
                                    } else if (o.PropertieName == "省保类") {
                                        
                                    }
                                }
                            }

                            $(txt).appendTo($("#ContentBox"));
                        });

                }
            } else {
                $("#ContentBox").html("未找到您要查找的产品！");
            }

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