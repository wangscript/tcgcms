//--------------

function show(){
	
}

function hidden(){
}

function ChangeIcon(num){
	for(var i =0 ;i<stNums[num];i++){
		var st = $("menu_"+num+"_"+i);
		if(st!=null){
			if(st.style.display=="none"){
				st.style.display="";
			}else{
				st.style.display="none";
			}
		}
	}
}