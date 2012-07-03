var DivMoves=new Array();

function DivMove(){
	this.oldwid={w:1,h:1};
	this.newwid={w:0,h:0};
	this.oldpos={w:0,h:0};
	this.newpos={w:0,h:0};
	this.obj=null;
	this.Mobj=null;
	this.Screen={w:window.screen.availWidth,h:window.screen.availHeight};
	this.DefaultPos={w:0,h:0};
	this.times=10;
	this.a=1;
	this.i=1;//记录加速度
	this.sound=null;
	this.bAction=false;//边框动画
	this.bw=8;//边框宽度*2
	this.n=1;//记录边框
	
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
	
	this.getPointInLine=function(){
		var pos={w:0,h:0};
		var tt=(this.i==1)?0:parseInt(this.Mobj.offsetTop);
		var tl=(this.i==1)?0:parseInt(this.Mobj.offsetLeft);
		var w=(tt+(parseInt(this.mtop/this.times))+this.i*this.a);
		var h=(tl+(parseInt(this.mleft/this.times))+this.i*this.a);
		pos.w=(w<=this.mtop)?w:this.mtop;
		pos.h=(h<=this.mleft)?h:this.mleft;
		return pos;
	}
	
	this.getDivoffSet=function(){
		var pos={w:0,h:0};
		var tt=(this.i==1)?0:parseInt(this.Mobj.offsetWidth);
		var tl=(this.i==1)?0:parseInt(this.Mobj.offsetHeight);
		var tw=tt + parseInt((this.newwid.w/this.times)+this.i*this.a);
		var th=tl + parseInt((this.newwid.h/this.times)+this.i*this.a);
		pos.w=(tw<=this.newwid.w)?tw:this.newwid.w;
		pos.h=(th<=this.newwid.h)?th:this.newwid.h;
		return pos;
	}
	
	this.getPointInLineForClose=function(){
		var pos={w:0,h:0};
		var tt=(this.i==1)?this.mtop:parseInt(this.Mobj.offsetTop);
		var tl=(this.i==1)?this.mleft:parseInt(this.Mobj.offsetLeft);
		var w=tt-(parseInt(this.mtop/this.times)+this.i*this.a);
		var h=tl-(parseInt(this.mleft/this.times)+this.i*this.a);
		pos.w=(w>=this.oldpos.w)?w:this.oldwid.w;
		pos.h=(h>=this.oldpos.h)?h:this.oldwid.h;
		return pos;
	}
	
	this.getDivoffSetForClose=function(){
		var pos={w:0,h:0};
		var tt=(this.i==1)?this.newwid.w:parseInt(this.Mobj.offsetWidth);
		var tl=(this.i==1)?this.newwid.h:parseInt(this.Mobj.offsetHeight);
		var tw=tt - parseInt((this.newwid.w/this.times)+this.i*this.a);
		var th=tl - parseInt((this.newwid.h/this.times)+this.i*this.a);
		pos.w=(tw>=this.oldwid.w)?tw:this.oldwid.w;
		pos.h=(th>=this.oldwid.h)?th:this.oldwid.h;
		return pos;
	}
	
	this.Init=function(){
		var pos=this.getAbsolutePosition(this.obj)
		this.Mobj.style.width=this.oldwid.w+"px";
		this.Mobj.style.height=this.oldwid.h+"px";
		this.Mobj.style.top=pos.x+"px";
		this.Mobj.style.left=pos.y+"px";
		this.oldpos.w=pos.y;this.oldpos.h=pos.x;
	}
	
	this.MoveToSrceenM = function(){
		var callback,ac=arguments.length,av=arguments;
		if(!(callback=ac>0?av[0]:null)) return;
		if(this.Mobj==null)return;
		this.Init();
		this.mleft = parseInt(this.Screen.w)/2-parseInt(this.newwid.w)/2 -this.DefaultPos.w;
		this.mtop =  parseInt(this.Screen.h)/2-parseInt(this.newwid.h)/2 -this.DefaultPos.h;
		this.Mobj.style.borderColor="#55b5f8";
		this.Timer=setInterval("StartMove("+DivMoves.length+",'"+callback+"')",10); 
		//this.setSound();
	}
	
	this.setSound=function(){
	this.sound = $("#movedivsound");
		if(this.sound==null){
			this.sound=document.createElement("embed");
			this.sound.src=weisite+"wav/Windows Minimize.wav";
			this.sound.setAttribute("id","movedivsound");
			this.sound.hidden=true;
			this.sound.style.width="0px";
			this.sound.style.height="0px";
			document.body.appendChild(this.sound);
		}
	}
	
	this.CloseDiv=function(){
		var callback,ac=arguments.length,av=arguments;
		if(!(callback=ac>0?av[0]:null)) return;
		this.i=1;
		this.Timer=setInterval("CloseMove("+DivMoves.length+",'"+callback+"')",10);
	}
	
	CloseMove=function(){
		var index,callback,ac=arguments.length,av=arguments;
		if(!(index=ac>0?av[0]:null)) return;
		if(!(callback=ac>1?av[1]:null)) return;
		var mv=DivMoves[index-1];
		
		if(mv.times>mv.i){
			var pos = mv.getPointInLineForClose();
			var wpos = mv.getDivoffSetForClose();
			mv.Mobj.style.top=pos.w+"px";
			mv.Mobj.style.left=pos.h+"px";
			mv.Mobj.style.width=wpos.w+"px";
			mv.Mobj.style.height=wpos.h+"px";
		}else{
			clearInterval(mv.Timer);eval(callback);
		}
		if(mv.times>mv.i)mv.i++;
	}
	
	StartMove=function(){
		var index,callback,ac=arguments.length,av=arguments;
		if(!(index=ac>0?av[0]:null)) return;
		if(!(callback=ac>1?av[1]:null)) return;
		var mv=DivMoves[index-1];
		if(mv.times<=mv.i){
			if(mv.bAction){
				if(mv.n<(mv.times/2)){
					mv.Mobj.style.borderWidth=parseInt((mv.bw/mv.times)*mv.n)+"px";
					mv.Mobj.style.borderStyle="solid";
					mv.n++;
				}else{
					clearInterval(mv.Timer);
					eval(callback);
				}
			}else{
				clearInterval(mv.Timer);
				eval(callback);
			}
		}else{
			var pos = mv.getPointInLine();
			var wpos = mv.getDivoffSet();
			mv.Mobj.style.top=pos.w+"px";
			mv.Mobj.style.left=pos.h+"px";
			mv.Mobj.style.width=wpos.w+"px";
			mv.Mobj.style.height=wpos.h+"px";
		}
		if(mv.times>mv.i)mv.i++;
	}
}