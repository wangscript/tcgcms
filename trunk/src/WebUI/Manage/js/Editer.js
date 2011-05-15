
function Editer(){
	this.EditInput=null;
	this.width=0;
	this.height=0;
	
	this.getAbsolutePosition=function(){
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
	
	this.Start=function(){
		this.EditBgInit();//≥ı ºªØ±≥æ∞ 
		this.iframeInit();
	}
	
	this.iframeInit=function(){
		this.TCG_Edit=$("TCG_Edit");
		if(this.TCG_Edit==null){
			var o=document.createElement("IFRAME");
			o.setAttribute("name","TCG_Edit");
			o.setAttribute("id","TCG_Edit");
			o.className="TCG_Edit";
			o.frameborder="0";

			
			this.EditBg.appendChild(o);
			this.TCG_Edit=o;
		}
	}
	
	this.EditBgInit=function(){
		this.EditBg=$("EditBg");
		if(this.EditBg==null){
			var o=document.createElement("DIV");
			o.setAttribute("name","EditBg");
			o.setAttribute("id","EditBg");
			o.className="EditBg";
			var pos=this.getAbsolutePosition(this.EditInput);
			o.style.top=pos.y+"px";
			o.style.left=pos.x+"px";
			o.style.width=this.EditInput.offsetWidth+"px";
			o.style.height=this.EditInput.offsetHeight+"px";
			document.body.appendChild(o);
			this.EditBg=o;
		}
	}
}

var editer = new Editer();