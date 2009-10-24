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
}

var utils = new Utils();