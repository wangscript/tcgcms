function Utils(){
	
	//验证邮箱的正则表达
	this.IsMail = function(){
		var str,ac=arguments.length,av=arguments;
		if(!(str=ac>0?av[0]:null)) return false;
		var reg = /^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/; 
		return reg.test(str);
	}
	
	//根据正则判断是否符合规则
	this.IsRegex = function(){
		var str,pt,ac=arguments.length,av=arguments;
		if(!(str=ac>0?av[0]:null)) return false;
		if(!(pt=ac>1?av[1]:null)) return false;
		return pt.test(str);
	}
	
	//系统信息提示框
	this.SystemDo = function(){
		var sdo,dotext,callback,ac=arguments.length,av=arguments;
		if(!(sdo=ac>0?av[0]:null)) return false;
		if(!(dotext=ac>1?av[1]:null)) return false;
		callback=ac>2?av[2]:null;
		
		var mybody = $('body');
		if(mybody.length==0)return false;
		
		var obj = $("#systemdo");
		if(obj.length==0){
			obj = $("<div class=\"systemdo\" id=\"systemdo\"></div>");
			mybody.append(obj);
		}	
		
		//设置层的基本属性
		obj.html(dotext)
		var top =  parseInt(((window.screen.availHeight-obj.height())/2)*0.618);
		var left = (window.screen.availWidth-obj.width())/2;
		obj.css({top:(top+30)+"px",left:left+"px"});
		
		if(dotext.length<15){
			obj.css({fontWeight:"bold"});
		}else{
			obj.css({fontWeight:"100"});
		}
		switch(sdo)
		{
			case "do":
				obj.css({display:"none"});
				obj[0].className = "systemdo do";
				
				obj.animate({
				   top:top, opacity: 'show'
				}, 200,callback); 
				
			break;
			case "err":

				obj[0].className = "systemdo serr";
				obj.animate({top:top}, 1200,function(){
					obj.animate({
					   top:top+30, opacity: 'hide'
					}, 200,callback);
				});
				
			break;
			case "out":
			
				obj.animate({
				   top:top+30, opacity: 'hide'
				}, 200,callback);
				
			break;
			case "text":
				obj.html(dotext)
				obj.animate({top:top}, 1200,callback);
			break;
		}
		
		return true;
	}
}

var utils = new Utils();