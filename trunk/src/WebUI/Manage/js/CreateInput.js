var bluraction="";
var imgaction="";
var CreateInputobj=null;
function CreateInput(){
	this.obj=null;
	this.Id="CreateInput";
	this.input=null;
	this.inputClassName="";
	this.img=null;
	this.pos=null;
	this.fobj=null;

	this.Create = function() {
	    if (this.obj == null) return;
	    CreateInputobj = this.obj;
	    this.pos = this.getAbsolutePosition(this.obj);
	    if ($(this.Id) != null) {
	        this.input = $(this.Id);
	    } else {
	        this.input = document.createElement("INPUT");
	    }
	    this.input.setAttribute("id", this.Id);
	    this.input.setAttribute("name", this.Id);
	    this.input.className = this.inputClassName;
	    this.input.style.top = (this.pos.y + 2) + "px";
	    this.input.style.left = (this.pos.x + 4) + "px";
	    this.input.style.width = (parseInt(this.obj.offsetWidth) - 8) + "px";
	    this.input.onblur = function() {
	        eval(bluraction);
	    }
	    this.input.value = "";
	    if ($(this.Id) == null) {
	        if (this.fobj == null) {
	            document.body.appendChild(this.input);
	        } else {
	            this.fobj.appendChild(this.input);
	        }
	    }
	    this.input.value = CreateInputobj.innerText;
	    this.input.focus();
	    this.ImgInit();
	}
	this.ImgInit=function(){
		if($("CloseImg")!=null){
			this.img=$("CloseImg");
		}else{
			this.img=document.createElement("IMG");
		}
		this.img.setAttribute("id","CloseImg");
		this.img.src=weisite+"images/close.gif";
		this.img.className="inputcloseimg";
		
		this.img.style.top=(this.pos.y+3)+"px";
		var inum=(document.all)?17:19;
		this.img.style.left=(this.pos.x+(parseInt(this.obj.offsetWidth)-inum))+"px";
		this.img.onclick=function(){
			eval(imgaction);
		}
		if($("CloseImg")==null)document.body.appendChild(this.img);
	}
	
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
}