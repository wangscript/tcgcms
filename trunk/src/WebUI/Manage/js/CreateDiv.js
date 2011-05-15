
function CreateDiv(){
	this.set=1;
	this.setcount=0;
	this.msgstr="";
	this.Screen={w:window.screen.availWidth,h:window.screen.availHeight};
	this.Default={w:0,h:0};
	
	this.Start=function(){
		var val,ac=arguments.length,av=arguments;
		val=ac>0?av[0]:null;
		this.Divinit();
		this.create_num.value=0;
		this.createjdtPlacebg.style.width="0px";
		this.MsgDiv.innerHTML="";
		SetInnerText(this.Msg,"正在"+val+"...");
		SetInnerText(this.createtitle,"操作正在进行中...");
		this.msgstr=val;
		this.o.className="MsgDiv";
		this.o.style.left =(this.Screen.w-this.o.offsetWidth+this.Default.w) +"px";
		this.o.style.top =(this.Screen.h-this.o.offsetHeight+this.Default.h) +"px";
	}
	
	this.End=function(){
		SetInnerText(this.createtitle,"操作已经执行完成！");
		SetInnerText(this.Msg,this.msgstr+"已完成!");
	}
	
	this.SetSep=function(){
		var val,ac=arguments.length,av=arguments;
		val=ac>0?av[0]:null;
		SetInnerText(this.create_num,parseInt(((this.set)/this.setcount)*100));
		this.createjdtPlacebg.style.width=parseInt(((this.set)/this.setcount)*407)+"px";
		this.MsgDiv.innerHTML += val;
		this.MsgDiv.scrollTop = this.MsgDiv.scrollHeight;
		if(this.set==this.setcount)this.End();
		this.set++;
	}
	
	this.Divinit=function(){
		if(this.Msg==null)this.CreateDivIntoPage();
		this.createtitle=$("createtitle");
		this.Msg=$("Msg");
		this.create_num=$("create_num");
		this.createjdtPlacebg=$("createjdtPlacebg");
		this.MsgDiv=$("MsgDiv");
	}
	
	this.CreateDivIntoPage=function(){
		this.o = document.createElement("div");
		this.o.className="MsgDiv";
		this.o.setAttribute("id","CreateMsgPlace");
		var strHtml ="<div class=\"createclose\"><a onclick=\"$('CreateMsgPlace').className='MsgDiv hid';\"  href='javascript:GoTo();'></a></div><div class=\"createtitle\"><span id=\"createtitle\">操作正在进行中...</span></div><div class=\"createMsgbg\"><div class=\"createjdt\"><span style=\"margin-left:5px;\" id=\"Msg\"></span><span style=\"margin-left:15px;\">完成度:<span class=\"bold\" id=\"create_num\">0</span>%</span><div class=\"createjdtPlace\"><div class=\"createjdtPlacebg rbfcreate\" id=\"createjdtPlacebg\"></div></div></div><div class=\"createMsg\" id=\"MsgDiv\"></div><div class=\"createbottom\"></div></div>";
		this.o.innerHTML = strHtml
		document.body.appendChild(this.o);
	}
}