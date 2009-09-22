
var FileSite="http://files.xzdsw.com/";

function UploadFile(){
	this.Screen={w:window.screen.availWidth,h:window.screen.availHeight};
	this.Default={w:0,h:0};
	this.ClassId=0;
	this.CallBack="";
	this.MaxNum=1;
	
	this.Start=function(){
		this.Screen={w:window.screen.availWidth,h:window.screen.availHeight};
		this.uploadfilebg=$("uploadfilebg");
		if(this.uploadfilebg==null)document.body.innerHTML +=this.Html;
		this.uploaddiv=$("uploaddiv");
		this.uploadfilebg=$("uploadfilebg");
		this.uploadfile=$("uploadfile");
		this.init();
	}
	
	this.init=function(){
		this.Screen.w=this.Screen.w+this.Default.w;
		this.Screen.h=this.Screen.h+this.Default.h;
		this.uploadfilebg.style.width=this.Screen.w+"px";
		this.uploadfilebg.style.height=this.Screen.h+"px";
		this.uploaddiv.style.width="554px";
		this.uploaddiv.style.height="334px";
		this.uploaddiv.style.top=(this.Screen.h-this.uploaddiv.offsetHeight)/2+"px";
		this.uploaddiv.style.left=(this.Screen.w-this.uploaddiv.offsetWidth)/2+"px";
		this.uploadfile.style.width="100%";
		this.uploadfile.style.height="100%";
		this.uploadfile.src=FileSite+"upload/uploadfile.aspx?classid="+this.ClassId+"&CallBack="+this.CallBack+"&MaxNum="+this.MaxNum;
	}
	
	this.Html="<div class=\"uploadfilebg\" id=\"uploadfilebg\"></div><div class=\"uploaddiv\" id=\"uploaddiv\"><iframe id=\"uploadfile\" width=\"0\" height=\"0\" frameborder=\"0\"></iframe></div>";
}