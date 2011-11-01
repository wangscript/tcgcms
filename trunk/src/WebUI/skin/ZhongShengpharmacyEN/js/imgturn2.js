// JavaScript Document
var n=0;
var showNum = document.getElementById("simg");
function Mea(value){
	n=value;
	setBg(value);
	plays(value);
	//cons(value);
	}
function setBg(value){
	for(var i=0;i<imgnums;i++)
   if(value==i){
		showNum.getElementsByTagName("td")[i].className='f1';
		} 
	else{	
		showNum.getElementsByTagName("td")[i].className='';
		}  
	} 
function plays(value){
	try
	{
		with (bimg)
		{
			filters[0].Apply();
			for(i=0;i<imgnums;i++)i==value?children[i].className="dis":children[i].className="undis"; 
			filters[0].play(); 
		}
	}
	catch(e)
	{
		var divlist = document.getElementById("bimg").getElementsByTagName("div");
		for(i=0;i<imgnums;i++)
		{
			i==value?divlist[i].className="dis":divlist[i].className="undis";
		}
	}

	
}
function cons(value){
	try
	{
		with (con)
		{
				for(i=0;i<imgnums;i++)i==value?children[i].className="dis":children[i].className="undis"; 		
		}
	}
	catch(e)
	{
		var divlist = document.getElementById("con").getElementsByTagName("div");
		for(i=0;i<imgnums;i++)
		{
			i==value?divlist[i].className="dis":divlist[i].className="undis";
		}		
	}
}

function clearAuto(){clearInterval(autoStart)}
function setAuto(){autoStart=setInterval("auto(n)", 5000)}
function auto(){
	n++;
	if(n>imgnums-1)n=0;
	Mea(n);
} 
function sub(){
	n--;
	if(n<0)n=imgnums-1;
	Mea(n);
} 
setAuto(); 