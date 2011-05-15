//--------------
//Pager
function setPage(s, i)
{
	return s.replace('{p}', i.toString());
}
function ChangPager(s,obj){
	var url = s.replace('{p}', obj.value);
	window.location.href=url;
}
function pager(url, page, maxPage, total, per, countsIsVisible)
{
	var first		= "首页";
	var previous	= "上一页";
	var next		= "下一页";
	var last		= "末页";
	
	var s = "";
			  
	
	s +="<select class='pagesl rfl' onchange=\"ChangPager('"+url+"',this);\">"
	for (var i=1; i<=maxPage; i++)
	{
		if (page == i)
		{
			s += ("<option value='"+i+"' selected>" + i + " / " + maxPage + "</span> ");
		}else{
			s += ("<option value='"+i+"'>" + i + " / " + maxPage + "</span> ");
		}
	}
	s += "</select>";
	
	s += "<span class='info1 rfl'>[";
	if (page == 1)
	{
		s += (first + " | " + previous + " | ");
	}
	else
	{
		if (first != "") s += (" <a href='" + setPage(url, 1) + "'>" + first + "</a> |");
		s += (" <a href='" + setPage(url, (page-1)) + "'>" + previous + "</a> |");
	}

	var j = (page-5 < 1) ? 1 : page-5;
	var k = (page+5 > maxPage) ? maxPage : page+5;

	
	if (page == maxPage)
	{
		s += (" " + next + " | " + last);
	}
	else
	{
		s += (" <a href='" + setPage(url, page+1) + "'>" + next + "</a> |");
		if (last != "") s += (" <a href='" + setPage(url, maxPage) + "'>" + last + "</a>");
	}
	
	s +="]";
	s +="</span>";
	
	
	
	if (countsIsVisible)
	{
		s += "<span class='info1 rfl'>";
		s += ("总记录:<span class='bold'>" + total + "</span>　");
		s += ("每页:<span class='bold'>" + per + "</span>　");
		s += ("总页数:<span class='bold'>" + maxPage + "</span>");
		s += "</span>";
	}
	
	return s;
}
