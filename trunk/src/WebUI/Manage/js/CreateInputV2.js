/// <reference path="jquery-1.3.1-vsdoc.js" />

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
	    this.obj = $(this.obj);
	    if (this.obj == null || this.obj.length == 0) return;
	    CreateInputobj = this.obj;
	    this.pos = this.getAbsolutePosition(this.obj.get(0));
	    this.input = $("#" + this.Id);
	    if (this.input == null || this.input.length == 0) {
	        this.input = $("<INPUT id='" + this.Id + "' style='top:" + (this.pos.y + 2) + "px;left:" + (this.pos.x + 4) + "px;width:" + (parseInt(this.obj.outerWidth()) - 8) + "px;' class='"
	        + this.inputClassName + "' name='" + this.Id + "' type='text' />");
	        $(this.fobj).append(this.input);
	    }

	    this.input.blur(function() {
	        eval(bluraction);
	    });

	    this.input.show();
	    this.input.val(CreateInputobj.text());
	    this.input.focus();
	    this.ImgInit();
	}
	
	this.ImgInit = function() {
	    if ($("#CloseImg") != null && $("#CloseImg").length != 0) {
	        this.img = $("#CloseImg");
	    } else {
	    var inum = (document.all) ? 17 : 19;
	        this.img = $("<img id='CloseImg' style='top:" + (this.pos.y + 3) + "px;left:" + (this.pos.x + (parseInt($(this.obj).outerWidth()) - inum)) + "px;' class='inputcloseimg' src='../images/close.gif' />");
	        $("body").append(this.img);
	    }

	    this.img = $(this.img);
	    this.img.click(function() {
	        eval(imgaction);
	    });

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