function MenuDiv(){
	this.DivList=new Array();
	this.DivCount=0;
	this.DefZIndex=100;
	this.pos={x:0,y:0};
	this.HidAuto=true;
	this.IsMut=false;
	
	this.getAbsolutePosition = function (){
		var obj,ac=arguments.length,av=arguments;
		var position = {x:0,y:0};
		if(!(obj=ac>0?av[0]:null)) return false;
		while(obj!=null && obj!=document.body){
			position.x += obj.offsetLeft;
			position.y += obj.offsetTop;
			obj = obj.offsetParent
		 }
		return position;
	}
	
	this.IsInDivList = function(){
		var DivId,ac=arguments.length,av=arguments;
		DivId=ac>0?av[0]:null;
		if(DivId==null)return false;
		for(var i=0;i<this.DivList.length;i++){
			if(this.DivList[i]==DivId){
				return true;
			}
		}
		return false;
	}
	
	this.HidMenuItem=function(){
		var id,ac=arguments.length,av=arguments;
		id=ac>0?av[0]:null;
		if(id==null)return;
		var o=$(id);
		if(o!=null)o.className="topmenu hid";
	}
	
	this.HidMenuDiv=function(){
		for(var i=0;i<this.DivList.length;i++){
			$(this.DivList[i]).className="topmenu hid";
		}
	}
	
	this.CreadDiv=function(){
		var DivId,obj,Alist,width,sa,mtop,mleft,Position,ac=arguments.length,av=arguments;
		DivId=(av[0]==null)?"topmenu"+this.DivCount:av[0];
		obj=ac>1?av[1]:null;Alist=ac>2?av[2]:null;width=ac>3?av[3]:0;
		sa=ac>4?av[4]:null;
		mtop=ac>5?av[5]:0;mleft=ac>6?av[6]:0;
		Position=ac>7?av[7]:"bottom";
		var Div;
		if(this.IsInDivList(DivId)){
			Div = $(DivId);
		}else{
			Div = document.createElement("div");
			Div.setAttribute("id",DivId); 
			if(this.HidAuto){
				Div.onmouseover=function(){$(DivId).className="topmenu";};
				Div.onmouseout=function(){$(DivId).className="topmenu hid";}; 
			}
		}
		var DivTop=mtop;
		var DivLeft=mleft;
		if(obj!=null){
			var pos = this.getAbsolutePosition(obj);
			DivTop+=pos.y;
			DivLeft+=pos.x;
			switch(Position)
			{
				case "left" :
					DivLeft=DivLeft-obj.scrollWidth;
					break;
				case "right" :
					DivLeft=DivLeft+obj.scrollWidth;
					break;
				case "bottom":
					DivTop=DivTop+obj.scrollHeight;
					break;	
			}
		}
		var csstxt="top:"+DivTop+"px; left:"+DivLeft+"px;border:#81a4c2 solid 1px;"
			+"width:"+width+"px;background-color:#f3f8fc;"
			+" color:#333333; position:absolute;"
		Div.className="topmenu";
		if(!this.IsInDivList(DivId)){
			document.body.appendChild(Div);
			csstxt+="z-index:"+this.DefZIndex+";";this.DefZInde++;
			this.DivList[this.DivCount] = DivId;this.DivCount++;
		}
		Div.style.cssText = csstxt;
		var strText=this.GetDivInnderHtml(Alist,sa,width);
		Div.innerHTML=strText==""?Div.innerHTML:strText;
	}
	
	this.MutClick=function(){
		var obj,ac=arguments.length,av=arguments;
		obj=ac>0?av[0]:null;
		if(obj==null)return;
		if(obj.className=="menua"){
			obj.className="menua mut";
			obj.style.paddingLeft="18px";
			obj.style.width=(obj.offsetWidth-30)+"px";
		}else{
			obj.className="menua";
			obj.style.paddingLeft="6px";
			obj.style.width=(obj.offsetWidth +6)+"px";
		}
	}
	
	this.GetDivInnderHtml=function(){
		var Alist,sa,width,ac=arguments.length,av=arguments;
		Alist=ac>0?av[0]:null;
		sa=ac>1?av[1]:null;
		width=ac>2?av[2]:0;
		var Text = "";
		var cssText = "float:left; height:23px; margin-left:1px; margin-right:1px; margin-top:1px; line-height:23px; text-align:left; margin-bottom:1px;";
		if(Alist!=null){
			if(Alist.length!=0){
				for(var i=0;i<Alist.length;i++){
					var o=Alist[i];
					var classn="";
					var cssTexts="";
					if(this.IsMut){
						if(o.Sel){
							cssTexts = cssText + "padding-left:18px;";
							cssTexts += "width:"+(width-20)+"px;";
							classn="menua mut";
						}else{
							cssTexts = cssText + "padding-left:6px;";
							cssTexts += "width:"+(width-8)+"px;";
							classn="menua";
						}
					}else{
						if(o.Sel){
							cssTexts = cssText + "padding-left:18px;";
							cssTexts += "width:"+(width-20)+"px;";
							classn="menua msl";
						}else{
							cssTexts = cssText + "padding-left:6px;";
							cssTexts += "width:"+(width-8)+"px;";
							classn="menua";
						}
					}
					if(typeof(o.href)=="undefined")continue;
					Text+="<a href=\""+o.href+"\" onclick=\""+o.onclick+"\" class=\""+classn+"\" style=\""+cssTexts+"\" onmouseover=\""+o.onmouseover+"\" onmouseout=\""+o.onmouseout+"\">"+o.Text+"</a>";
				}
			}
		}
		if(sa!=null){
			if(typeof(sa.href)!="undefined"){
				Text+="<div style=\"margin:0px; padding:0px;border:0px;height:1px;background-color:#81a4c2; width:"+(width-2)+"px;\"></div>";
				Text+="<a href=\""+sa.href+"\" onclick=\""+sa.onclick+"\" class=\"menua\" style=\""+cssText+"\">"+sa.Text+"</a>";
				Text+="<div style=\"margin:0px; padding:0px;border:0px;height:1px;width:"+(width-2)+"px;\"></div>";
			}
		}
		return Text;
	}
}