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
		var left = parseInt((window.screen.availWidth-obj.width())/2);
		obj.css({top:(top+30)+"px",left:left+"px"});
		
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
	
	this.ConvenientLogin = function(){
		var action,ac=arguments.length,av=arguments;
		if(!(action=ac>0?av[0]:null)) return false;
		
		var loginDiv = $("#loginDiv");
		if(loginDiv.length==0){
			var newsplas = $(".news");
			if(newsplas.length==0)return false;
			newsplas.eq(0).append($(this.convenientloginText));
			loginDiv = $("#loginDiv");
		}
		
		var DivBg = $("#DivBg");
		var top =  parseInt(((window.screen.availHeight-loginDiv.height())/2)*0.618);
		var left = parseInt((window.screen.availWidth-loginDiv.width())/2);
		loginDiv.css({top:top+"px",left:left+"px",display:"none"});
		
		DivBg.css({width:window.screen.availWidth,height:window.screen.availHeight,display:"none"});
		

		switch(action)
		{
			case "show":
				DivBg.animate({opacity: "show"}, 200,function(){
					loginDiv.animate({top:top+"px",left:left+"px",opacity: 'show'},300);	
				});
				try{RegFormInit();LoginFormInit();}catch(err){}
			break;
			case "hide":
				loginDiv.animate({ opacity: "hide" }, 300,function(){
					DivBg.animate({opacity: 'hide'},300);	
				});
			break;
		}
	}
	
	this.convenientloginText = "<div class=\"DivBg\" id=\"DivBg\"></div><div class=\"loginDiv\" id=\"loginDiv\">"
    	+"<div class=\"login\">"
        +"<div class=\"title\"><a>用户登陆</a></div>"
		+"<form id=\"LoginFrom\" method=\"post\">"
        +"<div class=\"Registerinfo3\">"
        	+"<div class=\"fl info3\"></div>"
           	+"<div class=\"fl info3\">注册用户请从这里登陆</div>"
            +"<div class=\"fl info3\"></div>"
            +"<div class=\"fl text3\">用户名：</div>"
            +"<div class=\"fl input3\"><input id=\"UserName\"  name=\"UserName\" class=\"input\" /></div>"
            +"<div class=\"fl info3\" id=\"LoginMsg\"></div>"
            
            +"<div class=\"fl text3\">密码：</div>"
            +"<div class=\"fl input3\"><input id=\"UserPassWord\"  class=\"input\"  type=\"password\" maxlength=\"16\" value=\"\" name=\"UserPassWord\" /></div>"
            +"<div class=\"fl text3\">&nbsp;</div>"
            +"<div class=\"fl input3\">忘记密码？</div>"   
            +"<div class=\"fl text3\">&nbsp;</div>"
            +"<div class=\"fl input3\"><input type=\"submit\" class=\"Btn\" value=\"提 交\" /></div>"
            +"<div class=\"fl info3\"></div>"
        +"</div>"
		+"</form>"
      +"</div>"
    
    	+"<div class=\"Register\">"
		+"<form id=\"Register\" method=\"post\">"
        +"<div class=\"title\"><a>用户注册</a></div>"
        +"<div class=\"Registerinfo2\">"
            +"<div class=\"fl text2\">请填写您的Email地址：</div>"
            +"<div class=\"fl input2\"><input id=\"Email\"  name=\"Email\" class=\"input\" /></div>"
            +"<div class=\"fl info2\" id=\"EmailMsg\">请填写有效的   Email地址作为用户名，我们向这个地址发送您的帐户信息、订单通知等。</div>"    
            +"<div class=\"fl text2\">请设定密码：</div>"
            +"<div class=\"fl input2\"><input id=\"PassWord\"  class=\"input\"  type=\"password\" maxlength=\"16\" value=\"\" name=\"PassWord\" /></div>"
            +"<div class=\"fl info2\" id=\"PassWordMsg\">密码请设为6-16位字母或数字。</div>"        
            +"<div class=\"fl text2\">请再次输入设定密码：</div>"
            +"<div class=\"fl input2\"><input id=\"RePassWord\"  class=\"input\"  type=\"PassWord\" maxlength=\"16\" value=\"\" name=\"RePassWord\" /></div>"
            +"<div class=\"fl info2\" id=\"RePassWordMsg\"></div>"
            +"<div class=\"fl text2\">请输入验证码：</div>"
            +"<div class=\"fl input2\"><input id=\"Validate_Code\" name=\"Validate_Code\" class=\"input fl\" style=\"width:80px;\" /><img src=\"/ValidCode.aspx?height=25&temp=\" class=\"fl maginleft5\" title=\"点击更换新的验证码\" align=\"absmiddle\" onclick=\"this.src=this.src + new Date().toString()\" /></div>"
            +"<div class=\"fl info2\" id=\"ValidateCodeMsg\"></div>" 
            +"<div class=\"fl text2\">&nbsp;</div>"
            +"<div class=\"fl input2\"><input type=\"submit\" class=\"Btn\" value=\"提 交\" /></div>"
            +"<div class=\"fl info2\"></div>"
		+"<div class=\"close\" ><a href=\"javascript:utils.ConvenientLogin('hide');\">&nbsp;</a></div>"
        +"</div>"
		+"</form>"
      +"</div>"
    +"</div>";
	
	this.user = null;
}

var utils = new Utils();